using System.Collections;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private float _disableSpeed;

    public void StartDisable()
    {
        StartCoroutine(DisableAfterTime());
    }

    private IEnumerator DisableAfterTime()
    {
        yield return new WaitForSeconds(_disableSpeed);
        gameObject.SetActive(false);
    }
}
