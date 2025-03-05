using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerInput m_playerInput;
    private PlayerMovement m_playerMovement;

    void Awake()
    {
        m_playerInput = GetComponent<PlayerInput>();
        m_playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        m_playerMovement.Move(m_playerInput.InputVec);
        m_playerMovement.LookAtMouse(m_playerInput.MousePos);
    }
}
