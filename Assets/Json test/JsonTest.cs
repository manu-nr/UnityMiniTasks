using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class CubeData
{
    [SerializeField] public string color;
    [SerializeField] public Vector3 position;
    [SerializeField] public Vector3 scale;
}

[Serializable]
public class CubesData
{
    [SerializeField] public CubeData[] cubes;
}

public class JsonTest : MonoBehaviour
{
    [SerializeField] private string _jsonData;
    [SerializeField] private CubesData _cubesData;

    void Start()
    {
        //CubesData data = new CubesData();

        _cubesData = JsonUtility.FromJson<CubesData>(_jsonData.ToString());

        if (_cubesData != null && _cubesData.cubes != null) Debug.Log("[M] Count: " + _cubesData.cubes.Count());

        if (_cubesData == null) Debug.Log("[M] Data is null");
        if (_jsonData == null) Debug.Log("[M] Json data is null");
    }

}
