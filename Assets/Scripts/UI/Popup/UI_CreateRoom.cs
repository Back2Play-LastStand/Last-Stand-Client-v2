using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CreateRoom : UI_Popup
{
    enum GameObjects
    {
        RoomPanel,
        Player1Panel,
        Player2Panel,
        Player3Panel,
        Player4Panel,
        StartGame,
        ExitRoom,
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GetObject((int)GameObjects.RoomPanel).GetComponentInChildren<Text>().text = Managers.UI.m_lobby.RoomName;
        GetObject((int)GameObjects.Player1Panel).GetComponentInChildren<Text>().text = Managers.UI.m_inputName.name;
        GetObject((int)GameObjects.Player2Panel).gameObject.SetActive(false);
        GetObject((int)GameObjects.Player3Panel).gameObject.SetActive(false);
        GetObject((int)GameObjects.Player4Panel).gameObject.SetActive(false);
        GetObject((int)GameObjects.StartGame).AddUIEvent((PointerEventData) => { StartGame(); });
        GetObject((int)GameObjects.ExitRoom).AddUIEvent((PointerEventData) => { ClosePopupUI(); });
    }

    void StartGame()
    {
        var lobby = Managers.Scene.CurrentScene.GetComponent<LobbyScene>();
        lobby.TurnGameScene();
    }
}
