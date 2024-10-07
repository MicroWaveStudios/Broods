using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    PlayerStats playerStats;

    PlayerMoveRigidbody rbPlayer;

    [SerializeField] float damage;
    [SerializeField] float attackRange;
    float moveDamage;
    bool addEnergy;

    private void Awake()
    {
        playerStats = transform.parent.GetComponent<PlayerStats>();

        rbPlayer = transform.parent.GetComponent<PlayerMoveRigidbody>();
    }

    private void OnEnable()
    {
        rbPlayer.MoverAoAtacar(moveDamage);
    }

    public void SetAttack(float newDamage, float newAttackRange, float newMoveDamage, bool newAddEnergy)
    {
        damage = newDamage;
        attackRange = newAttackRange;
        moveDamage = newMoveDamage;
        addEnergy = newAddEnergy;
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

            if (addEnergy)
            {
                playerStats.AddEnergy(damage);
            }

            otherPlayerStats.SufferDamage(damage, attackRange, moveDamage, this.transform.parent.gameObject);
            otherPlayerStats.AddEnergy(damage / 2);
        }
    }

}
