using UnityEngine;

public class Door : MonoBehaviour,IInteractable
{
    private bool isOpened = false;
    
    public void Interact(IInteractor interactor) {
        if (!isOpened) {
            transform.localPosition += new Vector3(0, 4f, 0f);
            isOpened = true;

        }
        else {
            transform.localPosition -= new Vector3(0, 4f, 0f);
            isOpened = false;
        }
    }
}
