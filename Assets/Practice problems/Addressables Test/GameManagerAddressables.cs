using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GameManagerAddressables : MonoBehaviour
{
    [SerializeField] private string _groupName;
    [SerializeField] private List<AssetReference> _cubePrefabReference;
    private AsyncOperationHandle<GameObject> _handle;
    private void Start()
    {
        //LoadGameObject();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadGameObject();
        }

        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            ReleaseGameObject();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            LoadGameObjectByGroup();
        }
    }

    private async void LoadGameObject()
    {
        _handle = Addressables.LoadAssetAsync<GameObject>("Cube1");
        await _handle.Task;
        GameObject gameObject = _handle.Result;
        Instantiate(gameObject, Vector3.zero, Quaternion.identity);
        Addressables.Release(_handle);
    }

    private async void LoadGameObjectByGroup()
    {
        AsyncOperationHandle<IList<GameObject>> handle = Addressables.LoadAssetsAsync<GameObject>(_groupName, null);
        await handle.Task;
        foreach (var obj in handle.Result)
        {
            Instantiate(obj, Vector3.zero, Quaternion.identity);
        }
    }

    private void ReleaseGameObject()
    {
        Addressables.Release(_handle);
    }
}
