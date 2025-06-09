using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        Managers.UI.ShowSceneUI<UI_Lobby>();
        Managers.Network.CoonectServer();

        Protocol.REQ_ENTER pkt = new()
        {
            Name = "Name",
        };
        Managers.Network.Send(pkt, (ushort)PacketId.PKT_REQ_ENTER);
    }

    public void TurnGameScene()
    {
        Managers.Scene.LoadScene(Define.Scene.Game);
    }

    public override void Clear()
    {
        
    }
}
