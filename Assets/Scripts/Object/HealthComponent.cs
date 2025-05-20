using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IDamageReceiver
{
    public uint startHealth = 100;
    [field: SerializeField]
    public uint Health
    {
        get; protected set;
    }

    protected virtual void Start()
    {
        Health = startHealth;
    }

    public void GetDamage(IDamage damage)
    {
        Health -= damage.Amount;
    }
    public void SetHealth(uint health)
    {
        Health = health;
    }
}
