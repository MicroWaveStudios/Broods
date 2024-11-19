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
}

public class ControladorDialogo : MonoBehaviour
{
    public DialogoData dialogoData;

    [SerializeField] float timer;

    [SerializeField] Image spriteMostrar;
    [SerializeField] GameObject tutorialManager;
    [SerializeField] bool treinamento;

    [SerializeField] sceneManager sceneManager;
    [SerializeField] GameObject caixaPreta;

    int textoAtual = 0;
    bool acabouAnimacao = false;
    bool rodouMetodoEscrevendo;
    bool rodouMetodoEsperando;

    AnimacaoTexto textoComAnimacao;
    Dialogo_UI dialogueUI;
    PlayerController playerController;

    ESTADO estado;

    void Awake()
    {
        textoComAnimacao = FindObjectOfType<AnimacaoTexto>();
        dialogueUI = GameObject.FindGameObjectWithTag("CaixaDialogo").GetComponent<Dialogo_UI>();
        playerController = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerController>();
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

    void Escrevendo()
    {
        if (rodouMetodoEscrevendo)
        {
            textoComAnimacao.Skip();
            estado = ESTADO.ESPERANDO;
        }
    }

    public void PularTexto(InputAction.CallbackContext context)
    {
        if (context.started && estado == ESTADO.ESCREVENDO)
        {
            rodouMetodoEscrevendo = true;
            rodouMetodoEsperando = false;
        }

        if (context.started && estado == ESTADO.ESPERANDO)
        {
            rodouMetodoEscrevendo = false;
            rodouMetodoEsperando = true;
        }
    }

    void Esperando()
    {
        if (rodouMetodoEsperando)
        {
            if (!acabouAnimacao)
            {
                Next();
            }
            else
            {
                dialogueUI.Disable();
                textoComAnimacao.texto.gameObject.SetActive(false); 
                textoAtual = 0;
                //playerController.EnableMapActionPlayer();

                //here
                StartCoroutine(StartFadeInFadeOut());
                
                if (!treinamento)
                {
                    tutorialManager.GetComponent<ComandosTutorial>().enabled = true;
                }
            }
        }
    }

    IEnumerator StartFadeInFadeOut()
    {
        caixaPreta.GetComponent<Animator>().SetTrigger("Change");
        yield return new WaitForSeconds(timer);
        sceneManager.GetComponent<sceneManager>().TutorialTeste();
    }
}