using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    GameController gameController;
    CinemachineVirtualCamera virtualCamera;

    [Header("Velocidade do FOV")]
    [SerializeField] float valor;

    [Header("")]
    
    [Header("Valores Iniciais")]
    [SerializeField] float inicialDistance;
    [SerializeField] float inicialAdc;

    [Header("")]

    [Header("Valores à adicionar")]
    [SerializeField] float adicionarAdc; 
    [SerializeField] float adicionarDistance; 

    float adc;
    float minAdc;
    float maxAdc;

    float distance;
    float minDistance; 
    float maxDistance; 

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameController>();
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        minDistance = inicialDistance;
        maxDistance = inicialDistance + adicionarDistance;
        adc = inicialAdc;
    }

    private void Update()
    {
        distance = gameController.GetDistance();
        Verificacao();
    }

    void Calculo(float distance, float adc)
    {
        if (virtualCamera.m_Lens.FieldOfView < distance + adc)
        {
            virtualCamera.m_Lens.FieldOfView += valor;
        }
        if (virtualCamera.m_Lens.FieldOfView > distance + adc)
        {
            virtualCamera.m_Lens.FieldOfView -= valor;
        }
    }
    void Adicionar(float value)
    {
        minDistance += adicionarDistance * value;
        maxDistance += adicionarDistance * value;
        adc += adicionarAdc * value;
    }
    void Verificacao()
    {
        if (distance < minDistance && distance > inicialDistance)
        {
            Adicionar(-1f);
        }
        else if (distance > minDistance && distance < maxDistance)
        {
            Calculo(distance, adc);
        }
        else if (distance > maxDistance)
        {
            Adicionar(1f);
        }
    }
}
