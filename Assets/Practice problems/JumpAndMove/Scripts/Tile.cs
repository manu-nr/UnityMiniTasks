using System;
using System.Collections;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private Coroutine _startDisableRoutine;

    public void StartDisable(float delay)
    {
        _startDisableRoutine = StartCoroutine(DisableAfterTime(delay));
    }

    public void ForceDisableTile()
    {
        gameObject.SetActive(false);

        if(_startDisableRoutine != null)
            StopCoroutine(_startDisableRoutine);
    }

    private IEnumerator DisableAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
