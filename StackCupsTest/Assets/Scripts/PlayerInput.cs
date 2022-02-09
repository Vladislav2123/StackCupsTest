using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public event Action OnClick;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) OnClick?.Invoke();
    }
}
