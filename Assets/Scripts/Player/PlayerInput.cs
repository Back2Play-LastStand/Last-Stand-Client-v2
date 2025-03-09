using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour, IPlayerInput
{
    private Vector2 m_inputVec;
    public Vector2 InputVec => m_inputVec;

    private Vector2 m_mousePos;
    public Vector2 MousePos => m_mousePos;

    public void OnMove(InputValue value)
    {
        m_inputVec = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        m_mousePos = value.Get<Vector2>();
    }
}
