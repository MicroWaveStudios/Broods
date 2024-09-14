using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    bool defendendo;

    [SerializeField] bool teste;
    

    private void Awake()
    {
        playerMoveRigidbody = this.GetComponent<PlayerMoveRigidbody>();
        playerCombat = this.GetComponent<PlayerCombat>();
        playerAnimator = this.GetComponent<PlayerAnimator>();
    }

    private void Start()
    {
        life = maxLife;
        energy = maxEnergy;
    }

    private void Update()
    {
        if (life <= 0f)
        {
            GameObject gameController = GameObject.FindGameObjectWithTag("GameManager");
            gameController.GetComponent<GameController>().GameFinished();
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

    public void SufferDamage(float damage, float attackRange, GameObject otherPlayer)
    {
        StartCoroutine(SetDefended());
        if (playerCombat.GetInAttack() && playerCombat.GetInCombo())
        {
            if (playerCombat.GetOrdem() > otherPlayer.GetComponent<PlayerCombat>().GetOrdem())
            {
                MoveDamage();
                otherPlayer.GetComponent<PlayerStats>().MoveDamage();
            }
        }
        else
        {
            if (defendendo)
            {
                playerAnimator.TriggerAction("Defendeu");
            }
            else
            {
                life -= damage;
                playerMoveRigidbody.MoveUp();
                MoveDamage();
                playerCombat.ResetCombo(0f);
                if (playerCombat.ordem > 0)
                {
                    StartCoroutine(ResetScripts(0.5f));
                }
            }
        }
       
        GameObject GameManager = GameObject.FindGameObjectWithTag("GameManager");
        GameManager.GetComponent<GameController>().SetTimeScale();
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
}
