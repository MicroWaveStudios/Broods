using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;

    [SerializeField] float damage;
    [SerializeField] float attackRange;
    int divisaoEnergia = 1;

    private void Awake()
    {
        playerStats = transform.parent.GetComponent<PlayerStats>();
    }

    public void SetAttack(float newDamage, float newAttackRange)
    {
        damage = newDamage;
        attackRange = newAttackRange;
    }

    public void SetDamage(float newDamage)
    {
        damage = newDamage;
    }

    private void OnTriggerEnter(Collider collision)
    {
        PlayerStats otherPlayerStats = collision.gameObject.GetComponent<PlayerStats>();

        if (otherPlayerStats != null)
        {
            if (collision.CompareTag("Player2"))
            {
                playerStats = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerStats>();
            }
            else
            {
                playerStats = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerStats>();
            }

            if(otherPlayerStats.GetDefendendo() == true)
            {
                divisaoEnergia = 4;         
            }
            else
            {
                divisaoEnergia = 1;
            }

            playerStats.AddEnergy(damage / divisaoEnergia);
            otherPlayerStats.AddEnergy(damage / divisaoEnergia * 2);
            otherPlayerStats.SufferDamage(damage, attackRange, this.transform.parent.gameObject);




        }
    }

}
