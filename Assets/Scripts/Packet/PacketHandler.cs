using Google.Protobuf;
using Protocol;
using ServerCore;

public class PacketHandler
{
    public static void ResEnterHandler(PacketSession session, IMessage packet)
    {
        RES_ENTER enterPacket = packet as RES_ENTER;
        ServerSession serverSession = session as ServerSession;
    }
}
