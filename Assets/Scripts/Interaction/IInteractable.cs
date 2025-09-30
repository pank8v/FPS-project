using UnityEngine;

public interface IInteractable
{
    public string InteractionText { get; }
    public void Interact(IInteractor interactor);
}
