using UnityEngine;

public class RangeWeapon : Weapon
{
    protected override void Shoot() {
        Debug.Log("Fire");
    }

    protected override void Reload() {
        Debug.Log("Reload");
    }
}
