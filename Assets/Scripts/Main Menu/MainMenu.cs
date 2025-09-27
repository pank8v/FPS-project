using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   [SerializeField] private Button playButton;
   [SerializeField] private Button exitButton;

   private void Start() {
      playButton.onClick.AddListener(StartGame);
      exitButton.onClick.AddListener(ExitGame);
   }

   private void StartGame() {
      SceneManager.LoadScene(1);
   }

   private void ExitGame() {
      Application.Quit();
   }
}
