using UnityEngine;
using UnityEngine.UI;
public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    
    private void Start() {
        restartButton.onClick.AddListener(GameManager.Instance.RestartGame);
    }
}
