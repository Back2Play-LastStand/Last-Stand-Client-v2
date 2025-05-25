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

    [Header("Ammo")]
    [SerializeField] private int m_maxAmmo = 90;
    [SerializeField] private int m_gunAmmo = 30;
    public int m_curAmmo;

    public void Start()
    {
        m_curAmmo = m_gunAmmo;
    }

    public void Attack()
    {
        if (m_curAmmo > 0 && (m_firingTime - m_lastFiringTime > m_delay))
        {
            Instantiate(m_bulletPrefab, m_firingPos.position, m_firingPos.rotation);
            m_lastFiringTime = Time.time;
            m_curAmmo--;

            Managers.UI.m_Interface.SetRemainAmmoText(m_curAmmo, m_maxAmmo);
        }
        else if (m_curAmmo <= 0)
        {
            Reload();
        }
        m_firingTime = Time.time;
    }

    public void Reload()
    {
        if (m_curAmmo == m_gunAmmo || m_maxAmmo <= 0)
            return;

        int needAmmo = m_gunAmmo - m_curAmmo;

        if (m_maxAmmo >= needAmmo)
        {
            m_curAmmo += needAmmo;
            m_maxAmmo -= needAmmo;
        }
        else
        {
            m_curAmmo += m_maxAmmo;
            m_maxAmmo = 0;
        }

        Managers.UI.m_Interface.SetRemainAmmoText(m_curAmmo, m_maxAmmo);
    }
}