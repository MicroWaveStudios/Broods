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
    float moveDamageOtherPlayer;
    float moveUpOtherPlayer;
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

    public void SetAttack(float newDamage, float newAttackRange, float newMoveDamage, float newMoveDamageOtherPlayer, float newMoveUpOtherPlayer, bool newAddEnergy)
    {
        damage = newDamage;
        attackRange = newAttackRange;
        moveDamage = newMoveDamage;
        moveDamageOtherPlayer = newMoveDamageOtherPlayer;
        moveUpOtherPlayer = newMoveUpOtherPlayer;
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

            otherPlayerStats.SufferDamage(damage, attackRange, moveDamage, moveDamageOtherPlayer, moveUpOtherPlayer, this.transform.parent.gameObject);
            otherPlayerStats.AddEnergy(damage / 2);
        }
    }

}
