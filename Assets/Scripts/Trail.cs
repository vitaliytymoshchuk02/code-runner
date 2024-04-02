using UnityEngine;

public class Trail : MonoBehaviour
{
    [SerializeField] private int trailResolution = 10;
    private LineRenderer lineRenderer;
    private Player player;
    private Vector3[] lineSegmentPositions;
    private Vector3[] lineSegmentVelocities;
    private float offset = 2f;
    private Vector3 facingDirection;
    public enum LocalDirections { XAxis, YAxis, ZAxis }
    private LocalDirections localDirectionToUse = LocalDirections.YAxis;
    private float lagTime = 0.15f;

    private Vector3 GetDirection()
    {
        switch (localDirectionToUse)
        {
            case LocalDirections.XAxis:
                return transform.right;
            case LocalDirections.YAxis:
                return transform.up;
            case LocalDirections.ZAxis:
                return transform.forward;
        }

        Debug.LogError("The variable 'localDirectionToUse' on the 'ManualTrail' script, located on object " + name + ", was somehow invalid. Please investigate!");
        return Vector3.zero;
    }

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = trailResolution;

        lineSegmentPositions = new Vector3[trailResolution];
        lineSegmentVelocities = new Vector3[trailResolution];

        facingDirection = GetDirection();

        for (int i = 0; i < lineSegmentPositions.Length; i++)
        {
            lineSegmentPositions[i] = new Vector3();
            lineSegmentVelocities[i] = new Vector3();
            if (i == 0)
            {
                lineSegmentPositions[i] = new Vector3(player.transform.position.x, 0.1f, 0);
            }
            else
            {
                lineSegmentPositions[i] = new Vector3(player.transform.position.x, 0.1f, 0) - (facingDirection * (offset * i));
            }
        }
    }
    void Update()
    {
        facingDirection = GetDirection();

        for (int i = 0; i < lineSegmentPositions.Length; i++)
        {
            if (i == 0)
            {
                lineSegmentPositions[i] = new Vector3(player.transform.position.x, 0.1f, 0);
            }
            else
            {
                lineSegmentPositions[i] = Vector3.SmoothDamp(lineSegmentPositions[i], lineSegmentPositions[i - 1] - (facingDirection * offset), ref lineSegmentVelocities[i], lagTime);
            }
            lineRenderer.SetPosition(i, lineSegmentPositions[i]);
        }
    }
}