using UnityEngine;

public class JumpAndMovePlayerController : MonoBehaviour
{
    [SerializeField] private Vector3 _jumpForce;
    [SerializeField] private Rigidbody _playerRigidBody;
    [SerializeField] private float _movementForce;

    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
        JumpAndMoveGameManager.OnGameStarted += OnGameStarted;

        TogglePlayer(false);
    }

    private void OnDestroy()
    {
        JumpAndMoveGameManager.OnGameStarted -= OnGameStarted;
    }

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
    private void OnGameStarted(bool started)
    {
        if (started)
            TogglePlayer(true);
        else
            OnGameOver();
    }

    private void OnGameOver()
    {
        ResetPlayerPosition();
    }

    private void ResetPlayerPosition()
    {
        transform.position = _startPosition;
        TogglePlayer(false);
    }

    private void TogglePlayer(bool on)
    {
        gameObject.SetActive(on);
    }
}

public enum MovementType
{
    Forward, 
    Backward,
    Right,
    Left
}
