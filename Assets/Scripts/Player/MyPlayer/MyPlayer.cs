using Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayer : Player
{
    protected PlayerInput m_playerInput;

    protected override void Init()
    {
        base.Init();
        m_playerInput = GetComponent<PlayerInput>();
    }

    protected override void Update()
    {
        base.Update();

        UpdateMovement();
        UpdateAnim();
        UpdateRotation();

        if (m_playerInput.MouseClick)
            UpdateAttack();
    }

    protected override void UpdateMovement()
    {
        Vector2 vec = m_playerInput.InputVec;
        m_playerMovement.Move(vec);

        if (m_playerInput.InputVec != Vector2.zero)
        {
            PosInfo.PosX += (int)vec.x;
            PosInfo.PosY += (int)vec.y;

            REQ_MOVE move = new();
            move.Info = PosInfo;
            Managers.Network.Send(move, (ushort)PacketId.PKT_REQ_MOVE);
        }
    }
    protected override void UpdateRotation()
    {
        Vector2 vec = m_playerInput.InputVec;
        if (vec != Vector2.zero)
        {
            Vector3 lookDir = new Vector3(vec.x, 0, vec.y);
            transform.rotation = Quaternion.LookRotation(lookDir);
        }
    }
    protected override void UpdateAttack()
    {
        m_playerShoot.Attack();
    }
    protected override void UpdateAnim()
    {
        Vector2 inputVec = m_playerInput.InputVec;

        if (inputVec == Vector2.zero)
        {
            m_playerMovement.PlayAnim(Vector2.zero);
            return;
        }

        Vector3 worldDir = new Vector3(inputVec.x, 0, inputVec.y);
        Vector3 localDir = transform.InverseTransformDirection(worldDir);
        Vector2 animDir = new Vector2(localDir.x, localDir.z);

        m_playerMovement.PlayAnim(animDir);
    }
}
