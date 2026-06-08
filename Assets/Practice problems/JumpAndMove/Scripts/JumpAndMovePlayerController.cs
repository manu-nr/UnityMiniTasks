using UnityEngine;

public class JumpAndMovePlayerController : MonoBehaviour
{
    [SerializeField] private Vector3 _jumpForce;
    [SerializeField] private Rigidbody _playerRigidBody;
    [SerializeField] private float _movementForce;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _playerRigidBody.AddForce(_jumpForce, ForceMode.Impulse);
        }

        if (_playerRigidBody.linearVelocity != Vector3.zero)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                ControlPlayerMovement(MovementType.Forward);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                ControlPlayerMovement(MovementType.Backward);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ControlPlayerMovement(MovementType.Left);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                ControlPlayerMovement(MovementType.Right);
            }
        }
        

        Debug.Log("[NRM] Player velocity: " + _playerRigidBody.linearVelocity);
    }

    private void ControlPlayerMovement(MovementType type)
    {
        switch(type)
        {
            case MovementType.Forward:
                _playerRigidBody.AddForce(new Vector3(0f, 0f, _movementForce), ForceMode.Impulse);
                break;
            case MovementType.Backward:
                _playerRigidBody.AddForce(new Vector3(0f, 0f, -_movementForce), ForceMode.Impulse);
                break;
            case MovementType.Left:
                _playerRigidBody.AddForce(new Vector3(-_movementForce, 0f, 0f), ForceMode.Impulse);
                break;
            case MovementType.Right:
                _playerRigidBody.AddForce(new Vector3(_movementForce, 0f, 0f), ForceMode.Impulse);
                break;
        }
    }
}

public enum MovementType
{
    Forward, 
    Backward,
    Right,
    Left
}
