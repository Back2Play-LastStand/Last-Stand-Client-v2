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
        Debug.Log(enterPacket.Players);
    }
}
