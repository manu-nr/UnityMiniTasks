using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _disableDistance = -20f;
    [SerializeField] private float _spawnNextDistance = -20f;

    private bool _canSpawnNextPlatform = true;

    private void Update()
    {
        transform.Translate(Vector3.back * _moveSpeed * Time.deltaTime);
        if (transform.position.z <= _spawnNextDistance && _canSpawnNextPlatform)
        {
            _canSpawnNextPlatform = false;
            Debug.Log("Transform while spawning next platform: " + transform.position);
            PlatformManager.Instance.SpawnNextPlatform(); 
        }
        if (transform.position.z <= _disableDistance)
        {
            transform.position = Vector3.zero;
            Resetvars();
            gameObject.SetActive(false);
        }
    }

    private void Resetvars()
    {
        _canSpawnNextPlatform = true;
    }
}
