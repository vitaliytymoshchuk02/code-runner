using UnityEngine;

public class OnCollision : MonoBehaviour
{
    private GameManager gameManager;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Obstacle":
                gameManager.RunIntoObstacle(transform.gameObject, collision);
                break;
            default: break;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        switch (collision.gameObject.tag) {
            case "NewPickup":
                gameManager.RunIntoPickup(collision);
                break;
            case "TargetObject":
                Destroy(gameObject);
                break;
            default: break;
        }
    }
}
