using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    [SerializeField] private float _maxShootDistance = 50f;

    public static event Action<GameObject> OnShoot;
    
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, _maxShootDistance))
            {
                if(hitInfo.collider.CompareTag("Cube"))
                {
                    OnShoot?.Invoke(hitInfo.collider.gameObject);
                }
            }
        }
    }
}
