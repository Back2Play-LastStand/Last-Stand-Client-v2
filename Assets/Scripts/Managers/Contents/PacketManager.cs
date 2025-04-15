using Google.Protobuf;
using ServerCore;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PacketId : ushort
{
    PKT_REQ_ENTER = 1000,
    PKT_RES_ENTER = 1001,
    PKT_REQ_ENTER_ROOM = 1002,
    PKT_RES_ENTER_ROOM = 1003,
    PKT_REQ_LEAVE = 1004,
    PKT_RES_LEAVE = 1005,
    PKT_RES_SPAWN = 1006,
    PKT_RES_SPAWN_ALL = 1007,
    PKT_RES_DESPAWN = 1008,
    PKT_REQ_MOVE = 1009,
    PKT_RES_MOVE = 1010,
    PKT_RES_SPAWN_MONSTER = 1011,
}

public class PacketManager : MonoBehaviour
{
    Dictionary<ushort, Action<PacketSession, ArraySegment<byte>, ushort>> _onRecv = new Dictionary<ushort, Action<PacketSession, ArraySegment<byte>, ushort>>();
    Dictionary<ushort, Action<PacketSession, IMessage>> _handler = new Dictionary<ushort, Action<PacketSession, IMessage>>();

    public Action<PacketSession, IMessage, ushort> CustomHandler { get; set; }

    public PacketManager()
    {
        Register();
    }

    public void Register()
    {
        _onRecv.Add((ushort)PacketId.PKT_RES_ENTER, MakePacket<Protocol.RES_ENTER>);
        _handler.Add((ushort)PacketId.PKT_RES_ENTER, PacketHandler.ResEnterHandler);
        _onRecv.Add((ushort)PacketId.PKT_RES_LEAVE, MakePacket<Protocol.RES_LEAVE>);
        _handler.Add((ushort)PacketId.PKT_RES_LEAVE, PacketHandler.ResLeaveHandler);
        _onRecv.Add((ushort)PacketId.PKT_RES_ENTER_ROOM, MakePacket<Protocol.RES_ENTER_ROOM>);
        _handler.Add((ushort)PacketId.PKT_RES_ENTER_ROOM, PacketHandler.ResEnterRoomHandler);
        _onRecv.Add((ushort)PacketId.PKT_RES_SPAWN, MakePacket<Protocol.RES_SPAWN>);
        _handler.Add((ushort)PacketId.PKT_RES_SPAWN, PacketHandler.ResSpawnHandler);
        _onRecv.Add((ushort)PacketId.PKT_RES_SPAWN_ALL, MakePacket<Protocol.RES_SPAWN_ALL>);
        _handler.Add((ushort)PacketId.PKT_RES_SPAWN_ALL, PacketHandler.ResSpawnAllHandler);
        _onRecv.Add((ushort)PacketId.PKT_RES_DESPAWN, MakePacket<Protocol.RES_DESPAWN>);
        _handler.Add((ushort)PacketId.PKT_RES_DESPAWN, PacketHandler.ResDespawnHandler);
        _onRecv.Add((ushort)PacketId.PKT_RES_MOVE, MakePacket<Protocol.RES_MOVE>);
        _handler.Add((ushort)PacketId.PKT_RES_MOVE, PacketHandler.ResMoveHandler);
        _onRecv.Add((ushort)PacketId.PKT_RES_SPAWN_MONSTER, MakePacket<Protocol.RES_SPAWN_MONSTER>);
        _handler.Add((ushort)PacketId.PKT_RES_SPAWN_MONSTER, PacketHandler.ResSpawnMonsterHandler);
    }

    public void OnRecvPacket(PacketSession session, ArraySegment<byte> buffer)
    {
        ushort count = 0;

        ushort size = BitConverter.ToUInt16(buffer.Array, buffer.Offset);
        count += 2;
        ushort id = BitConverter.ToUInt16(buffer.Array, buffer.Offset + count);
        count += 2;

        Action<PacketSession, ArraySegment<byte>, ushort> action = null;
        if (_onRecv.TryGetValue(id, out action))
            action.Invoke(session, buffer, id);
    }

    public void MakePacket<T>(PacketSession session, ArraySegment<byte> buffer, ushort id) where T : IMessage, new()
    {
        T pkt = new T();
        pkt.MergeFrom(buffer.Array, buffer.Offset + 4, buffer.Count - 4);

        if (CustomHandler != null)
        {
            CustomHandler.Invoke(session, pkt, id);
        }
        else
        {
            Action<PacketSession, IMessage> action = null;
            if (_handler.TryGetValue(id, out action))
                action.Invoke(session, pkt);
        }
    }

    public Action<PacketSession, IMessage> GetPacketHandler(ushort id)
    {
        Action<PacketSession, IMessage> action = null;
        if (_handler.TryGetValue(id, out action))
            return action;
        return null;
    }
}
