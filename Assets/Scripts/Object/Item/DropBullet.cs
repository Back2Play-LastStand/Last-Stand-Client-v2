using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBullet : MonoBehaviour
{
    public int amount = 30;

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerShoot playerShoot = other.GetComponent<PlayerShoot>();
            if(playerShoot != null )
            {
                playerShoot.AddAmmo(amount);
            }
        }

        Destroy(gameObject);
    }
}
