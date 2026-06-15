using UnityEngine;

public class JumpAndMoveCameraController : MonoBehaviour
{
    [SerializeField] private float _cameraOffset;
    [SerializeField] private float _cameraSpeed;

    private Vector3 _targetPosition;

    private void Start()
    {
        TileController.OnTileSpawned += MoveCameraToTile;
    }

    private void OnDestroy()
    {
        TileController.OnTileSpawned -= MoveCameraToTile;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _targetPosition, _cameraSpeed * Time.deltaTime);
    }


    private void MoveCameraToTile(Vector3 tilePosition)
    {
        _targetPosition = new Vector3(tilePosition.x, transform.position.y, tilePosition.z - _cameraOffset);
    }
}
