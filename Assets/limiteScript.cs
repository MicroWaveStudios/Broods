using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class limiteScript : MonoBehaviour
{
    [SerializeField] GameObject[] limiteMapa;
    [SerializeField] float maxDistance;
    GameController gameController;
    float distance;
    private void Start()
    {
        gameController = GetComponent<GameController>();
    }

    private void Update()
    {
        distance = gameController.GetDistance();
        if (distance > maxDistance) 
        {
            
        }
    }
}
