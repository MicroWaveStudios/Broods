using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    PlayerStats playerStats;

    PlayerMoveRigidbody rbPlayer;

    Sons scrSons;

    [SerializeField] float damage;
    [SerializeField] float attackRange;
    float moveDamage;
    float moveDamageOtherPlayer;
    float moveUpOtherPlayer;
    float moveUp;
    bool addEnergy;
    string somAtaque;

    private void Awake()
    {
        playerStats = transform.parent.GetComponent<PlayerStats>();

        rbPlayer = transform.parent.GetComponent<PlayerMoveRigidbody>();

        scrSons = transform.parent.GetComponent<Sons>();
    }

    private void OnEnable()
    {
        rbPlayer.MoveUp(moveUp);
        rbPlayer.MoverAoAtacar(moveDamage);
        scrSons.TocarSom(somAtaque);
    }

    public void SetAttack(float newDamage, float newAttackRange, float newMoveDamage, float newMoveDamageOtherPlayer, float newMoveUpOtherPlayer, bool newAddEnergy, float newMoveUp, string som)
    {
        damage = newDamage;
        attackRange = newAttackRange;
        moveDamage = newMoveDamage;
        moveDamageOtherPlayer = newMoveDamageOtherPlayer;
        moveUpOtherPlayer = newMoveUpOtherPlayer;
        addEnergy = newAddEnergy;
        moveUp = newMoveUp;
        somAtaque = som;
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

            Debug.Log(moveDamageOtherPlayer);

            otherPlayerStats.SufferDamage(damage, attackRange, moveDamage, moveDamageOtherPlayer, moveUpOtherPlayer, this.transform.parent.gameObject);
            otherPlayerStats.AddEnergy(damage / 2);
        }
    }

}
