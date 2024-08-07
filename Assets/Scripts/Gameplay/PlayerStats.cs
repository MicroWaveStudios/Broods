using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] public float life;
    [SerializeField] public float maxLife;
    [SerializeField] float damageMultiplier;
    [SerializeField] PlayerMoveRigidbody moveRigidbody;
    public bool defendendo;

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
            life -= damage;
            moveRigidbody.MoverAoLevarDano();
        }
        
    }
}
