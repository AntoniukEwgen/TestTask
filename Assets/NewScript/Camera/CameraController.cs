using UnityEngine;
using Zenject;

public class CameraController : MonoBehaviour
{
    [Inject]
    private ICameraSettings _cameraSettings;

    [Inject]
    private IInputHandler _inputHandler;

    private Vector3 target = Vector3.zero;

    void Update()
    {
        MoveCamera();
        RotateCamera();
    }

    void MoveCamera()
    {
        float translationX = _inputHandler.GetHorizontalInput() * _cameraSettings.Speed;
        float translationZ = _inputHandler.GetVerticalInput() * _cameraSettings.Speed;

        translationX *= Time.deltaTime;
        translationZ *= Time.deltaTime;

        transform.Translate(translationX, 0, translationZ);
    }

    void RotateCamera()
    {
        if (Input.GetMouseButton(1))
        {
            float rotationY = Input.GetAxis("Mouse X") * _cameraSettings.RotationSpeed * Time.deltaTime;
            float rotationX = Input.GetAxis("Mouse Y") * _cameraSettings.RotationSpeed * Time.deltaTime;

            transform.Rotate(Vector3.up, rotationY, Space.World);
            transform.Rotate(transform.right, -rotationX, Space.World);
        }
    }
}
