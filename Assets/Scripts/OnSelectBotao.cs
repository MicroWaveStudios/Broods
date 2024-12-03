using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class OnSelectBotao : MonoBehaviour, ISelectHandler
{
    [SerializeField] AudioSource somBotao;
    public void OnSelect (BaseEventData eventData)
    {
        somBotao.Play();
    }
}
