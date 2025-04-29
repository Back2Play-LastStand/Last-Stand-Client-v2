using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageReceiver
{
    public void GetDamage(IDamage damage);
}

public interface IDamage
{
    public uint Amount { get; }

    public void ApplyDamage(IDamageReceiver receiver) { }
}