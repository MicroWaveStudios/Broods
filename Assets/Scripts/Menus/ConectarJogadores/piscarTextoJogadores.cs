using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class piscarTextoJogadores : MonoBehaviour
{
    public estado EstadoAtual;
    public float valorMinimo;
    public float valorMaximo;
    public float valorAtual;
    public TMP_Text txtFrase;


    void Start()
    {
        valorMinimo = 0.1f;
        valorMaximo = 1.5f;
        valorAtual = 1.5f;
        EstadoAtual = estado.ENCOLHENDO;
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
            txtFrase.color = new Color(Color.white.r, Color.white.g, Color.white.b, valorAtual);
            
            if(valorAtual <= valorMinimo)
            {
                EstadoAtual = estado.AUMENTANDO;
            }
        }

        if (EstadoAtual == estado.AUMENTANDO)
        {
            valorAtual += 0.01f;
            txtFrase.color = new Color(Color.white.r, Color.white.g, Color.white.b, valorAtual);

            if (valorAtual >= valorMaximo)
            {
                EstadoAtual = estado.ENCOLHENDO;
            }
        }
    }

}
