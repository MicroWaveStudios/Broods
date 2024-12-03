using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using UnityEngine.UIElements;
using UnityEngine.UI;
//using Microsoft.Unity.VisualStudio.Editor;

public class piscarTextoJogadores : MonoBehaviour
{
    public estado EstadoAtual;
    public float valorMinimo;
    public float valorMaximo;
    public float valorAtual;
    public TMP_Text txtFrase;

    [SerializeField] GameObject button;

    [SerializeField] bool Botao;


    void Start()
    {
        //valorMinimo = 0.1f;
        //valorMaximo = 1.7f;
        //valorAtual = 1.7f;
        //EstadoAtual = estado.ENCOLHENDO;
    }

    void Update()
    {
        Mudando();
    }

    public enum estado
    {
        ENCOLHENDO,
        AUMENTANDO    
    }

    public void Mudando()
    {
        if (EstadoAtual == estado.ENCOLHENDO)
        {
            valorAtual -= 0.01f;
            if (Botao)
            {
                button.GetComponent<Image>().color = new Color(Color.white.r, Color.white.g, Color.white.b, valorAtual);
            }
            else
            {
                txtFrase.color = new Color(Color.white.r, Color.white.g, Color.white.b, valorAtual);
            }
            
            if(valorAtual <= valorMinimo)
            {
                EstadoAtual = estado.AUMENTANDO;
            }
        }

        if (EstadoAtual == estado.AUMENTANDO)
        {
            valorAtual += 0.01f;
            if (Botao)
            {
                button.GetComponent<Image>().color = new Color(Color.white.r, Color.white.g, Color.white.b, valorAtual);
            }
            else
            {
                txtFrase.color = new Color(Color.white.r, Color.white.g, Color.white.b, valorAtual);
            }

            if (valorAtual >= valorMaximo)
            {
                EstadoAtual = estado.ENCOLHENDO;
            }
        }
    }

}
