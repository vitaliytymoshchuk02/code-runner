using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameObject pickup;
    [SerializeField] private TextMeshProUGUI fail;
    [SerializeField] private TextMeshProUGUI hold;
    [SerializeField] private GameObject button;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Spawner spawner;
    [SerializeField] private WallSpawner wallSpawner;
    [SerializeField] private PickupSpawner pickupSpawner;
    [SerializeField] private InputController inputController;
    [SerializeField] private CameraShake mainCamera;
    [SerializeField] private RagdollActivation ragdollActivation;
    private float offset = 0.501f;
    public enum State { Pause, Play, Lost }
    private State state = State.Pause;


    private void Awake()
    {
        state = State.Pause;
        fail.text = "";
        hold.text = "HOLD TO MOVE";

        button.SetActive(false);
        buttonText.text = "";
        Time.timeScale = 0f;
        player.enabled = false;

    }

    public void Play()
    {
        if (state == State.Lost)
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);

            /* the commented code is responsible for manually reloading the level, 
             which does not provide any advantages at this stage of development, 
             so it is replaced by the LoadScene method */

            /*ObjectMovement[] prefabs = FindObjectsOfType<ObjectMovement>();
            for (int i = 0; i < prefabs.Length; i++)
            {
                Destroy(prefabs[i].gameObject);
            }
            PickupPlayer[] pickups = FindObjectsOfType<PickupPlayer>();
            for (int i = 0; i < pickups.Length; i++)
            {
                pickups[i].enabled = true;
                pickups[i].Destroy();
            }
            spawner.enabled = true;
            wallSpawner.enabled = true;
            pickupSpawner.enabled = true;
            inputController.enabled = true;
            player.Reset();
            player.playerPickup.transform.position = Vector3.zero;
            spawner.Reset();
            wallSpawner.Reset();
            pickupSpawner.Reset();
            ragdollActivation.Reset();*/
        }

        Time.timeScale = 1.0f;
        player.enabled = true;
        hold.text = "";
        fail.text = "";

        button.SetActive(false);
        buttonText.text = "";
        state = State.Play;
    }

    public void RunIntoObstacle(GameObject go, Collision collision)
    {
        StartCoroutine(mainCamera.Shake());
        Handheld.Vibrate();

        if (go.gameObject.tag == "Player")
        {
            GameOver();
        }
        else if (go.transform.position.y <= (collision.gameObject.transform.position.y + offset))
        {
            CutPickups(go);
            player.UpdateHeight();
        }
    }

    private PickupPlayer FindPickup(GameObject go)
    {
        PickupPlayer[] pickups = FindObjectsOfType<PickupPlayer>();
        for (int i = 0; i < pickups.Length; i++)
        {
            if (pickups[i].gameObject == go)
            {
                return pickups[i];
            }
        }
        Debug.Log("GameManager.FindPickup returns null");
        return null;
    }

    public void RunIntoPickup(Collider collision)
    {
        player.AddPickup(collision);
        player.Jump();
    }

    void StopMovement()
    {
        ObjectMovement[] prefabs = FindObjectsOfType<ObjectMovement>();
        for (int i = 0; i < prefabs.Length; i++)
        {
            prefabs[i].enabled = false;
        }
        PickupPlayer[] pickups = FindObjectsOfType<PickupPlayer>();
        for (int i = 0; i < pickups.Length; i++)
        {
            pickups[i].enabled = false;
        }
    }

    void CutPickups(GameObject go)
    {
        if (player.GetList().Contains(go))
        {
            player.RemoveFromList(go);
            PickupPlayer pickup = FindPickup(go);
            pickup.Eliminate();

            go.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
        }
    }

    void GameOver()
    {
        fail.text = "FAIL!";
        StopMovement();
        player.enabled = false;
        spawner.enabled = false;
        wallSpawner.enabled = false;
        pickupSpawner.enabled = false;
        inputController.enabled = false;

        button.SetActive(true);
        buttonText.text = "TRY AGAIN";
        state = State.Lost;
    }

    public State GetState() => state;
    public void SetState(State s) => state = s;
}
