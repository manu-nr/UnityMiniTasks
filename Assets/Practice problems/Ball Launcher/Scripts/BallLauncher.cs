using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    [Header("Ball Pool")]
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private int _numberOfBallsToPool = 10;
    [SerializeField] private Transform _ballHolder;

    [Header("Launcher Settings")]
    [SerializeField] private float _throwForce = 10f;
    [SerializeField] private ScriptableObject _launchTarget;

    [Header("Spin Settings")]
    [SerializeField, UnityEngine.Range(-100, 300)] private float _xAxisAngularVelocity = 0f;
    [SerializeField, UnityEngine.Range(-100, 300)] private float _yAxisAngularVelocity = 0f;
    [SerializeField, UnityEngine.Range(-100, 300)] private float _zAxisAngularVelocity = 0f;

    [Header("Test")]
    [SerializeField] private float _launchHeight = 45f;
    [SerializeField] private bool _applyHeight = false;
    [SerializeField] private bool _applySpeed = false;


    private int _currentBallIndex = -1;
    private GameObject _currentBall;
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
            rb.angularVelocity = new Vector3(_xAxisAngularVelocity, _yAxisAngularVelocity, _zAxisAngularVelocity);
        }
    }

    #endregion

    #region Private Methods
    private GameObject GetBallFromPool()
    {
        ReturnBallToPool();
        _currentBallIndex = (_currentBallIndex + 1) % _numberOfBallsToPool;
        _currentBall = _ballList[_currentBallIndex];
        _currentBall.SetActive(true);
        return _currentBall;
    }

    private void ReturnBallToPool()
    {
        if (_currentBallIndex < 0) return;
        _currentBall.SetActive(false);
        _currentBall.transform.position = _ballHolder.position;
        _currentBall.transform.rotation = Quaternion.identity;
        _currentBall.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
    }
    #endregion
}
