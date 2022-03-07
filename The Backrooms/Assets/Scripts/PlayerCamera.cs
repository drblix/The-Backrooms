using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private UIManager uiManager;

    [SerializeField] [Range(50f, 500f)]
    private float mouseSensitivity = 100f;

    private Transform plrCamera;
    [SerializeField]
    private Transform eventCam;

    private float xRotation;

    public static bool cameraInverted = false;


    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
        plrCamera = transform.Find("Head").Find("MainCamera");

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (uiManager.GameLoaded)
        {
            RotateCamera();
        }
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
        eventCam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }

    public void ToggleInversion(bool state)
    {
        cameraInverted = state;
    }
}
