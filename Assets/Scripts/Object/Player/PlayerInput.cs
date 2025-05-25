using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour, IPlayerInput
{
    private Vector2 m_inputVec;
    public Vector2 InputVec => m_inputVec;

    private bool m_mouseClick;
    public bool MouseClick => m_mouseClick;

    private bool m_reloadButton;
    public bool ReloadButton => m_reloadButton;

    public void OnMove(InputValue value)
    {
        m_inputVec = value.Get<Vector2>();
    }

    public void OnFire(InputValue value)
    {
        m_mouseClick = value.isPressed;
    }

    public void OnReload(InputValue value)
    {
        m_reloadButton = value.isPressed;
    }
}
