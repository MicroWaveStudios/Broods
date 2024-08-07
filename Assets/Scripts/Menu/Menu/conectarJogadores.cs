using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class conectarJogadores : MonoBehaviour
{
    [SerializeField] GameObject conect1;
    [SerializeField] GameObject conect2;
    [SerializeField] GameObject teclado1;
    [SerializeField] GameObject teclado2;
    [SerializeField] GameObject controle1;
    [SerializeField] GameObject controle2;
    [SerializeField] GameObject texto1;
    [SerializeField] GameObject texto2;
    [SerializeField] GameObject Continuar;
    bool conectou1;
    bool conectou2;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ConectouTeclado1();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            ConectouControle1();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            ConectouTeclado2();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            ConectouControle2();
        }

        if (conectou1 && conectou2)
        {
            Continuar.SetActive(true);
        }
    }

    void ConectouTeclado1()
    {
        controle1.SetActive(false);
        conect1.SetActive(true);
        teclado1.SetActive(true);
        texto1.SetActive(false);
    }

    void ConectouControle1()
    {
        teclado1.SetActive(false);
        conect1.SetActive(true);
        controle1.SetActive(true);
        texto1.SetActive(false);
    }

    void ConectouTeclado2()
    {
        controle2.SetActive(false);
        conect2.SetActive(true);
        teclado2.SetActive(true);
        texto2.SetActive(false);
    }

    void ConectouControle2()
    {
        teclado2.SetActive(false);
        conect2.SetActive(true);
        controle2.SetActive(true);
        texto2.SetActive(false);
    }
}
