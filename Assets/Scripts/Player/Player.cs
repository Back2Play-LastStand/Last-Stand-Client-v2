using Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public ulong Id { get; set; }

    PositionInfo _positionInfo = new PositionInfo();
    public PositionInfo PosInfo
    {
        get { return _positionInfo; }
        set
        {
            if (_positionInfo.Equals(value))
                return;

            _positionInfo = value;
            //UpdateAnim();
        }
    }

    public Vector2 VectorPos
    {
        get { return new Vector2(PosInfo.PosX, PosInfo.PosY); }
        set
        {
            PosInfo.PosX = (int)value.x;
            PosInfo.PosY = (int)value.y;
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
    }

    protected virtual void UpdateMovement()
    {
        m_playerMovement.Move(VectorPos);
    }
    protected virtual void UpdateRotation()
    {
        Vector2 vec = VectorPos;
        if (vec != Vector2.zero)
        {
            Vector3 lookDir = new Vector3(vec.x, 0, vec.y);
            transform.rotation = Quaternion.LookRotation(lookDir);
        }
    }
    protected virtual void UpdateAttack()
    {
        m_playerShoot.Attack();
    }
    protected virtual void UpdateAnim()
    {
        //m_playerMovement.PlayAnim(new Vector2(PosInfo.PosX, PosInfo.PosY));
        //m_playerMovement.PlayAnim(VectorPos);
    }

    protected virtual void Update()
    {
    }
}
