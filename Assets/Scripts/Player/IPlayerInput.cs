using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Player Input Interface
/// </summary>
public interface IPlayerInput
{
    public Vector2 InputVec { get; }
    public Vector2 MousePos { get; }

    void OnMove(InputValue value);
    void OnLook(InputValue value);
}
