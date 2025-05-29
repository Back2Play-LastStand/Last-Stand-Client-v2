using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : UI_Base
{
    enum GameObjects
    {
        HealthBar
    }

    Creature m_creature;

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        m_creature = transform.parent.GetComponent<Creature>();
    }

    private void Update()
    {
        Transform parent = transform.parent;
        transform.position = parent.position + new Vector3(0, 0.1f, -1);
        transform.rotation = Camera.main.transform.rotation;

        float ratio = m_creature.Health / (float)m_creature.maxHealth;
        SetHealthRatio(ratio);
    }

    public void SetHealthRatio(float ratio)
    {
        GetObject((int)GameObjects.HealthBar).GetComponent<Slider>().value = ratio;
    }
}
