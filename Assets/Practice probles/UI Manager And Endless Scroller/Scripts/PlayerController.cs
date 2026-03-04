using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _horizontalGap;
    [SerializeField] private float _minBoundary;
    [SerializeField] private float _maxBoundary;
    [SerializeField] private float _speed;

    //[SerializeField] private float _spawn

    void Update()
    {
        Run();

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PositionPlayer(PlayerInputType.Left);
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            PositionPlayer(PlayerInputType.Right);
        }
        else if(Input.GetKeyDown(KeyCode.Space))
        {

        }
    }

    private void PositionPlayer(PlayerInputType inputType)
    {
        float newPlayerPos = transform.position.x;

        if(inputType == PlayerInputType.Left)
        {
            newPlayerPos -= _horizontalGap;
        }
        else if(inputType == PlayerInputType.Right)
        {
            newPlayerPos += _horizontalGap;
        }

        newPlayerPos = Mathf.Clamp(newPlayerPos, _minBoundary, _maxBoundary);
        
        Vector3 position = transform.position;

        transform.position = new Vector3(newPlayerPos, position.y, position.z);
    }

    private void Run()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }



    public enum PlayerInputType
    {
        Left,
        Right, 
        Jump
    }
}

