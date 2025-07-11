using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        Managers.UI.m_lobby = Managers.UI.ShowPopupUI<UI_Lobby>();
        Managers.Network.CoonectServer();

        if (Managers.UI.m_isNewAccount)
        {
            Managers.UI.ShowPopupUI<UI_InputName>();
        }
        else
        {
            Protocol.REQ_ENTER pkt = new()
            {
                Name = Managers.UI.m_playerName,
            };
            Managers.Network.Send(pkt, (ushort)PacketId.PKT_REQ_ENTER);
        }
    }

    public void TurnGameScene()
    {
        Managers.Scene.LoadScene(Define.Scene.Game);
    }

    public override void Clear()
    {
        Managers.UI.ClosePopupUI();
    }
}
