using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float m_speed = 10;
    private float m_lifeTime = 5f;

    public void SetSpeed(float speed)
    {
        m_speed = speed;
    }

    void Start()
    {
        Destroy(gameObject, m_lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * m_speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Monster monster = other.GetComponent<Monster>();
        if(monster != null)
        {
            Protocol.REQ_ATTACK_OBJECT attack = new()
            {
                Attacker = Managers.Object.MyPlayer.Id,
                ObjectId = monster.Id,
                Damage = Managers.Object.MyPlayer.Amount
            };

            Managers.Network.Send(attack, (ushort)PacketId.PKT_REQ_ATTACK_OBJECT);
            Destroy(gameObject);
        }
    }
}
