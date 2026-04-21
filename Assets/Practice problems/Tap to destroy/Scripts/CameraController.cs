using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float minPitchValue = -30f;
    [SerializeField] private float maxPitchValue = 30f;

    [SerializeField] private float minYawValue = -30f;
    [SerializeField] private float maxYawValue = 30f;

    float mouseX = 0;
    float mouseY = 0;

    private void Update()
    {
        mouseX += Input.GetAxis("Mouse X");
        mouseY += Input.GetAxis("Mouse Y");

        mouseY = Mathf.Clamp(mouseY, minPitchValue, maxPitchValue);
        mouseX = Mathf.Clamp(mouseX, minYawValue, maxYawValue);

        transform.localRotation = Quaternion.Euler(-mouseY, mouseX, 0);
    }
}
