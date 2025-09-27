using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private InputSystem_Actions inputSystem;
    [SerializeField] private PlayerInputHandler playerInputHandler;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button exitButton;
    private bool isPaused;
    
    private void Start() {
        inputSystem = new InputSystem_Actions();
        resumeButton.onClick.AddListener(ResumeGame);
        mainMenuButton.onClick.AddListener(ToMainMenu);
        exitButton.onClick.AddListener(ExitGame);
    }


    private void Update() {
        if (playerInputHandler.pauseTriggered) {
            PauseGame();
        }
        else {
            ResumeGame();
        }
    }

    private void PauseGame() {
            pauseMenu.SetActive(true);
            inputSystem.Player.Disable();
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
          //  Cursor.visible = true;
    }


    private void ResumeGame() {
        pauseMenu.SetActive(false);
        inputSystem.Player.Enable();
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    private void ToMainMenu() {
        SceneManager.LoadScene(0);
    }

    private void ExitGame() {
        Application.Quit();
    }
    
    
    
}
