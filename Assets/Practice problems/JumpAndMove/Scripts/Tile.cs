using System;
using System.Collections;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private TileTrigger _tileTrigger;

    private Coroutine _startDisableRoutine;

    public void Init(float delay)
    {
        _startDisableRoutine = StartCoroutine(DisableAfterTime(delay));
        _tileTrigger.SetCanPlayerEnterTile(true);
    }

    public void ForceDisableTile()
    {
        _tileTrigger.SetCanPlayerEnterTile(false);

        if(_startDisableRoutine != null)
            StopCoroutine(_startDisableRoutine);

        gameObject.SetActive(false);

    }

    private IEnumerator DisableAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        _tileTrigger.SetCanPlayerEnterTile(false);
        gameObject.SetActive(false);
    }
}
