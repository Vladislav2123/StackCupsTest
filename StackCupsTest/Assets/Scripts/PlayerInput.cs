using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public event Action OnStartTap;
    public event Action OnEndTap;

    void Update()
    {

        if (Input.GetMouseButtonDown(0)) OnStartTap?.Invoke();
        if (Input.GetMouseButtonUp(0)) OnEndTap?.Invoke();
    }
}
