using UnityEngine;

public class HealItem : IUsable
{
    private float healAmount = 20f;
    public bool Use(IInteractor interactor) {
       if ( interactor.PlayerHealth != null) {
        return interactor.PlayerHealth.AddHealth(healAmount);
       }

       return false;
    }
}
