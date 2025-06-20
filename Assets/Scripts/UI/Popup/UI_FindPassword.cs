using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_FindPassword : UI_Popup
{
    enum GameObjects
    {
        InputId,
        InputEmail,
        InputVerify,
        VerifyButton,
        SendButton,
        BackButton
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GetObject((int)GameObjects.SendButton).AddUIEvent((PointerEventData) => { return; });
        GetObject((int)GameObjects.VerifyButton).AddUIEvent((PointerEventData) => { return; });
        GetObject((int)GameObjects.BackButton).AddUIEvent((PointerEventData) => { ClosePopupUI(); });
    }
}
