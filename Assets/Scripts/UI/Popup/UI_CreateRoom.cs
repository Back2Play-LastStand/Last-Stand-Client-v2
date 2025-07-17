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
        StartGameButton = GetObject((int)GameObjects.StartGame);
        StartGameButton.AddUIEvent((PointerEventData) => { StartGame(); });
        GetObject((int)GameObjects.ExitRoom).AddUIEvent((PointerEventData) => { ClosePopupUI(); });
    }

    [field: SerializeField]
    public GameObject[] PlayerSlots = new GameObject[5];
    GameObject StartGameButton = null;

    void StartGame()
    {
        REQ_ENTER_ROOM enterRoomPacket = new();
        enterRoomPacket.Name = Managers.UI.m_lobby.RoomName;
        Managers.Network.Send(enterRoomPacket, (ushort)PacketId.PKT_REQ_ENTER_ROOM);
    }

    public void SetPlayer(RES_ENTER_GAMEROOM enter)
    {
        for (int i = 0; i < PlayerSlots.Length; i++)
        {
            var slot = PlayerSlots[i];

            if (slot.TryGetComponent<CanvasGroup>(out var cg))
            {
                cg.alpha = 0f;
                cg.interactable = false;
                cg.blocksRaycasts = false;
            }
        }

        for (int i = 0; i < enter.Players.Count; i++)
        {
            GameObject slot = PlayerSlots[i];

            // 텍스트 설정
            Text nameText = slot.GetComponentInChildren<Text>();
            if (nameText != null)
                nameText.text = enter.Players[i].Name;

            // 알파값 켜기
            if (slot.TryGetComponent<CanvasGroup>(out var cg))
            {
                cg.alpha = 1f;
                cg.interactable = true;
                cg.blocksRaycasts = true;
            }
        }

        StartGameButton.SetActive(enter.IsCreate);
    }

    public void SetPlayerAll(RES_ENTER_GAMEROOM_ALL enter)
    {
        for (int i = 0; i < PlayerSlots.Length; i++)
        {
            GameObject slot = PlayerSlots[i];

            // 이미 사용 중인 슬롯이면 넘김 (알파 > 0 or 활성화)
            if (slot.TryGetComponent<CanvasGroup>(out var cg))
            {
                if (cg.alpha > 0.9f) // 충분히 보이는 상태면 사용 중으로 간주
                    continue;

                // 비어 있는 슬롯 발견 → 여기 설정
                Text nameText = slot.GetComponentInChildren<Text>();
                if (nameText != null)
                    nameText.text = enter.Players[i].Name;

                cg.alpha = 1f;
                cg.interactable = true;
                cg.blocksRaycasts = true;

                return;
            }
        }
    }
}
