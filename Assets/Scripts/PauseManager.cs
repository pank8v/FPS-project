using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PauseManager : MonoBehaviour
{
    private InputSystem_Actions inputSystem;
    [SerializeField] private PlayerInputHandler playerInputHandler;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject hud;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button exitButton;
    private bool isPaused;
    
    private void Start() {
        DOTween.defaultTimeScaleIndependent = true;
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
            pauseMenu.SetActive(true);
            inputSystem.Player.Disable();
            pauseMenu.transform.localScale = Vector3.zero;
            pauseMenu.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
            Time.timeScale = 0f;
            hud.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
          //  Cursor.visible = true;
    }


    private void ResumeGame() {
        isPaused = false;
        pauseMenu.transform.DOScale(Vector3.zero, 0.1f).SetEase(Ease.InBack)
            .OnComplete(() => pauseMenu.SetActive(false));

      //  pauseMenu.SetActive(false);
        inputSystem.Player.Enable();
        Time.timeScale = 1f;
        hud.SetActive(true);
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
