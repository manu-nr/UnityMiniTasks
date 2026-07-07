using System;
using UnityEngine;

public class TileTrigger : MonoBehaviour
{
    public bool _canPlayerEnterTile = false;

    public static event Action OnPlayerEnterTile;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");

        if (other.CompareTag("Player") && _canPlayerEnterTile)
        {
            OnPlayerEnterTile?.Invoke();
            _canPlayerEnterTile = false; // Prevent further triggers until reset
        }
    }

    public void SetCanPlayerEnterTile(bool canEnter)
    {
        _canPlayerEnterTile = canEnter;
    }
}
