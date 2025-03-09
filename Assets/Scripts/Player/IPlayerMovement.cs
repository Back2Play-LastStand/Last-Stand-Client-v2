using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player Move Interface
/// </summary>
public interface IPlayerMovement
{
    public void Move(Vector2 inputVec); // Move
    public void MoveTo(Vector3 des); // Move to spesific location
    public void LookAtMouse(Vector2 inputMousePos); // Look Mouse
}
