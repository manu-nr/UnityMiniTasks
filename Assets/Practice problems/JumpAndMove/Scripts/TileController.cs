using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private float _tileSpacing;
    [SerializeField] private float _tileDisableSpeed;
    [SerializeField] private float _nextTileSpawnSpeed;

    [Header("Pool"), SerializeField] private float _poolSize;
    [SerializeField] private List<Tile> _pooledTiles;

    private int _currentTileIndex = -1;
    private int _previousTileIndex = -1;

    private Coroutine _spawnTileRoutine;

    public static event Action<Vector3> OnTileSpawned;

    #region Unity Methods
    private void Start()
    {
        JumpAndMoveGameManager.OnGameStarted += OnGameStarted;

        for (int i=0; i<_poolSize; i++)
        {
            Tile tile = Instantiate(_tilePrefab, transform);
            tile.gameObject.SetActive(false);
            _pooledTiles.Add(tile);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            SpawnTile();
    }

    private void OnDestroy()
    {
        JumpAndMoveGameManager.OnGameStarted -= OnGameStarted;
    }
    #endregion

    #region Private Methods

    private void OnGameStarted(bool started)
    {
        if (started)
            SpawnTile();
        else
            StopSpawning();
    }

    private void StopSpawning()
    {
        if(_spawnTileRoutine != null)
            StopCoroutine(_spawnTileRoutine);

        ResetTiles();
    }

    private void SpawnTile()
    {
        Tile tile = GetTile();
        tile.gameObject.SetActive(true);
        tile.StartDisable(_tileDisableSpeed);
        PositionTile(tile);
        SpawnTileAfterDelay(_nextTileSpawnSpeed);
        OnTileSpawned?.Invoke(tile.transform.position);
    }

    private void ResetTiles()
    {
        _currentTileIndex = -1;
        _previousTileIndex = -1;

        foreach (Tile tile in _pooledTiles)
        {
            tile.ForceDisableTile();
        }
    }

    private void SpawnTileAfterDelay(float delay = 0f)
    {
        _spawnTileRoutine = StartCoroutine(SpawnTileAfterDelayRoutine(delay));
    }

    private IEnumerator SpawnTileAfterDelayRoutine(float delay = 0f)
    {
        yield return new WaitForSeconds(delay);
        SpawnTile();
    }

    private void PositionTile(Tile tile)
    {
        if(_previousTileIndex >= 0)
        {
            Vector3 newPosition = _pooledTiles[_previousTileIndex].transform.position;
            int axis = UnityEngine.Random.Range(0, 2);

            float offsetValue = UnityEngine.Random.value > 0.5f ? 1 : -1;

            if (axis == 0)
                newPosition.x += _tileSpacing * offsetValue;
            else
                newPosition.z += _tileSpacing * offsetValue;

            tile.transform.position = newPosition;
        }
        else
        {
            tile.transform.position = Vector3.zero;
        }
    }
    #endregion

    #region Public Methods
    public Tile GetTile()
    {
        _previousTileIndex = _currentTileIndex;
        _currentTileIndex++;

        if (_currentTileIndex >= _poolSize)
            _currentTileIndex %= _pooledTiles.Count;

        return _pooledTiles[_currentTileIndex];
    }

    public void ReturnTile(int index)
    {
        if (index < 0) return;
        _pooledTiles[index].gameObject.SetActive(false);
    }
    #endregion
}
