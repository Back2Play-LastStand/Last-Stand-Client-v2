using Google.Protobuf;
using Protocol;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using ServerCore;

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
}