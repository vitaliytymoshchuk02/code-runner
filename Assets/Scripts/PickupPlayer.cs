using UnityEngine;

public class PickupPlayer : MonoBehaviour
{
    [SerializeField] private bool eliminated = false;
    [SerializeField] private GameObject targetObject;
    [SerializeField] private float speed;

    private Vector3 currentPoint;
    private Vector3 targetPoint;
    private float timeToTargetPoint;
    private float elapsedTime;

    void Update()
    {
        if (eliminated)
        {
            elapsedTime += Time.deltaTime;
            float elapsedPercentage = elapsedTime / timeToTargetPoint;
            transform.position = Vector3.Lerp(currentPoint, targetPoint, elapsedPercentage);

            if (elapsedPercentage >= 1)
            {
                Destroy(gameObject);
            }
        }
    }
    public void Destroy()
    {
        if (eliminated)
        {
            Destroy(gameObject);
        }
    }

    public void Eliminate()
    {
        TargetPoint();
        eliminated = true;
    }
    public bool GetEliminated() => eliminated;
    private void TargetPoint()
    {
        currentPoint = transform.position;
        targetPoint = new Vector3(currentPoint.x, currentPoint.y, targetObject.transform.position.z);
        elapsedTime = 0f;

        float distanceToPoint = Vector3.Distance(currentPoint, targetPoint);
        timeToTargetPoint = distanceToPoint / speed;
    }
}
