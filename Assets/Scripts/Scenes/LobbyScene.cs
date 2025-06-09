using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        Managers.UI.ShowSceneUI<UI_Lobby>();
    }

    public void TurnGameScene()
    {
        Managers.Scene.LoadScene(Define.Scene.Game);
    }

    public override void Clear()
    {
        
    }
}
