using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float damage;

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        
        if (collision != null)
        {
            PlayerStats stats = collision.gameObject.GetComponent<PlayerStats>();
            if (stats != null)
            {
                stats.SufferDamage(damage);
            }
        }
    }
}
