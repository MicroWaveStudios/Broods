using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosicaoImpacto : MonoBehaviour
{
    Vector3 posicaoLanca;

    Vector3 antigaPosicaoLanca;

    GameObject outroPlayer;

    Vector3 posicaoImpacto;

    private void Awake()
    {
        if (this.gameObject.CompareTag("Player1"))
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
        posicaoLanca = outroPlayer.transform.GetChild(7).transform.GetChild(1).transform.GetChild(0).transform.position;

        if(posicaoLanca.y <= 1.1 || posicaoLanca.y >= 3)
        {
            posicaoLanca = antigaPosicaoLanca;
        }

        antigaPosicaoLanca = posicaoLanca;

        posicaoImpacto = new Vector3(transform.position.x, posicaoLanca.y, transform.position.z);

        transform.position = posicaoImpacto;
    }
}
