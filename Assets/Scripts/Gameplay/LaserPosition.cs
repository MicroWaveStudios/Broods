using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserPosition : MonoBehaviour
{
    public GameObject outroPlayer;
    Vector3 posicaoInicial;
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
    }

    private void Update()
    {

        if (transform.parent.gameObject.CompareTag("Player1"))
        {
            outroPlayer = GameObject.FindGameObjectWithTag("Player2");
        }
        else
        {
            outroPlayer = GameObject.FindGameObjectWithTag("Player1");
        }

        if (outroPlayer != null)
        {
            posicaoLaser = new Vector3(outroPlayer.transform.position.x, outroPlayer.transform.position.y + 1.2f, outroPlayer.transform.position.z);
        }
    }

    public void StartLaser(GameObject player)
    {
        StartCoroutine(Laser(player));
    }
    private IEnumerator Laser(GameObject player)
    {
        particula.Play();
        yield return null;
        yield return AtirarLaser();
        yield return ResetPosition(player);
        yield break;
    }

    private IEnumerator AtirarLaser()
    {
        
        posicaoInicial = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        transform.parent = null;
        transform.position = posicaoLaser;
        yield break;
    }

    private IEnumerator ResetPosition(GameObject player)
    {
        particula.Stop();
        transform.parent = player.transform;
        transform.position = posicaoInicial;
        yield break;
    }
}
