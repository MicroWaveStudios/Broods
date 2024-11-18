using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;
using TMPro;

public class PersonagensManager : MonoBehaviour
{
    #region Variaveis
    [SerializeField] ConnectPlayerInMenu connectManager;

    [SerializeField] Image[] spritePersonagens;
    [SerializeField] GameObject[] BotoesDaCena;
    [SerializeField] GameObject eventSystemManager;
    [SerializeField] GameObject BotaoInicial;
    [SerializeField] GameObject txtContinuar;

    [SerializeField] Transform[] PosicaoInstanciar;

    [SerializeField] Transform[] posicaoPlayer;

    [SerializeField] TMP_Text[] VarianteTxt;

    GameObject[] playerGameObject = new GameObject[2];

    [SerializeField] int[] PersonagemID = new int[2];
    [SerializeField] int[] varianteAtual = new int[2];

    bool naSelecaoDePersonagem;

    bool[] selecionouPersonagem = new bool[2]; 
    bool[] confirmouPersonagem = new bool[2];
    #endregion

    private void Start()
    {
        for (int i = 0; i < 1; i++)
        {
            selecionouPersonagem[i] = false;
            confirmouPersonagem[i] = false;
            varianteAtual[i] = -1;
        }
    }

    public void AlterarCenaPersonagens(bool value)
    {
        if (value == true)
        {
            connectManager.SetEtapaSelecaoPersonagem();
        }
        else
        {
            connectManager.SetEtapaConectar();
        }
        naSelecaoDePersonagem = value;
        connectManager.GetPlayer(0).GetComponent<SelecaoPersonagem>().SetActiveBordaSelecao(value);
        connectManager.GetPlayer(1).GetComponent<SelecaoPersonagem>().SetActiveBordaSelecao(value);
    }
    public void SetPersonagemID(int personagemIndex, int playerIndex)
    {
        PersonagemID[playerIndex] = personagemIndex;
    }
    public int GetPersonagemID(int playerIndex)
    {
        return PersonagemID[playerIndex];
    }
    public GameObject GetBotaoInicial()
    {
        return BotaoInicial;
    }
    public void SetVariante(int playerIndex, int newVariante)
    {
        varianteAtual[playerIndex] = newVariante;
    }
    public int GetVariante(int playerIndex)
    {
        return varianteAtual[playerIndex];
    }
    #region Confirmacoes
    public void ConfirmouPersonagem(int playerIndex, int material, bool value)
    {
        varianteAtual[playerIndex] = material;
        confirmouPersonagem[playerIndex] = value;
        if (value)
        {
            VarianteTxt[playerIndex].text = "Pronto!";
            connectManager.GetPlayer(playerIndex).GetComponent<ConnectPlayer>().SetNumeroPersonagem(PersonagemID[playerIndex]);
            SetVariante(playerIndex, material);
            for (int i = 0; i < 2; i++)
            {
                if (playerIndex != i && GetPersonagemID(playerIndex) == GetPersonagemID(i) && GetVariante(playerIndex) == connectManager.GetPlayer(i).GetComponent<SelecaoPersonagem>().GetBotaoAtual().GetComponent<scriptBotao>().GetJogador(i).GetComponent<MaterialPlayer>().GetMaterialAtual())
                {
                    connectManager.GetPlayer(i).GetComponent<SelecaoPersonagem>().GetBotaoAtual().GetComponent<scriptBotao>().GetJogador(i).GetComponent<MaterialPlayer>().TrocarMaterial(1);
                }
            }
        }
        else
        {
            VarianteTxt[playerIndex].text = nomeVariante[playerIndex];
        }
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
    public void SelecionouPersonagem(int playerIndex, int personagemIndex, bool value)
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
        connectManager.GetPlayer(0).GetComponent<SelecaoPersonagem>().GetBotaoAtual().GetComponent<scriptBotao>().DestroyPersonagem(0);
        connectManager.GetPlayer(1).GetComponent<SelecaoPersonagem>().GetBotaoAtual().GetComponent<scriptBotao>().DestroyPersonagem(1);
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

