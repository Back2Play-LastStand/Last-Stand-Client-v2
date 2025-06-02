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
        Managers.Object.Remove(leavePacket.ObjectId);
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
    public static void ResChangeHpHandler(PacketSession session, IMessage packet)
    {
        RES_CHANGE_HP changePacket = packet as RES_CHANGE_HP;

        GameObject go = Managers.Object.FindById(changePacket.ObjectId);
        if (go == null) return;
        Creature creature = go.GetComponent<Creature>();
        if (creature == null) return;

        creature.SetHealth(changePacket.Hp);
        creature.UpdateHealthBar(creature);
    }
    public static void ResDieHandler(PacketSession session, IMessage packet)
    {
        RES_DIE diePacket = packet as RES_DIE;

        GameObject go = Managers.Object.FindById(diePacket.ObjectId);
        if (go == null) return;
        Creature creature = go.GetComponent<Creature>();
        if (creature == null) return;

        creature.Die();
    }
    public static void ResMoveHandler(PacketSession session, IMessage packet)
    {
        RES_MOVE movePacket = packet as RES_MOVE;

        GameObject go = Managers.Object.FindById(movePacket.Player.ObjectId);
        if (go == null) return;
        Player player = go.GetComponent<Player>();
        if (player == null) return;
        Debug.Log("ResMoveHandler");

        player.PosInfo = movePacket.Player.PosInfo;
    }
    public static void ResSpawnMonsterHandler(PacketSession session, IMessage packet)
    {
        RES_SPAWN_MONSTER spawnMonsterPacket = packet as RES_SPAWN_MONSTER;
        foreach (ObjectInfo monster in spawnMonsterPacket.Monsters)
        {
            Managers.Object.AddMonster(monster);
        }
    }
    public static void ResMoveMonsterHandler(PacketSession session, IMessage packet)
    {
        RES_MOVE_MONSTER movePacket = packet as RES_MOVE_MONSTER;

        GameObject go = Managers.Object.FindById(movePacket.Monster.ObjectId);
        if (go == null) return;
        Monster monster = go.GetComponent<Monster>();
        if (monster == null) return;

        monster.PosInfo = movePacket.Monster.PosInfo;
    }
    public static void ResAttackObjectHandler(PacketSession session, IMessage packet)
    {
        RES_ATTACK_OBJECT attackPacket = packet as RES_ATTACK_OBJECT;

        Debug.Log("ResAttackObjectHandler");

        GameObject attackerGO = Managers.Object.FindById(attackPacket.Attacker);
        GameObject targetGO = Managers.Object.FindById(attackPacket.ObjectId);
        if (attackerGO == null || targetGO == null) return;

        Creature attacker = attackerGO.GetComponent<Creature>();
        Creature target = targetGO.GetComponent<Creature>();
        if (attacker == null || target == null) return;

        target.OnDamaged(attacker);
    }
}
