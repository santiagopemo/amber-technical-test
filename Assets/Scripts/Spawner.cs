using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public int maxInScene = 100;
    public float spawnRate = 3f;
    public float spawnRadius = 1f;
    public float startAfter = 0f;

    private bool _startSpawn = false;
    [SerializeField] private int _count = 0;
    private float _spawnDelay;

    void Start()
    {
        _spawnDelay = spawnRate + startAfter;
        InitializeSpawner();
    }

    void Update()
    {
        Utilities.UpdateTimer(_startSpawn, ref _spawnDelay, spawnRate, SpawnInCircle);
    }

    private void InitializeSpawner()
    {
        _startSpawn = true;
    }

    private void StopSpawner()
    {
        _startSpawn = false;
    }

    private void SpawnInCircle()
    {
        if (transform.childCount >= maxInScene) return;
        Vector2 randomXZ = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPosition = new Vector3(randomXZ.x + transform.position.x, 
                    transform.position.y, randomXZ.y + transform.position.z);
        Instantiate(prefab, spawnPosition, transform.rotation, transform);
        _count++;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
