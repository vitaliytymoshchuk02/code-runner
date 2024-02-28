using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float spawnRate = 3f;
    [SerializeField] private Vector3 firstPosition = new Vector3(0, 0, -30);
    [SerializeField] private Vector3 secondPosition = new Vector3(0, 0, 0);
    [SerializeField] private Vector3 thirdPosition = new Vector3(0, 0, 30);
    [SerializeField] private Vector3 fourthPosition = new Vector3(0, 0, 60);

    private void Awake()
    {
        Reset();
    }

    public void Reset()
    {
        GameObject trackGround1 = Instantiate(prefab, firstPosition, Quaternion.identity);
        GameObject trackGround2 = Instantiate(prefab, secondPosition, Quaternion.identity);
        GameObject trackGround3 = Instantiate(prefab, thirdPosition, Quaternion.identity);
        GameObject trackGround4 = Instantiate(prefab, fourthPosition, Quaternion.identity);
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
        GameObject trackGround = Instantiate(prefab, transform.position, Quaternion.identity);
    }
}
