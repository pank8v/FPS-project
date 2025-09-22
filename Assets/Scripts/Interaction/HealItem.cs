using UnityEngine;

public class HealItem : IUsable
{
    public bool Use(GameObject interactor) {
       var health =  interactor.GetComponent<PlayerHealth>();
       if (health != null) {
        return health.AddHealth(20);
       }

       return false;
    }
}
