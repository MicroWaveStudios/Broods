using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class PersonagensManager : MonoBehaviour
{
    #region Variáveis
    [SerializeField] Image[] spritePersonagens;
    [SerializeField] GameObject[] BotoesDaCena;
    [SerializeField] GameObject eventSystemManager;
    [SerializeField] GameObject BotaoInicial;

    [SerializeField] Transform[] PosicaoInstanciar;

    [SerializeField] Transform[] posicaoPlayer;

    int[] indexPersonagem = new int[] { -1, -1};
    GameObject[] playerGameObject = new GameObject[2];
    int player = 0;

    bool naSelecaoDePersonagem;

    bool[] selecionouPersonagem = new bool[2]; 

    //[System.Serializable]
    //struct Lista
    //{
    //    public GameObject[] posePersonagem;
    //    public GameObject confirmar;
    //}
    //[SerializeField] List<Lista> ListaPose = new List<Lista>();
    #endregion

    public void AlterarCenaPersonagens(bool value)
    {
        naSelecaoDePersonagem = value;
        GameObject.FindGameObjectWithTag("ConnectManager").GetComponent<ConnectPlayerInMenu>().GetPlayer(0).GetComponent<SelecaoPersonagem>().SetActiveBordaSelecao(value);
        GameObject.FindGameObjectWithTag("ConnectManager").GetComponent<ConnectPlayerInMenu>().GetPlayer(1).GetComponent<SelecaoPersonagem>().SetActiveBordaSelecao(value);
    }

    public GameObject GetBotaoInicial()
    {
        return BotaoInicial;
    }

    public void IniciarPainelPersonagem()
    {
        naSelecaoDePersonagem = true;
        playerGameObject[0] = GameObject.FindGameObjectWithTag("ConnectManager").GetComponent<ConnectPlayerInMenu>().GetPlayer(0);
        playerGameObject[1] = GameObject.FindGameObjectWithTag("ConnectManager").GetComponent<ConnectPlayerInMenu>().GetPlayer(1);
    }

    #region Confirmações
    public void Confirmou()
    {
        
    }
    #endregion

    #region Selecionar Personagens
    public void SelecionouPersonagem(int playerIndex, bool value)
    {
        selecionouPersonagem[playerIndex] = value;
    }
    #endregion

    public bool GetNaSelecaoDePersonagem()
    {
        return naSelecaoDePersonagem;
    }
    public void SetNaSelecaoDePersonagem(bool value)
    { 
        naSelecaoDePersonagem = value;
    }

    public Transform GetPosicaoInstanciar(int value)
    {
        return PosicaoInstanciar[value];
    }
}

