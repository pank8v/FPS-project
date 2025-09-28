using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private InputSystem_Actions inputSystem;
    [SerializeField] private PlayerInputHandler playerInputHandler;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject hud;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button exitButton;
    public static bool isPaused;
    
    private void Start() {
        playerInputHandler.OnPause += TogglePauseMenu;
        inputSystem = new InputSystem_Actions();
        resumeButton.onClick.AddListener(ResumeGame);
        mainMenuButton.onClick.AddListener(ToMainMenu);
        exitButton.onClick.AddListener(ExitGame);
    }




    private void TogglePauseMenu() {
        isPaused = !isPaused;
        if (isPaused)
            PauseGame();
        else
            ResumeGame();
    }

    private void PauseGame() {
        inputSystem.Player.Disable();
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
            hud.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
    }


    private void ResumeGame() {
        isPaused = false;
        pauseMenu.SetActive(false);
        inputSystem.Player.Enable();
        Time.timeScale = 1f;
        hud.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void ToMainMenu() {
        SceneManager.LoadScene(0);
    }

    private void ExitGame() {
        Application.Quit();
    }
    
    
    
}
