using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;

    [SerializeField] float damage;
    [SerializeField] float attackRange;

    private void Awake()
    {
        playerStats = transform.parent.GetComponent<PlayerStats>();
    }

    public void SetAttack(float newDamage, float newAttackRange)
    { 
        damage = newDamage;
        attackRange = newAttackRange;
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

            playerStats.AddEnergy(damage);

            otherPlayerStats.SufferDamage(damage, attackRange);
            otherPlayerStats.AddEnergy(damage/2);
        }
    }

}
