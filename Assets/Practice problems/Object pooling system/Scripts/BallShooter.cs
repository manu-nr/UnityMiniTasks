using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BallShooter : MonoBehaviour
{
    [SerializeField] private GameObject _ball;
    [SerializeField] private int _poolSize = 10;
    [SerializeField] private Transform _ballContainer;
    [SerializeField] private Vector3 _ballForce;

    private int _currentBallIndex = -1;
    private List<GameObject> _pooledBalls = new List<GameObject>();

    private void Start()
    {
        for(int i=0; i<_poolSize; i++)
        {
            GameObject ball = Instantiate(_ball, _ballContainer);
            ball.transform.position = Vector3.zero;
            ball.SetActive(false);
            _pooledBalls.Add(ball);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ShootBall();
    }

    private void ShootBall()
    {
        ReturnBallToPool();
        GameObject ball = GetBallFromPool();
        ball.transform.position = _ballContainer.transform.position;
        ball.GetComponent<Rigidbody>().AddForce(_ballForce, ForceMode.Impulse);
    }

    private GameObject GetBallFromPool()
    {
        _pooledBalls[_currentBallIndex].SetActive(true);
        return _pooledBalls[_currentBallIndex];
    }

    private void ReturnBallToPool()
    {
        if (_currentBallIndex != -1)
        {
            _pooledBalls[_currentBallIndex].transform.position = Vector3.zero;
            _pooledBalls[_currentBallIndex].SetActive(false);
        }

        _currentBallIndex++;

        if (_currentBallIndex >= _poolSize)
            _currentBallIndex = 0;
    }

}
