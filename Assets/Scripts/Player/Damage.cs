using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    PlayerStats playerStats;

    PlayerCombat playerCombat;

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
    //bool acertou = false;
    bool isAtaqueMedioXimas;

    private void Awake()
    {
        playerStats = transform.parent.GetComponent<PlayerStats>();
        
        playerCombat = transform.parent.GetComponent<PlayerCombat>();

        rbPlayer = transform.parent.GetComponent<PlayerMoveRigidbody>();

        scrSons = transform.parent.GetComponent<Sons>();
    }

    private void OnDisable()
    {
        if (playerStats.GetXimas() == true)
        {
            bool zerarVelocidade = false;

            if (playerCombat.GetNomeAtaqueAtual() == "AtaqueFraco")
            {
                if (playerCombat.GetOrdemCombo() == 2)
                {
                    zerarVelocidade = true;
                }
            }

            if (playerCombat.GetNomeAtaqueAtual() == "AtaqueForte")
            {
                if (playerCombat.GetOrdemCombo() >= 2)
                {
                    zerarVelocidade = true;
                }
            }

            //if (playerCombat.GetNomeAtaqueAtual() == "AtaqueMedio")
            //{
            //    if (playerCombat.GetOrdemCombo() < 3 && playerCombat.GetOrdemCombo() != 0)
            //    {
            //        zerarVelocidade = true;
            //    }
            //}

            if (zerarVelocidade == true)
            {
                rbPlayer.ZerarVelocidade();
            }
        }
        
    }

    private void OnEnable()
    {
        if (playerCombat.GetNomeAtaqueAtual() == "AtaqueMedio" && playerCombat.GetInCombo() == true)
        {
            if (playerCombat.GetOrdemCombo() < 3 && playerCombat.GetOrdemCombo() != 0)
            {
                isAtaqueMedioXimas = true;
            }
            else
            {
                isAtaqueMedioXimas = false;
            }
        }
        else
        {
            isAtaqueMedioXimas = false;
        }

        rbPlayer.MoveUp(moveUp);
        rbPlayer.MoverAoAtacar(moveDamage, isAtaqueMedioXimas);
        scrSons.TocarSom("NullHit");

        if (somAtaque == "Laser")
        {
            scrSons.TocarSom("Laser");
        }

        if (somAtaque == "Tarticos")
        {
            scrSons.TocarSom("Tarticos");
        }

        if (somAtaque == "Sopro")
        {
            scrSons.TocarSom("Sopro");
        }

        if (somAtaque == "ParryCerto")
        {
            scrSons.TocarSom("ParryCerto");
        }

        if (somAtaque == "ParryInicio")
        {
            scrSons.TocarSom("ParryInicio");
        }

        if (somAtaque == "Dash")
        {
            scrSons.TocarSom("Dash");
        }
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

    //public void SetDamage(float newDamage)
    //{
    //    damage = newDamage;
    //}

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
                playerStats.SomarPontos(100);
                playerStats.AddEnergy(damage * playerStats.GetDamageMultiplier());
            }

            //playerStats.SomarSequencia();
            otherPlayerStats.SufferDamage(damage * playerStats.GetDamageMultiplier(), attackRange, moveDamage, moveDamageOtherPlayer, moveUpOtherPlayer, this.transform.parent.gameObject);
            otherPlayerStats.AddEnergy((damage * playerStats.GetDamageMultiplier()) / 3);

            scrSons.TocarSom(somAtaque);
        }
    }
}
