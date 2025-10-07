using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler playerInputHandler;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject hud;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button exitButton;

    
    private void OnDisable() {
        playerInputHandler.OnPause -= GameManager.Instance.TogglePauseMenu;
        GameManager.Instance.OnPauseStateChanged -= UpdateUI;

    }
    

    private void Start() {
        if (GameManager.Instance != null) {
            GameManager.Instance.OnPauseStateChanged += UpdateUI;
            playerInputHandler.OnPause += GameManager.Instance.TogglePauseMenu;
        }
        resumeButton.onClick.AddListener(GameManager.Instance.ResumeGame);
        mainMenuButton.onClick.AddListener(GameManager.Instance.ToMainMenu);
        exitButton.onClick.AddListener(GameManager.Instance.ExitGame);
    }
    
    
    
    private void UpdateUI(bool isPaused) {
         pauseMenu.SetActive(isPaused);
         hud.SetActive(!isPaused);
    }
    
  




 

 


    
    
}
