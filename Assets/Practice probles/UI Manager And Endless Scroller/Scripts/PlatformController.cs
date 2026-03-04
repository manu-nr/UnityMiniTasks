using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private GameObject _platform;
    [SerializeField] private float _platformLength;
    [SerializeField] private int _maxPoolCount;
    [SerializeField] private List<GameObject> _platforms = new List<GameObject>();
    [SerializeField] private Transform _platformContainer;

    private float _spawnZPosition;
    private int _currentPlatformIndex = -1;

    #region Unity Methods
    void Start()
    {
        PlayerController.OnPlayerReachedMaxTriggerLength += SpawnPlatform;
        SpawnAndPoolPlatforms();
        SpawnPlatform();
        _spawnZPosition += _platformLength;
    }

    private void OnDestroy()
    {
        PlayerController.OnPlayerReachedMaxTriggerLength -= SpawnPlatform;
    }
    #endregion


    #region Private Methods

    private void SpawnPlatform()
    {
        //if(_currentPlatformIndex != -1)
        //    ReturnPlatformToPool();

        GameObject platform = GetPlatformFromPool();

        if (platform != null)
        {
            SetPlatformPosition(platform);
        }
    }

    private void SetPlatformPosition(GameObject platform)
    {
        platform.SetActive(true);
        platform.transform.SetPositionAndRotation(new Vector3(0f, 0f, _spawnZPosition), Quaternion.identity);
        _spawnZPosition += _platformLength;
    }

    private void ReturnPlatformToPool()
    {
        if(_platforms[_currentPlatformIndex] != null)
        {
            ResetPlatform(_platforms[_currentPlatformIndex]);
        }
    }

    private GameObject GetPlatformFromPool()
    {
        _currentPlatformIndex++;

        if (_currentPlatformIndex >= _maxPoolCount)
            _currentPlatformIndex = 0;

        if (_platforms[_currentPlatformIndex] != null)
        {
            _platforms[_currentPlatformIndex].SetActive(true);
            return _platforms[_currentPlatformIndex];
        }
        else
            return null;
    }

    private void SpawnAndPoolPlatforms()
    {
       for(int i=0; i<_maxPoolCount; i++)
       {
            _platforms.Add(Instantiate(_platform, _platformContainer));
            ResetPlatform(_platforms[i]);
       }
    }

    private void ResetPlatform(GameObject platform)
    {
        platform.SetActive(false);
        platform.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
    }


    #endregion

}
