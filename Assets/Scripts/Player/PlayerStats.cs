using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] float vida;
    [SerializeField] float vidaMax;
    float damageMultiplier = 0.5f;
    [SerializeField] float energiaMax;
    [SerializeField] float energia;
    PlayerMoveRigidbody playerMoveRigidbody;
    PlayerCombat playerCombat;
    PlayerAnimator playerAnimator;
    GameController scrpGameController;
    Sons scrSons;
    public bool defendendo;

    public bool defendeu;

    public float pontos = 0f;
    public float golpesSequencia = 0f;
    public float timer = 0f;

    [SerializeField] bool tutorial;

    [SerializeField] bool Ximas;

    [SerializeField] bool teste;

    [SerializeField] VisualEffect vfxImpacto;
    [SerializeField] VisualEffect vfxDefesa;
    private void Awake()
    {
        playerMoveRigidbody = this.GetComponent<PlayerMoveRigidbody>();
        playerCombat = this.GetComponent<PlayerCombat>();
        playerAnimator = this.GetComponent<PlayerAnimator>();
        scrSons = this.GetComponent<Sons>();

        if (!tutorial)
        {
            scrpGameController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameController>();
        }
    }

    private void Start()
    {
        vida = vidaMax;
        energia = 0;
        StartCoroutine(TimerFimSequencia());
    }

    private void Update()
    {
        if (tutorial) 
        {
            //GetComponent<PlayerInput>().SwitchCurrentControlScheme("");
        }
        if (vida <= 0f && !tutorial)
        {
            Debug.Log("Vida do Player" + GetComponent<PlayerController>().GetPlayerID() + " é: " + vida);
            scrpGameController.GameFinished();
            playerMoveRigidbody.Morrer();
            tutorial = true;
        }
        if (teste == true)
        {
            InvokeRepeating("Regen", 3f, 3f);
            teste = false;
        }
    }

    public void SetDefendendo(bool value)
    {
        defendendo = value;
    }

    public bool GetDefendendo()
    {
        return defendendo;
    }

    bool InAction = false;

    public bool GetInAction()
    {
        return InAction;
    }

    //public void MoveDamage(float moveDamage)
    //{
    //    //playerMoveRigidbody.SetForce(moveDamage);
    //    playerMoveRigidbody.MoverAoLevarDano(moveDamage);
    //    //playerAnimator.TriggerAction("TomouDano");
    //}

    public void CounterParry(GameObject otherPlayer)
    {
        scrSons.TocarSom("ParryCerto");
        SomarPontos(150);
        AddEnergy(9999);
        playerAnimator.TriggerAction("PerryContinuacao");
        otherPlayer.GetComponent<PlayerStats>().SufferDamage(7, 3, 10, 10, 0, null);
    }

    public void SufferDamage(float damage, float attackRange, float moveDamage, float moveDamageOtherPlayer, float moveUpOtherPlayer, GameObject otherPlayer)
    {
        StartCoroutine(SetInAction());
        switch (attackRange)
        {
            case 0:
                if (playerMoveRigidbody.GetCrouched() && defendendo)
                {
                    playerAnimator.TriggerAction("Defendeu");
                    defendeu = true;
                }
                else
                {
                    defendeu = false;
                }
                break;
            case 1:
                if (!playerMoveRigidbody.GetCrouched() && defendendo)
                {
                    playerAnimator.TriggerAction("Defendeu");
                    defendeu = true;
                }
                else
                {
                    defendeu = false;
                }
                break;
            case 2:
                if (defendendo)
                {
                    playerAnimator.TriggerAction("Defendeu");
                    defendeu = true;
                }
                else
                {
                    defendeu = false;
                }
                break;
        }
        if (!defendeu)
        {
            if (playerCombat.GetInParry() == true)
            {
                CounterParry(otherPlayer);
            }
            else
            {
                SomarPontos(25);
                vida -= damage;
                playerAnimator.TriggerAction("TomouDano");
                playerMoveRigidbody.MoveUp(moveUpOtherPlayer);
                playerMoveRigidbody.MoverAoLevarDano(moveDamageOtherPlayer);
                //MoveDamage(moveDamage);

                vfxImpacto.Play();
                scrSons.TocarSom("LevarDano");
                

                playerCombat.ResetCombo();
                StartCoroutine(ResetScripts(0.5f));
            }
        }
        else
        {
            SomarPontos(50);
            vfxDefesa.Play();
        }
        //scrpGameController.SetTimeScale();
    }


    public IEnumerator ResetScripts(float delay)
    {
        yield return new WaitForSeconds(0.1f);
        playerMoveRigidbody.enabled = false;
        playerCombat.enabled = false;
        yield return new WaitForSeconds(delay);
        playerMoveRigidbody.enabled = true;
        playerCombat.enabled = true;
        yield break;
    }

    IEnumerator SetInAction()
    {
        InAction = true;
        yield return new WaitForSeconds(0.3f);
        InAction = false;
    }

    public void UsouSkill(float custoSkill)
    {
        //float oldEnergy = energy;

        energia -= custoSkill;

        //if (energy < 0)
        //{
        //    energy = oldEnergy;
        //}

    }

    void Regen()
    {
        vida += 100f;

        if (vida > vidaMax)
        {
            vida = vidaMax;
        }
    }

    public void AddEnergy(float qtdEnergia)
    {
        energia += qtdEnergia/2;
        if (energia > energiaMax)
        {
            energia = energiaMax;
        }
    }

    public bool GetXimas()
    {
        return Ximas;
    }

    public bool GetEnergyFull()
    {
        if (energia >= energiaMax)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public float GetDamageMultiplier()
    {
        return damageMultiplier;
    }
    public void SomarDamageMultiplier(float NewDamageMultiplier)
    {
        damageMultiplier += NewDamageMultiplier;
    }

    public void SomarPontos(float somaPontos)
    {
        pontos += somaPontos;
    }

    public float GetPontos()
    {
        return pontos;
    }

    public void SomarSequencia()
    {
        timer = 0f;
        golpesSequencia++;
    }

    public float GetSequencia()
    {
        return golpesSequencia;
    }

    public float GetEnergia()
    { 
        return energia;
    }
    public float GetEnergiaMax()
    { 
        return energiaMax;
    }
    public float GetVida()
    {
        return vida;
    }
    public float GetVidaMax() 
    { 
        return vidaMax;
    }
    IEnumerator TimerFimSequencia()
    {
        while (true)
        {
            while (timer < 0.8f)
            {
                timer += 1 * Time.deltaTime;
                yield return null;
            }

            timer = 0f;
            golpesSequencia = 0;
            yield return null;
        }        
    }
}