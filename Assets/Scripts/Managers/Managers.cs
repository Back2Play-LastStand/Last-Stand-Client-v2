using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers instance;
    public static Managers Instance { get { Init(); return instance; } }

    #region Contents
    NetworkManager _network = new();
    PacketManager _packet = new();
    ObjectManager _object = new();

    public static NetworkManager Network { get { return Instance._network; } }
    public static PacketManager Packet { get { return Instance._packet; } }
    public static ObjectManager Object { get { return Instance._object; } }
    #endregion
    #region Cores
    PoolManager _pool = new();
    ResourceManager _resource = new();

    public static PoolManager Pool { get { return Instance._pool; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    #endregion

    void Start()
    {
        Init();
    }
    void Update()
    {
        instance._network.Update();
    }

    public static void Init()
    {
        if (instance == null)
        {
            GameObject go = new GameObject { name = "@Managers" };
            go.AddComponent<Managers>();

            DontDestroyOnLoad(go);
            instance = go.GetComponent<Managers>();

            instance._network.Init(3333);
        }
    }
}
