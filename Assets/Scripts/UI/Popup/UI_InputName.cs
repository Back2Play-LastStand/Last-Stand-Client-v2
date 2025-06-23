using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InputName : UI_Popup
{
    enum GameObjects
    {
        InputName,
        NameButton,
        CheckButton,
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GetObject((int)GameObjects.NameButton).AddUIEvent((PointEventData) => { return; });
        GetObject((int)GameObjects.CheckButton).AddUIEvent((PointEventData) => { return; });
    }
}
