using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class scriptBotao : MonoBehaviour
{
    [Header("Baixo = 0 - Esquerda = 1 - Cima = 2 - Direito = 3")]
    [SerializeField] GameObject[] BotoesProximos;
    [SerializeField] GameObject PrefabJogador;
    GameObject Jogador;
    bool[] NoBotao = new bool[2];

    public void SetJogadorNoBotao(bool value, int playerIndex)
    { 
        NoBotao[playerIndex] = value;
        InstantiateJogador(value);
    }

    public GameObject GetPrefabJogador()
    { 
        return PrefabJogador;
    }


    void InstantiateJogador(bool value)
    {
        if (value && PrefabJogador != null)
        {
            Jogador = Instantiate(PrefabJogador, transform.position, Quaternion.identity);
            Jogador.GetComponent<PlayerInput>().enabled = false;
        }
        else
        {
            Destroy(Jogador);
        }
    }

    public void Orientacao(GameObject jogador, Vector2 orientacao)
    {
        switch (orientacao)
        {
            case Vector2 when orientacao.Equals(Vector2.up):
                if (BotoesProximos[2] != null)
                {
                    jogador.GetComponent<SelecaoPersonagem>().AlterarBotaoAtual(BotoesProximos[2]);
                }
                break;
            case Vector2 when orientacao.Equals(Vector2.down):
                if (BotoesProximos[0] != null)
                {
                    jogador.GetComponent<SelecaoPersonagem>().AlterarBotaoAtual(BotoesProximos[0]);
                }
                break;
            case Vector2 when orientacao.Equals(Vector2.right):
                if (BotoesProximos[3] != null)
                {
                    jogador.GetComponent<SelecaoPersonagem>().AlterarBotaoAtual(BotoesProximos[3]);
                }
                break;
            case Vector2 when orientacao.Equals(Vector2.left):
                if (BotoesProximos[1] != null)
                {
                    jogador.GetComponent<SelecaoPersonagem>().AlterarBotaoAtual(BotoesProximos[1]);
                }
                break;
        }
    }
}
