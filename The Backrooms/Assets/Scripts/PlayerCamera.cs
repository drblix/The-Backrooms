using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private UIManager uiManager;
    private InventoryManager invManager;

    [SerializeField] [Range(50f, 500f)]
    private float mouseSensitivity = 100f;

    private Transform plrCamera;

    [SerializeField]
    private Transform eventCam;

    private float xRotation;

    public static bool cameraInverted = false;

    [Header("Raycast Config")]

    [SerializeField] [Min(1f)]
    private float rayDistance;

    private int itemsLayer;

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
        invManager = FindObjectOfType<InventoryManager>();
        plrCamera = transform.Find("Head").Find("MainCamera");
        itemsLayer = LayerMask.GetMask("Items");

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (uiManager.GameLoaded)
        {
            RotateCamera();
            Raycast();
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

    private void Raycast()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(plrCamera.position, plrCamera.TransformDirection(Vector3.forward));

            Debug.DrawRay(plrCamera.position, plrCamera.TransformDirection(Vector3.forward), Color.red, 1f);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, rayDistance, itemsLayer))
            {
                GameObject obj = hitInfo.collider.gameObject;

                invManager.PickupItem(obj);
            }
        }
    }

    public void ToggleInversion(bool state)
    {
        cameraInverted = state;
    }
}
