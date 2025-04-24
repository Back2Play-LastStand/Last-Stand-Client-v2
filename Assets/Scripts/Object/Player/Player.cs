using Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Creature
{
    protected MoveDir _lastDir = MoveDir.Down;
    public MoveDir Dir
    {
        get { return PosInfo.MoveDir; }
        set
        {
            if (PosInfo.MoveDir == value)
                return;

            PosInfo.MoveDir = value;
            if (value != MoveDir.None)
                _lastDir = value;
        }
    }

    protected PlayerMovement m_playerMovement;
    protected PlayerShoot m_playerShoot;

    protected virtual void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        m_playerMovement = GetComponent<PlayerMovement>();
        m_playerShoot = GetComponent<PlayerShoot>();
        _destPos = transform.position;
    }

    protected virtual void UpdateMovement()
    {
        Vector3 currentPos = transform.position;
        if (currentPos != _destPos)
        {
            Vector3 moveDir = _destPos - currentPos;
            float dist = moveDir.magnitude;
            float moveStep = m_playerMovement.m_speed * Time.deltaTime;

            if (dist < moveStep)
            {
                transform.position = _destPos;
            }
            else
            {
                transform.position += moveDir.normalized * moveStep;
            }
        }

        UpdateAnim();
    }
    protected virtual void UpdateRotation()
    {
    }
    protected virtual void UpdateAttack()
    {
        m_playerShoot.Attack();
    }
    protected virtual void UpdateAnim()
    {
        if (m_playerMovement == null) return;

        switch (Dir)
        {
            case MoveDir.Up:
                m_playerMovement.PlayAnim(new Vector2(0, 1));
                transform.rotation = Quaternion.Euler(0, 0, 0);
                Debug.Log("Move Up");
                break;
            case MoveDir.Down:
                m_playerMovement.PlayAnim(new Vector2(0, -1));
                transform.rotation = Quaternion.Euler(0, 180, 0);
                Debug.Log("Move Down");
                break;
            case MoveDir.Left:
                m_playerMovement.PlayAnim(new Vector2(-1, 0));
                transform.rotation = Quaternion.Euler(0, -90, 0);
                Debug.Log("Move Left");
                break;
            case MoveDir.Right:
                m_playerMovement.PlayAnim(new Vector2(1, 0));
                transform.rotation = Quaternion.Euler(0, 90, 0);
                Debug.Log("Move Right");
                break;
            default:
                m_playerMovement.PlayAnim(Vector2.zero);
                Debug.Log("Move None");
                break;
        }
    }

    protected virtual void Update()
    {
        UpdateMovement();
    }
}
