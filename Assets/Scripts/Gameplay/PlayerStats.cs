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
    public bool defendendo;

    [SerializeField] bool teste;
    

    private void Awake()
    {
        playerMoveRigidbody = this.GetComponent<PlayerMoveRigidbody>();
        playerCombat = this.GetComponent<PlayerCombat>();
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
            playerCombat.ResetCombo(0f);
            life -= damage;    
            if (playerCombat.ordem > 0)
            {
                playerMoveRigidbody.MoverAoLevarDano();
                StartCoroutine(ResetScripts(true, 0.5f));
            }
            
        }
        
    }

    public IEnumerator ResetScripts(bool damage, float delay)
    {
        if(damage == true)
        {
            //scrpPlayerCombat.HitAnimation();
        }
        playerMoveRigidbody.enabled = false;
        playerCombat.enabled = false;
        yield return new WaitForSeconds(delay);
        playerMoveRigidbody.enabled = true;
        playerCombat.enabled = true;
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
