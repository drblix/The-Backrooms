using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private GameObject loadingScreen;
    private GameObject plrStats;
    private GameObject pauseScreen;

    [SerializeField]
    private Image loadingBar;
    [SerializeField]
    private Gradient loadingBarColor;

    private bool canPause = false;

    private bool gameLoaded = false;
    public bool GameLoaded { get { return gameLoaded; } }

    private void Awake()
    {
        plrStats = transform.GetChild(0).gameObject;
        loadingScreen = transform.GetChild(1).gameObject;
        pauseScreen = transform.GetChild(2).gameObject;

        pauseScreen.transform.GetChild(2).GetComponent<Toggle>().isOn = PlayerCamera.cameraInverted;
    }

    private void Update()
    {
        PauseMenu();
    }

    private void PauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (canPause)
            {
                Cursor.lockState = CursorLockMode.None;

                Time.timeScale = 0f;
                plrStats.SetActive(false);
                pauseScreen.SetActive(true);
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;

        Time.timeScale = 1f;
        plrStats.SetActive(true);
        pauseScreen.SetActive(false);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    // Loading screen stuff
    public void UpdateLoadingBar(int remainingRooms, int roomsToMake)
    {
        float newFill = (float)remainingRooms / (float)roomsToMake;
        Color newColor = loadingBarColor.Evaluate(newFill);

        loadingBar.color = newColor;
        loadingBar.fillAmount = newFill;
    }

    public void FinishedLoading()
    {
        loadingScreen.SetActive(false);
        plrStats.SetActive(true);

        gameLoaded = true;
        canPause = true;
    }
}
