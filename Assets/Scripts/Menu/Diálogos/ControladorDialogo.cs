using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum ESTADO
{
    DESABILITADO,
    ESPERANDO,
    ESCREVENDO,
    JOGANDO
}

public class ControladorDialogo : MonoBehaviour
{
    public DialogoData dialogoData;

    int textoAtual = 0;
    bool acabouAnimacao = false;
    GameObject CaixaDialogo;
    [SerializeField] Image spriteMostrar;
    [SerializeField] GameObject painelFimTutorial;
    [SerializeField] GameObject instrucoes;

    AnimacaoTexto textoComAnimacao;
    Dialogo_UI dialogueUI;
    AudioSource somTexto;
    PlayerMoveRigidbody playerMove;
    PlayerCombat playerCombat;
    PlayerStats playerStats;

    [SerializeField] private List<GameObject> acao = new List<GameObject>();
    ESTADO estado;

    void Awake()
    {
        textoComAnimacao = FindObjectOfType<AnimacaoTexto>();
        dialogueUI = FindObjectOfType<Dialogo_UI>();
        somTexto = FindObjectOfType<AudioSource>();
        playerMove = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerMoveRigidbody>();
        playerStats = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerStats>();
        playerCombat = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerCombat>();
        CaixaDialogo = dialogueUI.gameObject;
        textoComAnimacao.acabouDeEscrever = AnimacaoDeEscreverTerminou;
    }

    void Start()
    {
        estado = ESTADO.DESABILITADO;
        Next();
    }

    void Update()
    {
        if (estado == ESTADO.DESABILITADO) return;

        switch (estado)
        {
            case ESTADO.ESPERANDO:
                Esperando();
                break;
            case ESTADO.ESCREVENDO:
                Escrevendo();
                break;
            case ESTADO.JOGANDO:
                Jogando();
                break;
        }
    }

    public void Next()
    {
        if (textoAtual == 0)
        {
            dialogueUI.Enable();
        }

        dialogueUI.SetName(dialogoData.scriptFalas[textoAtual].nome);
        dialogueUI.SetIcon(dialogoData.scriptFalas[textoAtual].icon);

        textoComAnimacao.textoCompleto = dialogoData.scriptFalas[textoAtual].text;
        spriteMostrar.sprite = dialogoData.scriptFalas[textoAtual].icon;

        if (textoAtual == dialogoData.scriptFalas.Count - 1)
        {
            acabouAnimacao = true;
        }

        textoComAnimacao.comecouEscrever();
        textoAtual++;

        estado = ESTADO.ESCREVENDO;
    }

    void AnimacaoDeEscreverTerminou()
    {
        estado = ESTADO.ESPERANDO;
    }

    void Esperando()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!acabouAnimacao)
            {
                Next();
            }

            else
            {
                dialogueUI.Disable();
                estado = ESTADO.JOGANDO;
                textoAtual = 0;
                acabouAnimacao = false;           
            }
        }
    }

    void Escrevendo()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            textoComAnimacao.Skip();
            estado = ESTADO.ESPERANDO;
        }
    }

    void Jogando()
    {
        instrucoes.SetActive(true);

        if (playerMove.GetDirecaoX() == 1f)
        {
            //acao[0].fontStyle = FontStyles.Strikethrough;
            Destroy(acao[0]);
        }

        if (playerMove.GetDirecaoX() == -1f)
        {
            //acao[1].fontStyle = FontStyles.Strikethrough;
            Destroy(acao[1]);
        }

        if (!playerMove.GetNoChao())
        {
            //acao[2].fontStyle = FontStyles.Strikethrough;
            Destroy(acao[2]);
        }

        if (playerMove.GetEstaAgachado())
        {
            //acao[3].fontStyle = FontStyles.Strikethrough;
            Destroy(acao[3]);
        }

        if (playerCombat.GetAtacouLeve())
        {
            //acao[4].fontStyle = FontStyles.Strikethrough;
            Destroy(acao[4]);
        }

        if (playerCombat.GetAtacouMedio())
        {
            //acao[5].fontStyle = FontStyles.Strikethrough;
            Destroy(acao[5]);
        }

        if (playerCombat.GetAtacouPesado())
        {
            //acao[6].fontStyle = FontStyles.Strikethrough;
            Destroy(acao[6]);
        }

        if (playerCombat.GetAtacouBaixo())
        {
            //acao[7].fontStyle = FontStyles.Strikethrough;
            Destroy(acao[7]);
        }

        if (instrucoes.transform.childCount == 0)
        {
            StartCoroutine(Acabou());
        }
    }
    private IEnumerator Acabou()
    {
        instrucoes.SetActive(false);
        yield return new WaitForSeconds(1f);
        painelFimTutorial.SetActive(true);
        estado = ESTADO.DESABILITADO;
    }
}