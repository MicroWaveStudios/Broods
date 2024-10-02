using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonagemSprite : MonoBehaviour
{
    GameObject[] botoesProximos = new GameObject[4];
    [SerializeField] float NumeroPersonagem;

    bool TemPlayerAqui;

    public void BotoesProximos(GameObject novoBotao, int localizacao)
    {
        switch (localizacao)
        { 
            case 0:
                if (botoesProximos[localizacao].transform.position.y < novoBotao.transform.position.y)
                {
                    botoesProximos[localizacao] = novoBotao;
                }
                break;
            case 1:
                if (botoesProximos[localizacao].transform.position.x < novoBotao.transform.position.x)
                {
                    botoesProximos[localizacao] = novoBotao;
                }
                break;
            case 2:
                if (botoesProximos[localizacao].transform.position.y > novoBotao.transform.position.y)
                {
                    botoesProximos[localizacao] = novoBotao;
                }
                break;
            case 3:
                if (botoesProximos[localizacao].transform.position.x > novoBotao.transform.position.x)
                {
                    botoesProximos[localizacao] = novoBotao;
                }
                break;
        }
    }

    public void RetornarSprite(GameObject player, int posicaoSprite)
    {
        if (botoesProximos[posicaoSprite] != null)
        {
            GameObject spriteDestino = botoesProximos[posicaoSprite];
            player.GetComponent<NavegacaoSelecaoDePersonagem>().TrocarBotaoAtual(spriteDestino);
            if (TemPlayerAqui)
            {
                
            }
            else
            {

            }
        }
    }
}
