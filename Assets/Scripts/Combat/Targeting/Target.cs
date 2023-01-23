using System;
using UnityEngine;

public class Target : MonoBehaviour
{
    // Public events
    public event Action<Target> OnDestroyed;

    // Unity built-in methods
    private void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
    }
}