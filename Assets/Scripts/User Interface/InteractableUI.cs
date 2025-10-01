using TMPro;
using UnityEngine;

public class InteractableUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI interactableText;
    
    public void UpdateText(IInteractable interactable) {
        if (interactable != null) {
            interactableText.gameObject.SetActive(true);
            interactableText.text = interactable.InteractionText;
        }
        else {
            interactableText.gameObject.SetActive(false);
        }
    }

    private void Start() {
        interactableText.text = "";
    }
}
