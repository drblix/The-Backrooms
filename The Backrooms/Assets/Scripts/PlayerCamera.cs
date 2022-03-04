using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] [Range(50f, 500f)]
    private float mouseSensitivity = 100f;

    private Transform plrCamera;

    private float xRotation;

    private bool cameraInverted = false;


    private void Awake()
    {
        plrCamera = transform.Find("MainCamera");

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        RotateCamera();
        CheckInput();
    }

    private void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        if (!cameraInverted)
        {
            xRotation -= mouseY;
        }
        else if (cameraInverted)
        {
            xRotation += mouseY;
        }

        xRotation = Mathf.Clamp(xRotation, -70f, 70f);

        plrCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            cameraInverted = !cameraInverted;
        }
    }
}
