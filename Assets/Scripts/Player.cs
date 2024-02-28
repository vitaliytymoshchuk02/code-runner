using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    public LinkedList<GameObject> _list = new LinkedList<GameObject>();
    [SerializeField] public GameObject playerPickup;
    [SerializeField] public GameObject stickman;
    [SerializeField] private float gravity = 9.8f;
    [SerializeField] private GameObject collectCubeText;
    private float height = 1;

    private void Awake()
    {
        animator = stickman.GetComponent<Animator>();
    }

    private void Update()
    {
        stickman.GetComponent<Rigidbody>().AddForce(Vector3.down * gravity);
        playerPickup.GetComponent<Rigidbody>().AddForce(Vector3.down * gravity);
    }

    public void AddPickup(Collider collision)
    {
        if (collision.gameObject.activeSelf != false)
        {
            transform.position += Vector3.up;
            if (_list.Count > 0)
            {
                foreach (GameObject go in _list)
                {
                    go.transform.position += Vector3.up;
                }
            }

            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject);
            GameObject pickup = Instantiate(playerPickup, new Vector3(stickman.transform.position.x, 0, 0), Quaternion.identity);
            GameObject collectText = Instantiate(collectCubeText, new Vector3(stickman.transform.position.x, stickman.transform.position.y, 0), Quaternion.identity);
            _list.AddLast(pickup);

            UpdateHeight();
        }
    }

    public void Jump()
    {
        if (animator != null)
        {
            animator.SetTrigger("Jump");
        }
    }
    public void UpdateHeight() => height = _list.Count + 1f;

    public float GetHeight() => height;

    public void Reset()
    {
        _list.Clear();
        transform.position = Vector3.zero;
        stickman.transform.position = Vector3.up;
    }
}

