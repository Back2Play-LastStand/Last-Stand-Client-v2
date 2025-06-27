using Protocol;
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
        GetObject((int)GameObjects.StartGame).AddUIEvent((PointerEventData) => { StartGame(); });
        GetObject((int)GameObjects.ExitRoom).AddUIEvent((PointerEventData) => { ClosePopupUI(); });
    }

    [field: SerializeField]
    public GameObject[] PlayerSlots = new GameObject[4];

    void StartGame()
    {
        var lobby = Managers.Scene.CurrentScene.GetComponent<LobbyScene>();
        lobby.TurnGameScene();
    }

    public void SetPlayer(RES_ENTER_GAMEROOM enter)
    {
        foreach (var slot in PlayerSlots)
            slot.SetActive(false);

        for (int i = 0; i < enter.Players.Count && i < PlayerSlots.Length; i++)
        {
            PlayerSlots[i].SetActive(true);
            PlayerSlots[i].GetComponentInChildren<Text>().text = enter.Players[i].Name;
        }
    }
}
