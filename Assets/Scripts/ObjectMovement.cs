using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private float speed;

    private Vector3 currentPoint;
    private Vector3 targetPoint;
    private float timeToTargetPoint;
    private float elapsedTime;

    private void Start()
    {
        TargetPoint();
    }
    void Update()
    {
        elapsedTime += Time.deltaTime;
        float elapsedPercentage = elapsedTime / timeToTargetPoint;
        transform.position = Vector3.Lerp(currentPoint, targetPoint, elapsedPercentage);
     
        if (elapsedPercentage >= 1)
        {
            Destroy(gameObject);
        }
    }

    private void TargetPoint()
    {
        currentPoint = transform.position;
        targetPoint = new Vector3(currentPoint.x, currentPoint.y, targetObject.transform.position.z);
        elapsedTime = 0f;

        float distanceToPoint = Vector3.Distance(currentPoint, targetPoint);
        timeToTargetPoint = distanceToPoint/ speed;
    }
}
