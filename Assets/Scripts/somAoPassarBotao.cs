using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class somAoPassarBotao : MonoBehaviour
{
    [SerializeField] AudioSource som;

    public void Tocar()
    {
        som.Play();
    }
}