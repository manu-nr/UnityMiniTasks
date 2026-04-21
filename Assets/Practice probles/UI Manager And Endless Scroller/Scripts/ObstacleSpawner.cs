using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _obstacle;
    [SerializeField] private List<Transform> _spawnTransforms;

    public void Init()
    {
        int spawnIndex = Random.Range(0, _spawnTransforms.Count);

        _obstacle.transform.SetPositionAndRotation(_spawnTransforms[spawnIndex].position, _spawnTransforms[spawnIndex].rotation);
    }
}
