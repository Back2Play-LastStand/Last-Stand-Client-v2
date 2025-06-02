using Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Respawn : UI_Popup
{
    enum GameObjects
    {
        RespawnImage,
        RespawnText,
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GetObject((int)GameObjects.RespawnImage).AddUIEvent((PointerEventData) => { RespawnPlayer(); });
        GetObject((int)GameObjects.RespawnText).GetComponent<Text>().text = "RESPAWN?";
    }

    public void RespawnPlayer()
    {
        Debug.Log("RespawnPlayer");
    }
}
