using UnityEngine;

public class Barrel: MonoBehaviour, IInteractable
{
    public void Interact(IInteractor interactor) {
        Debug.Log("Barrel Interact");
    }
}
