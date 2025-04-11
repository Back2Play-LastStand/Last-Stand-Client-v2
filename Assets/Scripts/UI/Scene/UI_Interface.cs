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

    enum RawImages
    {
        Minimap,
    }

    enum Texts
    {
        AmmoText,
    }

    Transform m_target;
    Camera m_minimapCamera;

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Image>(typeof(Images));
        Bind<Text>(typeof(Texts));
        Bind<RawImage>(typeof(RawImages));

        GetImage((int)Images.GunImage);
        GetImage((int)Images.MinimapImage);
        RawImage minimap = Get<RawImage>((int)RawImages.Minimap);

        m_target = Managers.Object.MyPlayer.transform;
        GameObject go = Managers.Resource.Instantiate("MinimapCamera");
        m_minimapCamera = go.GetComponent<Camera>();
        RenderTexture rt = new RenderTexture(256, 256, 16);
        m_minimapCamera.targetTexture = rt;
        minimap.texture = rt;

        StartCoroutine(UpdateMinimap());
    }

    IEnumerator UpdateMinimap()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            Vector3 pos = m_target.position;
            pos.y = m_minimapCamera.transform.position.y;
            m_minimapCamera.transform.position = pos;
        }
    }

    public void SetRemainAmmoText(int ammo)
    {
        GetText((int)Texts.AmmoText).GetComponent<Text>().text = ammo.ToString();
    }
}
