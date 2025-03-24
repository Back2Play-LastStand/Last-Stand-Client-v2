using Google.Protobuf;
using Protocol;
using ServerCore;
using UnityEngine;

public class PacketHandler
{
    public static void ResEnterHandler(PacketSession session, IMessage packet)
    {
        RES_ENTER enterPacket = packet as RES_ENTER;
        ServerSession serverSession = session as ServerSession;

        Debug.Log("ResEnterHandler");

        REQ_ENTER_ROOM enterRoomPacket = new();
        enterRoomPacket.Name = "roomname";
        Managers.Network.Send(enterRoomPacket, (ushort)PacketId.PKT_REQ_ENTER_ROOM);
    }
    public static void ResLeaveHandler(PacketSession session, IMessage packet)
    {
        RES_LEAVE leavePacket = packet as RES_LEAVE;
        Managers.Object.RemoveMyPlayer();
    }
    public static void ResEnterRoomHandler(PacketSession session, IMessage packet)
    {
        RES_ENTER_ROOM enterRoomPacket = packet as RES_ENTER_ROOM;
        ServerSession serverSession = session as ServerSession;

        Debug.Log("ResEnterRoomHandler");
    }
    public static void ResSpawnHandler(PacketSession session, IMessage packet)
    {
        RES_SPAWN spawnPacket = packet as RES_SPAWN;
        ServerSession serverSession = session as ServerSession;

        Debug.Log("ResSpawnHandler");
        Managers.Object.Add(spawnPacket.Player, myPlayer: spawnPacket.Mine);
    }
    public static void ResSpawnAllHandler(PacketSession session, IMessage packet)
    {
        RES_SPAWN_ALL spawnAllPacket = packet as RES_SPAWN_ALL;
        foreach (ObjectInfo player in spawnAllPacket.Players)
        {
            Managers.Object.Add(player, myPlayer: false);
        }
    }
    public static void ResDespawnHandler(PacketSession session, IMessage packet)
    {
        RES_DESPAWN despawnPacket = packet as RES_DESPAWN;

        Debug.Log("ResDespawnHandler");
    }
    public static void ResMoveHandler(PacketSession session, IMessage packet)
    {
        RES_MOVE movePacket = packet as RES_MOVE;

        Debug.Log("ResMoveHandler");
    }
}
