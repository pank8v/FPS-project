using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   
   private InputSystem_Actions inputSystem;
   [SerializeField] private PlayerHealth playerHealth;
   [SerializeField] private GameObject gameOverMenu;
   public static bool isPaused;

   public static GameManager Instance { get; private set; }
   public event Action<bool> OnPauseStateChanged;
   
   private void OnEnable() {
      playerHealth.OnPlayerDeath += HandleGameOver;
   }
   
   private void OnDisable() {
      playerHealth.OnPlayerDeath -= HandleGameOver;
   }
   
   private void Awake() {
      if (Instance == null) {
         Instance = this;
      }
      else {
         Destroy(gameObject);
      }
   }

   private void Start() {
      inputSystem = new InputSystem_Actions();
   }

   private void HandleGameOver() {
      inputSystem.Player.Disable();
      Time.timeScale = 0f; 
      Cursor.lockState = CursorLockMode.None;
      Cursor.visible = true;
      gameOverMenu.SetActive(true);
   }
   
   
   public void TogglePauseMenu() {
      Debug.Log("toggle");
      isPaused = !isPaused;
      if (isPaused)
         PauseGame();
      else
         ResumeGame();
   }
   
   
   public void PauseGame() {
      inputSystem.Player.Disable();
      Time.timeScale = 0f;
      OnPauseStateChanged?.Invoke(true);
      Cursor.lockState = CursorLockMode.None;
      Cursor.visible = true;
   }


   public void ResumeGame() {
      isPaused = false;
      inputSystem.Player.Enable();
      Time.timeScale = 1f;
      OnPauseStateChanged?.Invoke(false);
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
   }

   public void RestartGame() {
      Time.timeScale = 1f;
      Scene currentScene = SceneManager.GetActiveScene();
      SceneManager.LoadScene(currentScene.buildIndex);
     
   }
   
   
   public void ToMainMenu() {
      isPaused = false;
      inputSystem.Player.Enable();
      Time.timeScale = 1f;
      SceneManager.LoadScene(0);
   }

   public void ExitGame() {
      Application.Quit();
   }

   
   
}
