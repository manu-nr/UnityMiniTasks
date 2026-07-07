using UnityEngine;

public class JumpAndMoveCameraController : MonoBehaviour
{
    [SerializeField] private float _cameraOffset;
    [SerializeField] private float _cameraSpeed;

    private Vector3 _targetPosition;
    private JumpAndMoveGameManager _gameManager;

    #region Unity Methods
    private void Start()
    {
        _gameManager = JumpAndMoveGameManager.Instance;
        TileController.OnTileSpawned += MoveCameraToTile;
    }

    private void OnDestroy()
    {
        TileController.OnTileSpawned -= MoveCameraToTile;
    }

    private void Update()
    {
        if(_gameManager.IsGameStarted)
            transform.position = Vector3.Lerp(transform.position, _targetPosition, _cameraSpeed * Time.deltaTime);
    }
    #endregion

    #region Private Methods

    private void MoveCameraToTile(Vector3 tilePosition)
    {
        _targetPosition = new Vector3(tilePosition.x, transform.position.y, tilePosition.z - _cameraOffset);
    }

   

    #endregion
}
