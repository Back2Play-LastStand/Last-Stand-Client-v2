using Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Lobby : UI_Popup
{
    enum GameObjects
    {
        RoomNameInput,
        CreateButton,
        JoinButton,
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GetObject((int)GameObjects.CreateButton).AddUIEvent((PointerEventData) => { EnterGameRoom(true); });
        GetObject((int)GameObjects.JoinButton).AddUIEvent((PointerEventData) => { EnterGameRoom(false); });
    }

    public string RoomName { get; protected set; }
    public bool IsCreate { get; protected set; }

    public void EnterGameRoom(bool isCreate)
    {
        InputField input = GetObject((int)GameObjects.RoomNameInput).GetComponent<InputField>();
        if (input.text == null)
        {
            Debug.Log("Room Name is null");
            return;
        }
        else
        {
            if (isCreate)
            {
                Debug.Log("CreateGame!!");
                IsCreate = true;
            }
            else
            {
                Debug.Log("JoinGame!!");
                IsCreate = false;
            }
            RoomName = input.text;
            JoinRoom();
        }
    }
    public void JoinRoom()
    {
        REQ_ENTER_GAMEROOM enter = new()
        {
            Name = RoomName,
            IsCreate = IsCreate,
        };
        Managers.Network.Send(enter, (ushort)PacketId.PKT_REQ_ENTER_GAMEROOM);
    }
}
