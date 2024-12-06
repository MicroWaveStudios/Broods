using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
//using UnityEditor.Experimental.GraphView;

public class SelecaoPersonagem : MonoBehaviour
{
    [SerializeField] GameObject[] BordaSelecaoPersonagem;
    [SerializeField] GameObject[] BordaSelecaoCor;
    //[SerializeField] TMP_Text textoBorda;
    //[SerializeField] Color[] CorBorda;
    //[SerializeField] Vector3[] PosicaoTexto;
    [SerializeField] AudioSource PassouPorCima;
    [SerializeField] AudioSource SelecionouBonecoErrado;
    [SerializeField] AudioSource SelecionouCerto;
    [SerializeField] AudioSource SelecionouTudo;
 
    GameObject personagensManager;
    GameObject connectManager;
    PersonagensManager personagensManagerScr;

    GameObject botaoAtual;

    int playerID;

    private void Awake()
    {
        personagensManager = GameObject.FindGameObjectWithTag("PersonagemManager");
        connectManager = GameObject.FindGameObjectWithTag("ConnectManager");
    }
    public void SetActiveBordaSelecaoPersonagem(bool value)
    {
        if (value && botaoAtual == null)
        {
            AlterarBotaoAtual(personagensManager.GetComponent<PersonagensManager>().GetBotaoInicial());
        }
        else if (value)
        {
            botaoAtual.GetComponent<scriptBotao>().InstantiateJogador(true, playerID);
        }
    }

    public GameObject GetBordaCor()
    {
        return BordaSelecaoCor[playerID];
    }
    public void TrocarCorDaBorda(int playerIndex)
    {
        playerID = playerIndex;
        //BordaSelecaoPersonagem.GetComponent<SpriteRenderer>().color = CorBorda[playerIndex];
        //int playerText = playerIndex + 1;
        //textoBorda.rectTransform.position = PosicaoTexto[playerIndex];
        //textoBorda.color = CorBorda[playerIndex];
        //textoBorda.text = "P" + playerText;
    }

    public void AlterarBotaoAtual(GameObject novoBotaoAtual)
    {
        int playerIndex = GetComponent<ConnectPlayer>().GetPlayerID();
        if (botaoAtual != null)
        {
            botaoAtual.GetComponent<scriptBotao>().SetJogadorNoBotao(false, playerIndex);
            PassouPorCima.Play();
        }
        botaoAtual = novoBotaoAtual;
        transform.position = novoBotaoAtual.transform.position;
        personagensManager.GetComponent<PersonagensManager>().SetActiveNomePersonagem(playerID, true);
        if (botaoAtual.GetComponent<scriptBotao>().GetPersonagemID() == -1)
        {
            personagensManager.GetComponent<PersonagensManager>().SetActiveNomePersonagem(playerID, false);
        }
        botaoAtual.GetComponent<scriptBotao>().SetJogadorNoBotao(true, playerIndex);
        personagensManager.GetComponent<PersonagensManager>().SetPositionBordaSelecaoPersonagem(novoBotaoAtual, playerID);
    }
    public void Confirmar(InputAction.CallbackContext context)
    {
        if (context.started && personagensManager.GetComponent<PersonagensManager>().GetNaSelecaoDePersonagem())
        {
            if (botaoAtual != null)
            {
                if (personagensManager.GetComponent<PersonagensManager>().GetSelecionouPersonagem(playerID) && !personagensManager.GetComponent<PersonagensManager>().GetConfirmouPersonagem(playerID))
                {
                    personagensManager.GetComponent<PersonagensManager>().ConfirmouPersonagem(playerID, botaoAtual.GetComponent<scriptBotao>().GetJogador(playerID).GetComponent<MaterialPlayer>().GetMaterialAtual(), true);
                    SelecionouCerto.Play();
                }
                else if (botaoAtual.GetComponent<scriptBotao>().GetJogador(playerID) != null && !personagensManager.GetComponent<PersonagensManager>().GetConfirmouPersonagem(playerID))
                {
                    personagensManager.GetComponent<PersonagensManager>().SelecionouPersonagem(playerID, botaoAtual.GetComponent<scriptBotao>().GetPersonagemID(), true);
                    SelecionouTudo.Play();
                }
            }
        }
    }
    public void Voltar(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            connectManager.GetComponent<ConnectPlayerInMenu>().Voltar(GetComponent<ConnectPlayer>().GetPlayerID());
        }
    }
    public void Navegacao(InputAction.CallbackContext context)
    {
        Vector2 orientacao = context.ReadValue<Vector2>();
        if (orientacao != Vector2.zero)
        {
            if (personagensManager.GetComponent<PersonagensManager>().GetNaSelecaoDePersonagem() && !personagensManager.GetComponent<PersonagensManager>().GetConfirmouPersonagem(playerID))
            {
                if (personagensManager.GetComponent<PersonagensManager>().GetSelecionouPersonagem(playerID))
                {
                    GetPersonagem().GetComponent<MaterialPlayer>().TrocarMaterial(context.ReadValue<Vector2>().x);
                    PassouPorCima.Play();
                }
                else
                {
                    botaoAtual.GetComponent<scriptBotao>().Orientacao(this.gameObject, orientacao);
                }
            }
        }
    }

    #region Void's Get/Set
    public GameObject GetBotaoAtual()
    { 
        return botaoAtual;
    }
    public GameObject GetPersonagem()
    {
        return botaoAtual.GetComponent<scriptBotao>().GetJogador(GetComponent<ConnectPlayer>().GetPlayerID());
    }
    #endregion

}
