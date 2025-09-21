using UnityEngine;

public class Barrel: MonoBehaviour, IInteractable
{
    public void Interact(GameObject interactor) {
        Debug.Log("Barrel Interact");
    }
}
