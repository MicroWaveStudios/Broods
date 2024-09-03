using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPosition : MonoBehaviour
{
    Rigidbody rb;
    GameObject outroPlayer;
    [SerializeField] float speed;
    Vector3 posicaoInicial;
    ParticleSystem particula;

    private void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();

        particula = this.gameObject.GetComponent<ParticleSystem>();

        if (this.gameObject.CompareTag("Player1"))
        {
            outroPlayer = GameObject.FindGameObjectWithTag("Player2");
        }        
        else
        {
            outroPlayer = GameObject.FindGameObjectWithTag("Player1");
        }
    }

    private void Start()
    {
        posicaoInicial = this.transform.position;
    }

    public void AtirarLaser()
    {
        particula.emission.rateOverDistance = 0f; 
        rb.velocity = outroPlayer.transform.position * speed;
    }

    public void ResetPosition()
    {
        particula.startSize = 0f;
        outroPlayer.transform.position = posicaoInicial;
    }
}
