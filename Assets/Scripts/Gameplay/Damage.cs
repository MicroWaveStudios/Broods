using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float damage;

    public void SetDamage(float newDamage)
    {
        damage = newDamage;
    }


    private void OnTriggerEnter(Collider collision)
    {
        
        if (collision != null)
        {
            PlayerStats playerStats;

            PlayerStats stats = collision.gameObject.GetComponent<PlayerStats>();

            if (stats != null)
            {
                if (collision.CompareTag("Player2"))
                {
                    playerStats = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerStats>();
                }
                else
                {
                    playerStats = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerStats>();
                }

                playerStats.AddEnergy(damage);

                stats.SufferDamage(damage);
                stats.AddEnergy(damage/2);
            }
        }
    }
}
