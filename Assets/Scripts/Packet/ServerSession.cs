using Google.Protobuf;
using Protocol;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace ServerCore
{
    public class ServerSession : PacketSession
    {
        private string[] nameList = {
        "Axel", "Blaze", "Caden", "Derek", "Elias", "Finn", "Gage", "Hunter", "Ivy", "Jade",
        "Kai", "Liam", "Maya", "Nina", "Oscar", "Piper", "Quinn", "Ryder", "Sage", "Tess"
        };

        public override void OnConnected(EndPoint endPoint)
        {
            Debug.Log($"OnConnected : {endPoint}");

            Managers.Packet.CustomHandler = (s, m, i) =>
            {
                PacketQueue.Instance.Push(i, m);
            };

            Protocol.REQ_ENTER pkt = new();
            System.Random random = new System.Random();
            int index = random.Next(nameList.Length);
            pkt.Name = nameList[index];
            Send(pkt, (ushort)PacketId.PKT_REQ_ENTER);
        }

        public void Send(IMessage packet, ushort id)
        {
            ushort size = (ushort)packet.CalculateSize();
            byte[] sendBuffer = new byte[size + 4];
            Array.Copy(BitConverter.GetBytes((ushort)(size + 4)), 0, sendBuffer, 0, sizeof(ushort));
            Array.Copy(BitConverter.GetBytes((ushort)id), 0, sendBuffer, 2, sizeof(ushort));
            Array.Copy(packet.ToByteArray(), 0, sendBuffer, 4, size);
            //Debug.Log($"send packet : {BitConverter.ToString(sendBuffer)}");
            Send(new ArraySegment<byte>(sendBuffer));
        }

        public override void OnDisconnected(EndPoint endPoint)
        {
            Debug.Log($"OnDisConnected : {endPoint}");
        }

        public override void OnRecvPacket(ArraySegment<byte> buffer)
        {
            Managers.Packet.OnRecvPacket(this, buffer);
        }

        public override void OnSend(int numOfBytes)
        {
            //Debug.Log($"SendPacket : {numOfBytes}");
        }
    }
}