using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] private Platform _platformPrefab;
    [SerializeField] private List<Platform> _pooledPlatforms;
    [SerializeField] private int _platformCount = 5;
    [SerializeField] private Transform _platformHolder;
    [SerializeField] private Vector3 _spawnPosition = new Vector3(0, 0, 0);

    private int _currentPlatformIndex = -1;

    public static PlatformManager Instance;

    #region Unity Methods
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        for (int i = 0; i < _platformCount; i++)
        {
            SpawnPlatform();
        }
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.S))
        //    SpawnPlatform();
    }
    #endregion

    private void SpawnPlatform()
    {
        Platform platformObj = Instantiate(_platformPrefab, _platformHolder);
        platformObj.gameObject.SetActive(false);
        _pooledPlatforms.Add(platformObj);
    }

    public void SpawnNextPlatform()
    {
            Platform platformObj = GetPlatformFromPool();
            platformObj.transform.position = _spawnPosition;
    }

    public Platform GetPlatformFromPool()
    {
        _currentPlatformIndex++;
        if(_currentPlatformIndex >= _pooledPlatforms.Count)
            _currentPlatformIndex = 0;
        _pooledPlatforms[_currentPlatformIndex]?.gameObject.SetActive(true);
        return _pooledPlatforms[_currentPlatformIndex]; 
    }

    public void ReturnPlatformToPool()
    {
        if (_currentPlatformIndex == -1) return;
        _pooledPlatforms[_currentPlatformIndex]?.gameObject.SetActive(false);
    }
}
