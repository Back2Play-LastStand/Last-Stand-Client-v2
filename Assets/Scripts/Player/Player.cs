using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public ulong Id { get; set; }
    protected PlayerMovement m_playerMovement;
    protected PlayerAim m_playerAim;
    protected PlayerShoot m_playerShoot;

    protected virtual void Awake()
    {
        m_playerMovement = GetComponent<PlayerMovement>();
        m_playerShoot = GetComponent<PlayerShoot>();
        m_playerAim = GetComponent<PlayerAim>();
    }

    public virtual void SetMovement(Vector3 movement)
    {
        m_playerMovement.Move(movement);
    }

    public virtual void SetRotation(Vector3 lookAtPosition)
    {
        m_playerMovement.LookAtMouse(lookAtPosition);
    }

    public virtual void SetShoot(bool isShooting)
    {
        if (isShooting)
            m_playerShoot.Attack();
    }

    protected virtual void Update()
    {

    }
}
