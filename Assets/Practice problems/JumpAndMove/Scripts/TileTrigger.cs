using System;
using UnityEngine;

public class TileTrigger : MonoBehaviour
{
    public static event Action OnPlayerEnterTile;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");

        if (other.CompareTag("Player"))
        {
            OnPlayerEnterTile?.Invoke();
        }
    }
}
