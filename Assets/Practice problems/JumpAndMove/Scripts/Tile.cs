using System;
using System.Collections;
using UnityEngine;

public class Tile : MonoBehaviour
{

    public void StartDisable(float delay)
    {
        StartCoroutine(DisableAfterTime(delay));
    }

    private IEnumerator DisableAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
