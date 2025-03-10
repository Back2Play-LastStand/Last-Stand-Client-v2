using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerAnim
{
    public Animator Anim { get; }

    public void PlayAnim(Vector2 inputVec);
}
