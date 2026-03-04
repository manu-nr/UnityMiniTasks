using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _ball;
    [SerializeField] private GameObject _ballsContainer;
    [SerializeField] private int _ballCount;
    [SerializeField] private int _ballSpeed = 30;

    private List<GameObject> _ballList = new List<GameObject>();
    private int _nextBallIndex = 0;
    private GameObject _currentBall;

    void Start()
    {
        for (int i = 0; i < _ballCount; i++)
        {
            GameObject ball = Instantiate(_ball, _ballsContainer.transform);
            ball.SetActive(false);
            _ballList.Add(ball);
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            SpawnBall();
        }
    }

    private void SpawnBall()
    {
        _currentBall = GetBallFromPool();
        _currentBall.SetActive(true);
        _currentBall.transform.position = _spawnPoint.position;
        _currentBall.GetComponent<Rigidbody>().AddForce(Vector3.forward*_ballSpeed, ForceMode.Impulse);
    }

    private void ReturnPreviousBallToPool()
    {
        _currentBall.transform.position = Vector3.zero;
        _currentBall.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        _currentBall.SetActive(false);

    }

    private GameObject GetBallFromPool()
    {
        if(_currentBall != null)
            ReturnPreviousBallToPool();

        if(_nextBallIndex >= _ballList.Count)
            _nextBallIndex = 0;

        return _ballList[_nextBallIndex++];
    }
}
