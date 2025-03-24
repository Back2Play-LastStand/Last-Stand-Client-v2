using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursor : MonoBehaviour
{
    [SerializeField]
    private Texture2D m_cursorImg;

    void Awake()
    {
        Managers manager = Managers.Instance;
    }
    void Start()
    {
        Cursor.SetCursor(m_cursorImg, Vector2.zero, CursorMode.ForceSoftware);
    }
}
