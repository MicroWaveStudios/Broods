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
    int textoAtual = 0;
    bool acabouAnimacao = false;

    AnimacaoTexto textoComAnimacao;
    Dialogo_UI dialogueUI;

    ESTADO estado;

    void Awake()
    {
        textoComAnimacao = FindObjectOfType<AnimacaoTexto>();
        dialogueUI = FindObjectOfType<Dialogo_UI>();
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
        if (Input.GetKeyDown(KeyCode.Return))
        {
            textoComAnimacao.Skip();
            estado = ESTADO.ESPERANDO;
        }
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
                textoAtual = 0;
                //acabouAnimacao = false;
                tutorialManager.GetComponent<ComandosTutorial>().enabled = true;
            }
        }
    }
}