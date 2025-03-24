using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayer : Player
{
    protected PlayerInput m_playerInput;

    protected override void Awake()
    {
        base.Awake();
        m_playerInput = GetComponent<PlayerInput>();
    }

    protected override void Update()
    {
        base.Update();

        m_playerMovement.Move(m_playerInput.InputVec);
        m_playerMovement.LookAtMouse(m_playerInput.MousePos);
        m_playerMovement.PlayAnim(m_playerInput.InputVec);
        m_playerAim.AimTowardsMouse(m_playerInput.MousePos);

        if (m_playerInput.MouseClick)
            m_playerShoot.Attack();
    }
}
