using UnityEngine;
using System.Collections;

public class Trail : MonoBehaviour
{
    public int trailResolution = 10;
    LineRenderer lineRenderer;
    Player player;

    Vector3[] lineSegmentPositions;
    Vector3[] lineSegmentVelocities;

    // This would be the distance between the individual points of the line renderer
    public float offset = 2f;
    Vector3 facingDirection;

    public enum LocalDirections { XAxis, YAxis, ZAxis }
    public LocalDirections localDirectionToUse = LocalDirections.ZAxis;

    // How far the points 'lag' behind each other in terms of position
    public float lagTime = 0.15f;

    Vector3 GetDirection()
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
    // Use this for initialization
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = trailResolution;

        lineSegmentPositions = new Vector3[trailResolution];
        lineSegmentVelocities = new Vector3[trailResolution];

        facingDirection = GetDirection();

        // Initialize our positions
        for (int i = 0; i < lineSegmentPositions.Length; i++)
        {
            lineSegmentPositions[i] = new Vector3();
            lineSegmentVelocities[i] = new Vector3();
            if (i == 0)
                {
                    // Set the first position to be at the base of the transform
                    lineSegmentPositions[i] = new Vector3(player.transform.position.x, 0.1f, 0);
            }
                else
                {
                    // All subsequent positions would be an offset of the original position.
                    lineSegmentPositions[i] = new Vector3(player.transform.position.x, 0.1f, 0) - (facingDirection * (offset * i));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        facingDirection = GetDirection();

        for (int i = 0; i<lineSegmentPositions.Length; i++)
        {
            if (i == 0)
            {
                // We always want the first position to be exactly at the original position
                lineSegmentPositions[i] = new Vector3(player.transform.position.x, 0.1f, 0);
            }
            else
            {
                // All others will follow the original with the offset that you set up*
                lineSegmentPositions[i] = Vector3.SmoothDamp(lineSegmentPositions[i], lineSegmentPositions[i - 1] - (facingDirection * offset), ref lineSegmentVelocities[i], lagTime);
            }
            // Once we�re done calculating where our position should be, set the line segment to be in its proper place*
            lineRenderer.SetPosition(i, lineSegmentPositions[i]);
        }
    }
}