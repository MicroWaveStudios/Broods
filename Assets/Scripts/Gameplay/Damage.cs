using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] float damage = 10f;

    private void Start()
    {
        Debug.Log("nasci");
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.name);
        if (collision != null)
        {
            PlayerStats stats = collision.gameObject.GetComponent<PlayerStats>();
            if (stats != null)
            {
                stats.SufferDamage(damage);
                Debug.Log("Player tomou dano");
            }
        }
    }
}
