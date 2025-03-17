using Google.Protobuf;
using Protocol;
using ServerCore;

public class PacketHandler
{
    public static void ReqEnterHandler(PacketSession session, IMessage packet)
    {
        REQ_ENTER enterPacket = packet as REQ_ENTER;
        ServerSession serverSession = session as ServerSession;
    }
}
