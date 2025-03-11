using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour, IPlayerAttack
{
    [SerializeField] private GameObject m_bulletPrefab;
    [SerializeField] private Transform m_firingPos;

    [SerializeField] private float m_delay;
    private float m_lastFiringTime;
    private float m_firingTime = float.MaxValue;

    public void Attack()
    {
        if(m_firingTime - m_lastFiringTime > m_delay)
        {
            Instantiate(m_bulletPrefab, m_firingPos);
            m_lastFiringTime = Time.time;
        }
        m_firingTime = Time.time;
    }
}