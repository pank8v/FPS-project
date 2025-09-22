using UnityEngine;

public interface IHealProvider
{
    public void AddHeal(int amount);
    public void UseHeal();
}
