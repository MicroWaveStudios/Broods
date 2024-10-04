using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class AnimacaoTexto : MonoBehaviour
{
    public Action acabouDeEscrever;

    public float delay = 0.05f;
    public TMP_Text texto;

    public string textoCompleto;

    Coroutine coroutine;
    AudioSource somTexto;

    private void Awake()
    {
        somTexto = GetComponent<AudioSource>();
    }
    public void comecouEscrever()
    {
        coroutine = StartCoroutine(Escrevendo());
    }

    IEnumerator Escrevendo()
    {
        texto.text = textoCompleto;
        texto.maxVisibleCharacters = 0;

        for (int i = 0; i <= texto.text.Length; i++)
        {
            texto.maxVisibleCharacters = i;
            yield return new WaitForSeconds(delay);
            somTexto.Play();
        }

        acabouDeEscrever?.Invoke();
    }

    public void Skip()
    {
        StopCoroutine(coroutine);
        texto.maxVisibleCharacters = texto.text.Length;
    }
}