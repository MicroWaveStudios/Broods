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

    [SerializeField] Image spriteMostrar;
    [SerializeField] GameObject tutorialManager;
    [SerializeField] bool treinamento;

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
        dialogueUI = FindObjectOfType<Dialogo_UI>();
        textoComAnimacao.acabouDeEscrever = AnimacaoDeEscreverTerminou;
        playerController = FindObjectOfType<PlayerController>();
    }

    void Start()
    {
        playerController.EnableMapActionUI();
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
                textoAtual = 0;
                playerController.EnableMapActionPlayer();

                if (!treinamento)
                {
                    tutorialManager.GetComponent<ComandosTutorial>().enabled = true;
                }
            }
        }
    }
}