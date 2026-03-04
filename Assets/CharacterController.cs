using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private Animator _animation;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
            _animation.SetBool("IsWalking", true);
        else
            _animation.SetBool("IsWalking", false);
    }
}
