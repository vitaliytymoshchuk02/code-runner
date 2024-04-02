using UnityEngine;

public class CollectCubeText : MonoBehaviour
{
    private Vector3 currentPoint;
    private Vector3 targetPoint;
    private float elapsedTime;
    private float timeToTargetPoint;
    [SerializeField] private float speed = 1f;
    private bool firstPointReached = false;
    private void Awake()
    {
        TargetPoint();
    }
    void Update()
    {
        if (firstPointReached) 
        {
            elapsedTime += Time.deltaTime;
            float elapsedPercentage = elapsedTime / timeToTargetPoint;
            transform.position = Vector3.Lerp(currentPoint, targetPoint, elapsedPercentage);
            if (elapsedPercentage >= 1)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            elapsedTime += Time.deltaTime;
            float elapsedPercentage = elapsedTime / timeToTargetPoint;
            transform.position = Vector3.Lerp(currentPoint, targetPoint, elapsedPercentage);

            if (elapsedPercentage >= 1)
            {
                firstPointReached = true;
                TargetPoint();
            }
        }
    }
    private void TargetPoint()
    {
        currentPoint = transform.position;
        if (firstPointReached)
        {
            targetPoint = new Vector3(currentPoint.x, currentPoint.y, -40);
        }
        else
        {
            targetPoint = new Vector3(currentPoint.x, currentPoint.y + 1f, 0);
        }
        elapsedTime = 0f;

        float distanceToPoint = Vector3.Distance(currentPoint, targetPoint);
        timeToTargetPoint = distanceToPoint / speed;
    }
}
