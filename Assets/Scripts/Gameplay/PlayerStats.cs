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
    PlayerMoveRigidbody scrpMoveRigidbody;
    PlayerCombat scrpPlayerCombat;
    public bool defendendo;

    [SerializeField] bool teste;
    

    private void Awake()
    {
        scrpMoveRigidbody = this.GetComponent<PlayerMoveRigidbody>();
        scrpPlayerCombat = this.GetComponent<PlayerCombat>();
    }

    private void Start()
    {
        life = maxLife;
        energy = maxEnergy;
    }

    private void Update()
    {
        if (life <= 0f)
            Destroy(gameObject);

        if (teste == true)
        {
            InvokeRepeating("Regen", 3f, 3f);
            teste = false;
        }

        
    }

    public void SufferDamage(float damage)
    {
        if (defendendo == false)
        {
            scrpPlayerCombat.ResetCombo();
            life -= damage;    
            if (scrpPlayerCombat.ordem > 0)
            {
                scrpMoveRigidbody.MoverAoLevarDano();
                StartCoroutine(ResetScripts(true, 0.5f));
            }
            
        }
<<<<<<< Updated upstream
        
=======
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

                vfxImpacto.Play();

                playerCombat.ResetCombo();
                if (playerCombat.ordem > 0)
                {
                    StartCoroutine(ResetScripts(0.5f));
                }
            }
        }
        GameObject GameManager = GameObject.FindGameObjectWithTag("GameManager");
        GameManager.GetComponent<GameController>().SetTimeScale();
>>>>>>> Stashed changes
    }

    public IEnumerator ResetScripts(bool damage, float delay)
    {
        if(damage == true)
        {
            scrpPlayerCombat.HitAnimation();
        }
        scrpMoveRigidbody.enabled = false;
        scrpPlayerCombat.enabled = false;
        yield return new WaitForSeconds(delay);
        scrpMoveRigidbody.enabled = true;
        scrpPlayerCombat.enabled = true;
        yield break;
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
