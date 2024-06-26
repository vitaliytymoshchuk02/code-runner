using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject cube;
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private Player player;
    [SerializeField] private Vector3 firstWallPosition = new Vector3(0,0,50);
    [SerializeField] private Vector3 secondWallPosition = new Vector3(0,0,80);
    private float possibleHeight;
    private float maxHeight = 5f;
    private float addition = 2f;

    void updatePossibleHeight()
    {
        possibleHeight = player.GetHeight() + addition;
    }

    public void Reset()
    {
        SpawnLine(-2f, firstWallPosition.z);
        SpawnLine(-1f, firstWallPosition.z);
        SpawnLine(0f, firstWallPosition.z);
        SpawnLine(1f, firstWallPosition.z);
        SpawnLine(2f, firstWallPosition.z);

        SpawnLine(-2f, secondWallPosition.z);
        SpawnLine(-1f, secondWallPosition.z);
        SpawnLine(0f, secondWallPosition.z);
        SpawnLine(1f, secondWallPosition.z);
        SpawnLine(2f, secondWallPosition.z);
    }

    private void Awake()
    {
        Reset();
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(SpawnCubes), spawnRate, spawnRate);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(SpawnCubes));
    }

    private void Spawn()
    {
        GameObject trackGround = Instantiate(prefab, transform.position, Quaternion.identity);
    }

    private void SpawnCubes()
    {
        SpawnLine(-2f, transform.position.z);
        SpawnLine(-1f, transform.position.z);
        SpawnLine(0f, transform.position.z);
        SpawnLine(1f, transform.position.z);
        SpawnLine(2f, transform.position.z);
    }

    private void SpawnLine(float x, float z)
    {
        updatePossibleHeight();
        float size = Random.Range(1, possibleHeight);
        if(size > maxHeight) size = maxHeight;
        int y = 0;
        for(int i = 0; i < size; i++)
        {
            GameObject wall = Instantiate(cube, new Vector3(x, y, z), Quaternion.identity);
            y++;
        }
    }
}
