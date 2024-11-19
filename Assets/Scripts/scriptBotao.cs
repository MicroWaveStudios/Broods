using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.LowLevel;

public class scriptBotao : MonoBehaviour
{
    [Header("Baixo = 0 - Esquerda = 1 - Cima = 2 - Direito = 3")]
    [SerializeField] GameObject[] BotoesProximos;
    [SerializeField] GameObject PrefabJogador;
    GameObject[] Jogador = new GameObject[2];
    [SerializeField] int PersonagemID; //Variavel para saber qual o personagem
    int[] variantePlayer = new int[2];
    bool[] NoBotao = new bool[2];
    public void DestroyPersonagem(int playerIndex)
    {
        if (Jogador[playerIndex] != null)
        {
            Destroy(Jogador[playerIndex]);
        }
    }

    void InstantiateJogador(bool value, int playerIndex)
    {
        if (value && PrefabJogador != null)
        {
            if (Jogador != null) 
            {
                Destroy(Jogador[playerIndex]);
            }
            Jogador[playerIndex] = Instantiate(PrefabJogador, GameObject.FindGameObjectWithTag("PersonagemManager").GetComponent<PersonagensManager>().GetPosicaoInstanciar(playerIndex).position, Quaternion.Euler(0f, 90f, 0f));
            switch (PrefabJogador.name)
            {
                case "NaraOutline Variant":
                    if (playerIndex == 0)
                    {
                        Jogador[playerIndex].transform.localScale = new Vector3(1f, 1f, 1f);
                    }
                    else
                    {
                        Jogador[playerIndex].transform.localScale = new Vector3(1f, 1f, -1);
                    }
                    break;
                case "XimasOutline Variant":
                    if (playerIndex == 0)
                    {
                        Jogador[playerIndex].transform.localScale = new Vector3(0.009999999f, 0.01f, 0.009999999f);
                    }
                    else
                    {
                        Jogador[playerIndex].transform.localScale = new Vector3(0.009999999f, 0.01f, -0.009999999f);
                    }
                    break;
            }
            PersonagensManager personagensManager = GameObject.FindGameObjectWithTag("PersonagemManager").GetComponent<PersonagensManager>();
            MaterialPlayer materialPlayer = Jogador[playerIndex].GetComponent<MaterialPlayer>();
            materialPlayer.SetPlayerID(playerIndex);
            materialPlayer.SetBotao(this.gameObject);
            personagensManager.SetPersonagemID(PersonagemID, playerIndex);
        }
        else
        {
            Destroy(Jogador[playerIndex]);
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

    #region Void's Get/Set
    public void SetJogadorNoBotao(bool value, int playerIndex)
    { 
        NoBotao[playerIndex] = value;
        InstantiateJogador(value, playerIndex);
    }
    public GameObject GetJogador(int playerIndex)
    { 
        return Jogador[playerIndex];
    }
    public void SetVariantePlayer(int newVariantePlayer, int playerIndex)
    {
        variantePlayer[playerIndex] = newVariantePlayer;
    }
    public int GetVariantePlayer(int playerIndex)
    { 
        return variantePlayer[playerIndex];
    }
    public int GetPersonagemID()
    { 
        return PersonagemID;
    }
    #endregion
}
