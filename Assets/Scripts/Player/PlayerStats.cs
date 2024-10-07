using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] public float life;
    [SerializeField] public float maxLife;
    [SerializeField] float damageMultiplier;
    [SerializeField] public float maxEnergy;
    [SerializeField] public float energy;
    PlayerMoveRigidbody playerMoveRigidbody;
    PlayerCombat playerCombat;
    PlayerAnimator playerAnimator;
    GameController scrpGameController;
    public bool defendendo;

    public bool defendeu;
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

        if (!tutorial)
        {
            scrpGameController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameController>();
        }
    }

    private void Start()
    {
        life = maxLife;
        energy = 0;
    }

    private void Update()
    {
        if (life <= 0f && !tutorial)
        {
            scrpGameController.GameFinished();
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

    bool defended = false;

    public bool GetDefendedOrSuffered()
    {
        return defended;
    }

    public void MoveDamage()
    {
        playerMoveRigidbody.MoverAoLevarDano();
        playerAnimator.TriggerAction("TomouDano");
    }

    public void CounterParry(GameObject otherPlayer)
    {
        playerAnimator.TriggerAction("PerryContinuacao");
        otherPlayer.GetComponent<PlayerStats>().SufferDamage(10, 3, 10, null);
    }

    public void SufferDamage(float damage, float attackRange, float moveDamage, GameObject otherPlayer)
    {
        StartCoroutine(SetDefended());
        if (playerCombat.GetInAttack() && otherPlayer != null || playerCombat.GetInCombo() && otherPlayer != null)
        {
            MoveDamage();
            otherPlayer.GetComponent<PlayerStats>().MoveDamage();
        }
        else
        {

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
                    life -= damage;
                    playerAnimator.TriggerAction("TomouDano");
                    playerMoveRigidbody.MoveUp();
                    MoveDamage();

                    vfxImpacto.Play();

                    playerCombat.ResetCombo();
                    StartCoroutine(ResetScripts(0.5f));
                }
            }
            else
            {
                vfxDefesa.Play();
            }
        }

        scrpGameController.SetTimeScale();
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

    IEnumerator SetDefended()
    {
        defended = true;
        yield return new WaitForSeconds(0.3f);
        defended = false;
    }

    public void UsouSkill(float custoSkill)
    {
        energy -= custoSkill;
    }

    void Regen()
    {
        life += 100f;

        if (life > maxLife)
        {
            life = maxLife;
        }
    }

    public void AddEnergy(float qtdEnergia)
    {
        energy += qtdEnergia;
        if (energy > maxEnergy)
        {
            energy = maxEnergy;
        }
    }

    public bool GetXimas()
    {
        return Ximas;
    }

    public bool GetEnergyFull()
    {
        if (energy >= maxEnergy)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}