using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPosition : MonoBehaviour
{
    GameObject outroPlayer;
    [SerializeField] Transform posicaoInicial;
    Vector3 posicaoLaser;
    public ParticleSystem particula;
    [SerializeField] bool ligado = false;

    private void Awake()
    {
        particula = this.gameObject.GetComponent<ParticleSystem>();

        if (ligado != true)
        {
            particula.Stop();
        }
        

        if (transform.parent.CompareTag("Player1"))
        {
            outroPlayer = GameObject.FindGameObjectWithTag("Player2");
        }
        else
        {
            outroPlayer = GameObject.FindGameObjectWithTag("Player1");
        }
    }

    private void Update()
    {
        if (outroPlayer != null)
        {
            posicaoLaser = new Vector3(outroPlayer.transform.position.x, outroPlayer.transform.position.y + 10f, outroPlayer.transform.position.z);
        }      
    }

    public void AtirarLaser()
    {
        transform.parent = null;
        transform.position = posicaoLaser;
    }

    public void ResetPosition(GameObject player)
    {
        particula.Stop();

        transform.position = posicaoInicial.position;
        transform.parent = player.transform;
    }
}
