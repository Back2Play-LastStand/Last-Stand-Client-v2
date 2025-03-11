using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float m_speed = 10;

    public void SetSpeed(float speed)
    {
        m_speed = speed;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * m_speed * Time.deltaTime);
    }
}
