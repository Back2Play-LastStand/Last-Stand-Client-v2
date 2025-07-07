using Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Creature
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

    public SOItemDropTable soMon;

    protected override void Start()
    {
        base.Start();
    }

    public override void Die()
    {
        base.Die();

        soMon.ItemDrop(transform.position);
    }
}