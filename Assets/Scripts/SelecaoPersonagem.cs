using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class SelecaoPersonagem : MonoBehaviour
{
    [SerializeField] GameObject BordaSelecaoPersonagem;
    [SerializeField] TMP_Text textoBorda;
    [SerializeField] Color[] CorBorda;
    [SerializeField] Vector3[] PosicaoTexto;

    GameObject personagensManager;
    GameObject connectManager;
    PersonagensManager personagensManagerScr;

    GameObject botaoAtual;

    private void Awake()
    {
        personagensManager = GameObject.FindGameObjectWithTag("PersonagemManager");
        connectManager = GameObject.FindGameObjectWithTag("ConnectManager");
    }
    public void SetActiveBordaSelecao(bool value)
    {
        BordaSelecaoPersonagem.SetActive(value);
        if (value)
        {
            AlterarBotaoAtual(personagensManager.GetComponent<PersonagensManager>().GetBotaoInicial());
        }
    }

    public void TrocarCorDaBorda(int playerIndex)
    {
        BordaSelecaoPersonagem.GetComponent<SpriteRenderer>().color = CorBorda[playerIndex];
        int playerText = playerIndex + 1;
        textoBorda.rectTransform.position = PosicaoTexto[playerIndex];
        textoBorda.color = CorBorda[playerIndex];
        textoBorda.text = "P" + playerText;
    }

    public void AlterarBotaoAtual(GameObject novoBotaoAtual)
    {
        int playerIndex = GetComponent<ConnectPlayer>().GetPlayerID();
        if (botaoAtual != null)
        {
            botaoAtual.GetComponent<scriptBotao>().SetJogadorNoBotao(false, playerIndex);
        }
        botaoAtual = novoBotaoAtual;
        transform.position = novoBotaoAtual.transform.position;
        botaoAtual.GetComponent<scriptBotao>().SetJogadorNoBotao(true, playerIndex);
    }

    public GameObject GetBotaoAtual()
    { 
        return botaoAtual;
    }
    public GameObject GetPersonagem()
    {
        return botaoAtual.GetComponent<scriptBotao>().GetJogador(GetComponent<ConnectPlayer>().GetPlayerID());
    }
    public void Confirmar(InputAction.CallbackContext context)
    {
        if (context.started && botaoAtual != null && personagensManager.GetComponent<PersonagensManager>().GetNaSelecaoDePersonagem())
        {
            if (personagensManager.GetComponent<PersonagensManager>().GetSelecionouPersonagem(GetComponent<ConnectPlayer>().GetPlayerID()))
            {
                personagensManager.GetComponent<PersonagensManager>().ConfirmouPersonagem(GetComponent<ConnectPlayer>().GetPlayerID(), true);
            }
            else if (botaoAtual.GetComponent<scriptBotao>().GetJogador(GetComponent<ConnectPlayer>().GetPlayerID()) != null)
            {
                personagensManager.GetComponent<PersonagensManager>().SelecionouPersonagem(GetComponent<ConnectPlayer>().GetPlayerID(), true);
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
            if (personagensManager.GetComponent<PersonagensManager>().GetNaSelecaoDePersonagem() && !personagensManager.GetComponent<PersonagensManager>().GetConfirmouPersonagem(GetComponent<ConnectPlayer>().GetPlayerID()))
            {
                if (personagensManager.GetComponent<PersonagensManager>().GetSelecionouPersonagem(GetComponent<ConnectPlayer>().GetPlayerID()))
                {
                    GetPersonagem().GetComponent<MaterialPlayer>().TrocarMaterial(context.ReadValue<Vector2>().x);
                }
                else
                {
                    botaoAtual.GetComponent<scriptBotao>().Orientacao(this.gameObject, orientacao);
                }
            }
        }
    }

}
