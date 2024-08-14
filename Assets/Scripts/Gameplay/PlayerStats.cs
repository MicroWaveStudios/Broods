using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] public float life;
    [SerializeField] public float maxLife;
    [SerializeField] float damageMultiplier;
    PlayerMoveRigidbody scrpMoveRigidbody;
    PlayerCombat scrpPlayerCombat;
    public bool defendendo;

    private void Awake()
    {
        scrpMoveRigidbody = this.GetComponent<PlayerMoveRigidbody>();
        scrpPlayerCombat = this.GetComponent<PlayerCombat>();
    }

    private void Start()
    {
        life = maxLife;
    }

    private void Update()
    {
        if (life <= 0f)
            Destroy(gameObject);
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
            }
            StartCoroutine(ResetScripts());
        }
        
    }

    IEnumerator ResetScripts()
    {
        scrpPlayerCombat.BreakAnimation();
        scrpMoveRigidbody.enabled = false;
        scrpPlayerCombat.enabled = false;
        Debug.Log("Parou");
        yield return new WaitForSeconds(0.5f);
        scrpMoveRigidbody.enabled = true;
        scrpPlayerCombat.enabled = true;
        yield break;
    }
}
