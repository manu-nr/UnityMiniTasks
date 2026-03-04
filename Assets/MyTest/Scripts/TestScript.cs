using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] private Transform _cubeOne;
    [SerializeField] private Vector3 _axis;

    
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position = Vector3.Lerp(transform.position, _target.position, _speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
            transform.rotation = Quaternion.Slerp(transform.rotation, _target.rotation, _speed*Time.deltaTime);
        else if(Input.GetKey(KeyCode.M))
        {
            transform.position = transform.position + _target.position.normalized * Time.deltaTime;
            //Debug.Log("[NRM] Nomralized target position: " + _target.position.normalized);
        }
    }
    //void Update()
    //{
    //    gameObject.transform.RotateAround(_cubeOne.transform.position, _axis, 45f * Time.deltaTime);
    //}
}
