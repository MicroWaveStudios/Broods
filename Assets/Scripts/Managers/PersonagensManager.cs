using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;
using TMPro;

public class PersonagensManager : MonoBehaviour
{
    #region Variáveis
    [SerializeField] ConnectPlayerInMenu connectManger;

    [SerializeField] Image[] spritePersonagens;
    [SerializeField] GameObject[] BotoesDaCena;
    [SerializeField] GameObject eventSystemManager;
    [SerializeField] GameObject BotaoInicial;
    [SerializeField] GameObject txtContinuar;

    [SerializeField] Transform[] PosicaoInstanciar;

    [SerializeField] Transform[] posicaoPlayer;

    [SerializeField] TMP_Text[] VarianteTxt;

    int[] indexPersonagem = new int[] { -1, -1};
    GameObject[] playerGameObject = new GameObject[2];
    int player = 0;

    bool naSelecaoDePersonagem;

    bool[] selecionouPersonagem = new bool[2]; 
    bool[] confirmouPersonagem = new bool[2];

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
        if (value == true)
        {
            connectManger.SetEtapaSelecaoMapa();
        }
        else
        {
            connectManger.SetEtapaConectar();
        }
        naSelecaoDePersonagem = value;
        GameObject.FindGameObjectWithTag("ConnectManager").GetComponent<ConnectPlayerInMenu>().GetPlayer(0).GetComponent<SelecaoPersonagem>().SetActiveBordaSelecao(value);
        GameObject.FindGameObjectWithTag("ConnectManager").GetComponent<ConnectPlayerInMenu>().GetPlayer(1).GetComponent<SelecaoPersonagem>().SetActiveBordaSelecao(value);
    }

    public GameObject GetBotaoInicial()
    {
        return BotaoInicial;
    }
    #region Confirmações
    public void ConfirmouPersonagem(int playerIndex, bool value)
    {
        if (value)
        {
            for (int i = 0; i < 1; i++)
            {
                if (playerIndex != i)
                {
                    //GetComponent<ConnectPlayerInMenu>().GetPlayer(playerIndex).GetComponent<SelecaoPersonagem>().GetBotaoAtual().GetComponent<MaterialPlayer>().SetMaterialOutroPlayer(NumeroDaVariante);
                }
            }
            VarianteTxt[playerIndex].text = "Pronto!";
        }
        else
        {
            VarianteTxt[playerIndex].text = nomeVariante[playerIndex];
        }
        confirmouPersonagem[playerIndex] = value;
        if (confirmouPersonagem[0] && confirmouPersonagem[1])
        {
            txtContinuar.SetActive(true);
            eventSystemManager.GetComponent<EventSystemManager>().SetCurrentSelectedButton(txtContinuar);
        }
        else
        {
            txtContinuar.SetActive(false);
        }
    }
    #endregion

    #region Selecionar Personagens
    public void SelecionouPersonagem(int playerIndex, bool value)
    {
        selecionouPersonagem[playerIndex] = value;
        VarianteTxt[playerIndex].gameObject.SetActive(value);
    }
    #endregion
    string[] nomeVariante = new string[2];
    public void TrocarVarianteTxt(int playerIndex, string Variante)
    {
        if (!confirmouPersonagem[playerIndex])
        {
            nomeVariante[playerIndex] = Variante;
            VarianteTxt[playerIndex].text = nomeVariante[playerIndex];
        }
    }
    public void DestroyPersonagens()
    {
        connectManger.GetPlayer(0).GetComponent<SelecaoPersonagem>().GetBotaoAtual().GetComponent<scriptBotao>().DestroyPersonagem(0);
        connectManger.GetPlayer(1).GetComponent<SelecaoPersonagem>().GetBotaoAtual().GetComponent<scriptBotao>().DestroyPersonagem(1);
    }
    public bool GetNaSelecaoDePersonagem()
    {
        return naSelecaoDePersonagem;
    }
    public void SetNaSelecaoDePersonagem(bool value)
    { 
        naSelecaoDePersonagem = value;
    }
    public bool GetSelecionouPersonagem(int playerIndex)
    {
        return selecionouPersonagem[playerIndex];
    }
    public bool GetConfirmouPersonagem(int playerIndex)
    {
        return confirmouPersonagem[playerIndex];
    }
    public Transform GetPosicaoInstanciar(int value)
    {
        return PosicaoInstanciar[value];
    }
}

