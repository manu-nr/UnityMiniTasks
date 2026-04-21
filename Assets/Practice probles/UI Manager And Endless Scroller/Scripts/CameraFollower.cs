using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private float _zOffset;
    [SerializeField] private float _yOffset;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private GameObject _player;

    private Vector3 _cameraPosition;

    private void LateUpdate()
    {
        _cameraPosition = _player.transform.position;
        _cameraPosition -= _offset;
        transform.position = _cameraPosition;
    }
}
