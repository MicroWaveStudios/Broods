using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defense : MonoBehaviour
{
    Damage ataque;
    float dano;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Attack"))
        {           
            ataque = collision.collider.gameObject.GetComponent<Damage>();
            dano = ataque.damage;
            ataque.damage = 0;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        ataque.damage = dano;
        dano = 0f;
    }
}
