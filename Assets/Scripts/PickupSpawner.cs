using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] private float min_x = -2f;
    [SerializeField] private float max_x = 2f;
    [SerializeField] private float min_z = 55f;
    [SerializeField] private float max_z = 75f;
    [SerializeField] private float middle_z = 65f;
    [SerializeField] private float y = 0f;

    [SerializeField] private GameObject prefab;
    [SerializeField] private float spawnRate = 1f;

    public void Reset()
    {
        GameObject pickup1 = Instantiate(prefab, new Vector3(Random.Range(min_x, max_x), y, 35), Quaternion.identity);
        GameObject pickup2 = Instantiate(prefab, new Vector3(Random.Range(min_x, max_x), y, 40), Quaternion.identity);
        GameObject pickup3 = Instantiate(prefab, new Vector3(Random.Range(min_x, max_x), y, 45), Quaternion.identity);
        GameObject pickup4 = Instantiate(prefab, new Vector3(Random.Range(min_x, max_x), y, 60), Quaternion.identity);
        GameObject pickup5 = Instantiate(prefab, new Vector3(Random.Range(min_x, max_x), y, 65), Quaternion.identity);
        GameObject pickup6 = Instantiate(prefab, new Vector3(Random.Range(min_x, max_x), y, 70), Quaternion.identity);
    }
    private void Start()
    {
        Reset();
    }
    private void OnEnable()
    {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
    }

    private void Spawn()
    {
        GameObject pickup1 = Instantiate(prefab, transform.position, Quaternion.identity);
        pickup1.transform.position = new Vector3(Random.Range(min_x,max_x), y, Random.Range(min_z, middle_z - 5));
        GameObject pickup2 = Instantiate(prefab, transform.position, Quaternion.identity);
        pickup2.transform.position = new Vector3(Random.Range(min_x, max_x), y, middle_z);
        GameObject pickup3 = Instantiate(prefab, transform.position, Quaternion.identity);
        pickup3.transform.position = new Vector3(Random.Range(min_x, max_x), y, Random.Range(middle_z + 5, max_z));
    }
}
