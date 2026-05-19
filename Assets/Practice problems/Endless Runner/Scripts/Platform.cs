using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _disableDistance = -20f;

    private void Update()
    {
        transform.Translate(Vector3.back * _moveSpeed * Time.deltaTime);
        if(transform.position.z <= _disableDistance)
        {
            PlatformManager.Instance.SpawnNextPlatform();
            gameObject.SetActive(false);
        }
    }
}
