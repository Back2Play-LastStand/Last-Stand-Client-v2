using Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
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
        }
    }

    public Vector3 VectorPos
    {
        get { return new Vector3(PosInfo.PosX, transform.position.y, PosInfo.PosY); }
        set
        {
            if (PosInfo.PosX == value.x && PosInfo.PosY == value.y)
                return;

            PosInfo.PosX = value.x;
            PosInfo.PosY = value.z;
        }
    }

    protected Vector3 _destPos;
}
