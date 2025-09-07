using UnityEngine;
using System;
public interface IReloadable
{
    event Action OnReload;
    void Reload();
}
