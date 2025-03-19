using Google.Protobuf;
using Protocol;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using ServerCore;
using System.Collections.Generic;
using System;

public class NetworkManager : MonoBehaviour
{
    ServerSession _session = new ServerSession();
    Connector _connector = new Connector();
    IPAddress _ipAddr;
    IPEndPoint _ipEndPoint;

    public void Init(int port)
    {
        string host = Dns.GetHostName();
        IPHostEntry ipHost = Dns.GetHostEntry(host);
        for (int i = 0; ipHost.AddressList.Length > i; i++)
        {
            if (ipHost.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
            {
                _ipAddr = ipHost.AddressList[i];
                break;
            }
        }

        _ipEndPoint = new IPEndPoint(_ipAddr, port);

        _connector.Connect(_ipEndPoint,
            () => { return _session; });
    }

    public void Update()
    {
        List<PacketMessage> list = PacketQueue.Instance.PopAll();
        foreach (PacketMessage packet in list)
        {
            Action<PacketSession, IMessage> handler = Managers.Packet.GetPacketHandler(packet.Id);
            if (handler != null)
                handler.Invoke(_session, packet.Message);
        }
    }

    public void Send(IMessage packet, ushort id)
    {
        _session.Send(packet, id);
    }
    
    void OnApplicationQuit()
    {
        _connector.Disconnect();
    }
}