using Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;
        REQ_ENTER_ROOM enterRoomPacket = new();
        enterRoomPacket.Name = Managers.UI.m_lobby.RoomName;
        Managers.Network.Send(enterRoomPacket, (ushort)PacketId.PKT_REQ_ENTER_ROOM);
    }

    public override void Clear()
    {
    }
}
