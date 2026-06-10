using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private float _tileSpacing;
    [SerializeField] private float _tileDisableSpeed;

    [Header("Pool"), SerializeField] private float _poolSize;
    [SerializeField] private List<Tile> _pooledTiles;

    private int _currentTileIndex = -1;
    private int _previousTileIndex = -1;

    private Tile _tile1;
    private Tile _tile2;

    #region Unity Methods
    private void Start()
    {
        for(int i=0; i<_poolSize; i++)
        {
            Tile tile = Instantiate(_tilePrefab, transform);
            tile.gameObject.SetActive(false);
            _pooledTiles.Add(tile);
        }
        SpawnTile(10f);
        SpawnTileAfterDelay(5f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            SpawnTile(_tileDisableSpeed);
    }
    #endregion

    private void SpawnTile(float delay = 0f)
    {
        Tile tile = GetTile();
        tile.gameObject.SetActive(true);
        tile.StartDisable(delay);
        PositionTile(tile);
    }

    private void SpawnTileAfterDelay(float delay = 0f)
    {
        StartCoroutine(SpawnTileAfterDelayRoutine(delay));
    }

    private IEnumerator SpawnTileAfterDelayRoutine(float delay = 0f)
    {
        yield return new WaitForSeconds(delay);
        SpawnTile(_tileDisableSpeed);
    }

    private void PositionTile(Tile tile)
    {
        if(_previousTileIndex >= 0)
        {
            Vector3 newPosition = _pooledTiles[_previousTileIndex].transform.position;
            int axis = Random.Range(0, 2);

            float offsetValue = Random.value > 0.5f ? 1 : -1;

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
