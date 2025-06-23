using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ChangePassword : UI_Popup
{
    enum GameObjects
    {
        InputPassword,
        InputRetry,
        CheckButton,
        BackButton,
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GetObject((int)GameObjects.CheckButton).AddUIEvent((PointerEventData) => { return; });
        GetObject((int)GameObjects.BackButton).AddUIEvent((PointerEventData) => { ClosePopupUI(); });
    }
}
