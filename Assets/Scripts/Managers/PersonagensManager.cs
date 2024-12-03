using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;
using TMPro;
using System;

public class PersonagensManager : MonoBehaviour
{
    #region Variaveis
    [SerializeField] ConnectPlayerInMenu connectManager;

    [SerializeField] Image[] spritePersonagens;
    [SerializeField] GameObject[] BotoesDaCena;
    [SerializeField] GameObject eventSystemManager;
    [SerializeField] GameObject BotaoInicial;
    [SerializeField] GameObject txtContinuar;

    [SerializeField] GameObject[] ProntoP;
    [SerializeField] GameObject[] Visual;
    [SerializeField] GameObject[] BordaCor;

    [SerializeField] GameObject[] VisualP1;
    [SerializeField] GameObject[] VisualP2;

    [SerializeField] Transform[] PosicaoInstanciar;

    [SerializeField] Transform[] posicaoPlayer;

    [SerializeField] TMP_Text[] NomePersonagemTxt;

    GameObject[] playerGameObject = new GameObject[2];

    [SerializeField] int[] PersonagemID = new int[2];
    [SerializeField] int[] varianteAtual = new int[2];

    [SerializeField] GameObject[] NomePersonagem;

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
            for (int i = 0; i < 2; i++)
            {
                ProntoP[i].SetActive(value);
                NomePersonagemTxt[i].gameObject.SetActive(value);
                BordaCor[i].SetActive(value);
                Visual[i].SetActive(value);
            }
        }
        naSelecaoDePersonagem = value;
        
        for (int i = 0; i < 2; i++)
        {
            connectManager.GetPlayer(i).GetComponent<SelecaoPersonagem>().SetActiveBordaSelecaoPersonagem(value);
        }
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
        ProntoP[playerIndex].SetActive(value);
        if (value)
        {
            //NomePersonagemTxt[playerIndex].text = "Pronto!";
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
            NomePersonagemTxt[playerIndex].text = nomeVariante[playerIndex];
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

    public void TrocarLocalBordaCor(int index, int variante)
    {
        BordaCor[index].transform.position = Visual[index].transform.GetChild(0).GetChild(variante).position;
    }

    #region Selecionar Personagens

    int[] atualPersonagem = new int[2];
    public void SelecionouPersonagem(int playerIndex, int personagemIndex, bool value)
    {
        selecionouPersonagem[playerIndex] = value;
        if (value)
        {
            atualPersonagem[playerIndex] = personagemIndex;
        }
        if (playerIndex == 0)
        {
            VisualP1[atualPersonagem[playerIndex]].SetActive(value);
        }
        else
        {
            VisualP2[atualPersonagem[playerIndex]].SetActive(value);
        }
        Visual[playerIndex].SetActive(value);
        BordaCor[playerIndex].SetActive(value);
    }
    public void SetActiveNomePersonagem(int playerIndex, bool value)
    {
        NomePersonagemTxt[playerIndex].gameObject.SetActive(value);
        NomePersonagemTxt[playerIndex].transform.GetChild(0).gameObject.SetActive(value);
    }
    #endregion
    string[] nomeVariante = new string[2];
    public void TrocarVarianteTxt(int playerIndex, string nome, string frase)
    {
        if (!confirmouPersonagem[playerIndex])
        {
            //switch (connectManager.GetPlayer(playerIndex).GetComponent<ConnectPlayer>().GetNumeroPersonagem())
            //{
            //    case 0:
            //        NomePersonagem[1].SetActive(false);
            //        NomePersonagem[0].SetActive(true);
            //        break;
            //    case 1:
            //        NomePersonagem[0].SetActive(false);
            //        NomePersonagem[1].SetActive(true);
            //        break;
            //    case -1:
            //        NomePersonagem[0].SetActive(false);
            //        NomePersonagem[1].SetActive(false);
            //        break;
            //}
            nomeVariante[playerIndex] = nome;
            NomePersonagemTxt[playerIndex].text = nomeVariante[playerIndex];
            NomePersonagemTxt[playerIndex].transform.GetChild(0).GetComponent<TMP_Text>().text = frase;
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

