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

        GetObject((int)GameObjects.CreateButton).AddUIEvent((PointerEventData) => { CreateGame(); });
        GetObject((int)GameObjects.JoinButton).AddUIEvent((PointerEventData) => { JoinGame(); });
    }

    public string RoomName { get; protected set; }

    public void CreateGame()
    {
        InputField input = GetObject((int)GameObjects.RoomNameInput).GetComponent<InputField>();
        if (input.text == null)
        {
            Debug.Log("Room Name is null");
            return;
        }
        else
        {
            Debug.Log("CreateGame!!");
            RoomName = input.text;
            JoinRoom();
        }
    }
    public void JoinGame()
    {
        InputField input = GetObject((int)GameObjects.RoomNameInput).GetComponent<InputField>();
        if (input.text == null)
        {
            Debug.Log("Room Name is null");
            return;
        }
        else
        {
            Debug.Log("JoinGame!!");
            RoomName = input.text;
            JoinRoom();
        }
    }
    public void JoinRoom()
    {
        REQ_ENTER_ROOM enterRoomPacket = new();
        enterRoomPacket.Name = RoomName;
        Managers.Network.Send(enterRoomPacket, (ushort)PacketId.PKT_REQ_ENTER_ROOM);
    }
}
