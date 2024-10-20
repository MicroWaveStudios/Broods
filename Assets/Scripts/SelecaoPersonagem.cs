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

    [SerializeField] GameObject personagensManager;

    GameObject botaoAtual;

    private void Awake()
    {
        personagensManager = GameObject.FindGameObjectWithTag("PersonagemManager");
    }

    private void Start()
    {
        //personagensManager.GetComponent<PersonagensManager>().
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
        textoBorda.GetComponent<Transform>().position = PosicaoTexto[playerIndex];
        textoBorda.color = CorBorda[playerIndex];
        textoBorda.text = "P" + playerText;

        //BordaSelecaoPersonagem.GetComponentInChildren<TextMeshPro>().text = "P" + playerText;
        //BordaSelecaoPersonagem.GetComponentInChildren<TextMeshPro>().color = CorBorda[playerIndex];
    }

    public void AlterarBotaoAtual(GameObject novoBotaoAtual)
    {
        int playerIndex = GetComponent<ConnectPlayer>().GetPlayerID();
        if (botaoAtual != null)
        {
            botaoAtual.GetComponent<scriptBotao>().SetJogadorNoBotao(false, playerIndex);
        }
        botaoAtual = novoBotaoAtual;
        BordaSelecaoPersonagem.transform.position = botaoAtual.transform.position;
        botaoAtual.GetComponent<scriptBotao>().SetJogadorNoBotao(true, playerIndex);
    }

    public void Confirmar(InputAction.CallbackContext context)
    {
        if (context.started && botaoAtual.GetComponent<scriptBotao>().GetPrefabJogador() != null && GameObject.FindGameObjectWithTag("PersonagemManager").GetComponent<PersonagensManager>().GetNaSelecaoDePersonagem())
        {

        }
    }

    public void Navegacao(InputAction.CallbackContext context)
    {
        Vector2 orientacao = context.ReadValue<Vector2>();
        if (orientacao != Vector2.zero && personagensManager.GetComponent<PersonagensManager>().GetComponent<PersonagensManager>().GetNaSelecaoDePersonagem())
        {
            botaoAtual.GetComponent<scriptBotao>().Orientacao(this.gameObject, orientacao);
        }
    }

}
