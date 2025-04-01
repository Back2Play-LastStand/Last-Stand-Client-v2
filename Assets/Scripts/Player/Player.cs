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
            _destPos = new Vector3(value.PosX, transform.position.y, value.PosY);
            transform.position = _destPos;
            //UpdateAnim();
        }
    }

    public Vector3 VectorPos
    {
        get { return new Vector3(PosInfo.PosX, transform.position.y, PosInfo.PosY); }
        set
        {
            PosInfo.PosX = value.x;
            PosInfo.PosY = value.z;
        }
    }

    protected PlayerMovement m_playerMovement;
    protected PlayerShoot m_playerShoot;

    protected Vector3 _destPos;

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
        UpdateMovement();
    }
}
