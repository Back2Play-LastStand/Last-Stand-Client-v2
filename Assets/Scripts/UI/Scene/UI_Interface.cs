using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Interface : UI_Scene
{
    enum Images
    {
        GunImage,
        MinimapImage,
    }

    enum Texts
    {
        AmmoText,
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Image>(typeof(Images));
        Bind<Text>(typeof(Texts));

        GetImage((int)Images.GunImage);
        GetImage((int)Images.MinimapImage);
    }

    public void SetRemainAmmoText(int ammo)
    {
        GetText((int)Texts.AmmoText).GetComponent<Text>().text = ammo.ToString();
    }
}
