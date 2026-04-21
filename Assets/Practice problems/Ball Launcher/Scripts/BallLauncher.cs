using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    [SerializeField] private GameObject _ballPrefab;

    [SerializeField] private int _numberOfBallsToPool = 10;
    [SerializeField] private Transform _ballHolder;
    [SerializeField] private float _throwForce = 500f;
    [SerializeField] private ScriptableObject _launchTarget;

    [Header("Test")]
    [SerializeField] private float _launchHeight = 45f;


    private int _currentBallIndex = -1;
    private List<GameObject> _ballList = new List<GameObject>();

    #region Unity Methods
    private void Start()
    {
        for(int i=0; i<_numberOfBallsToPool; i++)
        {
            GameObject ball = Instantiate(_ballPrefab, _ballHolder);
            ball.SetActive(false);
            _ballList.Add(ball);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            GameObject ball = GetBallFromPool();
            Quaternion rotation = ball.transform.rotation;
            rotation.x = _launchHeight;
            ball.transform.rotation = rotation;
            Rigidbody rb = ball.GetComponent<Rigidbody>();

            Vector3 angle = new Vector3(0f, Mathf.Sin(_launchHeight * Mathf.Deg2Rad), Mathf.Cos(_launchHeight * Mathf.Deg2Rad));
            rb.AddForce(angle * _throwForce, ForceMode.Impulse);
        }
    }

    #endregion

    #region Private Methods
    private GameObject GetBallFromPool()
    {
        ReturnBallToPool();
        _currentBallIndex = (_currentBallIndex + 1) % _numberOfBallsToPool;
        _ballList[_currentBallIndex].SetActive(true);
        return _ballList[_currentBallIndex];
    }

    private void ReturnBallToPool()
    {
        if (_currentBallIndex < 0) return;
        GameObject ball = _ballList[_currentBallIndex];
        ball.SetActive(false);
        ball.transform.position = _ballHolder.position;
        ball.transform.rotation = Quaternion.identity;
        ball.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
    }
    #endregion
}
