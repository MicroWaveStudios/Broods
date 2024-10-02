using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NavegacaoSelecaoDePersonagem : MonoBehaviour
{
    [SerializeField] Material Player1;
    [SerializeField] Material Player2;
    [SerializeField] GameObject BordaSelecao;
    GameObject botaoAtual;
    [SerializeField] ConnectPlayer connectPlayer;
    GameObject PersonagensManager;
    GameObject[] Personagens;

    PlayerInput playerInput;

    private void Start()
    {
        BordaSelecao.SetActive(false);
        playerInput = GetComponent<PlayerInput>();
        PersonagensManager = GameObject.FindGameObjectWithTag("PersonagensManager");
        if (gameObject.tag == "Player1")
        {
            BordaSelecao.GetComponent<Renderer>().material = Player1;
        }
        else
        {
            BordaSelecao.GetComponent<Renderer>().material = Player2;
        }
    }

    public void CenaDePersonagem()
    {
        BordaSelecao.SetActive(true);
    }
    public void EnableMapActionPersonagem()
    {
        playerInput.SwitchCurrentActionMap("Personagem");
        BordaSelecao.SetActive(true);
    }
    public void TrocarBotaoAtual(GameObject novoBotao)
    { 
        botaoAtual = novoBotao;
        transform.position = botaoAtual.transform.position;
    }

    void MudarBotao(int localizacao)
    {
        botaoAtual.GetComponent<PersonagemSprite>().RetornarSprite(this.gameObject, localizacao);
    }

    public void Submit(InputAction.CallbackContext context)
    {
        botaoAtual.GetComponent<Botao>().Interagir();
    }
    public void Cancel(InputAction.CallbackContext context)
    {
        botaoAtual.GetComponent<Botao>().Interagir();
    }

    public void Cima(InputAction.CallbackContext context)
    {
        MudarBotao(2);
    }

    public void Baixo(InputAction.CallbackContext context)
    {
        MudarBotao(0);
    }

    public void Esquerda(InputAction.CallbackContext context)
    {
        MudarBotao(1);
    }
    public void Direita(InputAction.CallbackContext context)
    {
        MudarBotao(3);
    }
}
