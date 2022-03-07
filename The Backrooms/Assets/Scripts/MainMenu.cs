using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    private Transform plrCamera;
    [SerializeField]
    private Transform eventCam;

    private GameObject mainScreen;
    private GameObject playScreen;

    [SerializeField]
    private TMP_InputField inputField;

    [Range(50, 1000)]
    public static int enteredRoomsToGenerate;

    [SerializeField] [Min(2f)]
    private float rotateSpeed;

    [SerializeField]
    private int maximumRooms;
    [SerializeField]
    private int minimumRooms;

    private void Awake()
    {
        Time.timeScale = 1f;

        plrCamera = Camera.main.transform;
        mainScreen = transform.GetChild(0).gameObject;
        playScreen = transform.GetChild(1).gameObject;

        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        Vector3 rotateDelta = rotateSpeed * Time.deltaTime * new Vector3(0f, 1f, 0f);
        plrCamera.Rotate(rotateDelta);
        eventCam.Rotate(rotateDelta);
    }

    public void PlayScreenToggle()
    {
        mainScreen.SetActive(!mainScreen.activeInHierarchy);
        playScreen.SetActive(!playScreen.activeInHierarchy);
    }

    public void GenerateButtonPressed()
    {
        int enteredRooms;
        try
        {
            enteredRooms = System.Convert.ToInt32(inputField.text);
        }
        catch (System.Exception)
        {
            Debug.LogError("Input not an integer");
            return;
        }

        if (enteredRooms < minimumRooms || enteredRooms > maximumRooms) { return; }

        enteredRoomsToGenerate = enteredRooms;

        SceneManager.LoadScene(1);
    }
}
