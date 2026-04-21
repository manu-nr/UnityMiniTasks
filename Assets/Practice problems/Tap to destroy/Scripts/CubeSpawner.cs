using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [Header("Cube"), SerializeField] private GameObject _cubePrefab;
    [SerializeField] private Transform _cubeContainer;

    [Header("Spawn Info"), SerializeField] private Transform _spawnCenter;
    [SerializeField] private float _spawnRadius;

    [Header("Object Pool"), SerializeField] private int _maxPooledItems = 2;
    [SerializeField] private List<GameObject> _cubes = new List<GameObject>();

    private int _currentObjectIndex;

    private void Start()
    {
        for(int i=0; i<_maxPooledItems; i++)
        {
            _cubes.Add(Instantiate(_cubePrefab, _cubeContainer));
            _cubes[i].SetActive(false);
        }

        _currentObjectIndex = -1;
        Shooter.OnShoot += OnShoot;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            SpawnNewCube();
    }

    private void OnDestroy()
    {
        Shooter.OnShoot -= OnShoot;
    }

    private void OnShoot(GameObject cube)
    {
        SpawnNewCube();
    }

    private void SpawnNewCube()
    {
        float minX = _spawnCenter.position.x - _spawnRadius;
        float maxX = _spawnCenter.position.x + _spawnRadius;

        float minZ = _spawnCenter.position.z - _spawnRadius;
        float maxZ = _spawnCenter.position.z + _spawnRadius;

        Vector3 spawnPos = new Vector3(Random.Range(minX, maxX), 0.5f, Random.Range(minZ, maxZ));

        ReturnToPool();
        GameObject cube = GetPooledObject();
        cube.transform.position = spawnPos;
    }

    private GameObject GetPooledObject()
    {
        if (_currentObjectIndex < 0)
            _currentObjectIndex = 0;

        _cubes[_currentObjectIndex].SetActive(true);
        return _cubes[_currentObjectIndex];
    }

    private void ReturnToPool()
    {
        if (_currentObjectIndex < 0)
            return;

        _cubes[_currentObjectIndex].SetActive(false);
        ResetCube(_cubes[_currentObjectIndex]);
        _currentObjectIndex++;

        if (_currentObjectIndex >= _cubes.Count)
            _currentObjectIndex = 0;
    }

    private void ResetCube(GameObject cube)
    {
        cube.transform.position = Vector3.zero;
    }
}
