using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class limiteScript : MonoBehaviour
{
    [SerializeField] GameObject[] limiteMapa;
    [SerializeField] float maxDistance;
    [SerializeField] float adicionarDistance;
    [SerializeField] float multiDistance;
    GameController gameController;
    float distance;
    float playerLeft;
    float playerRight;
    float[] inicalDistance = new float[2];
    private void Start()
    {
        gameController = GetComponent<GameController>();
        for (int i = 0; i < limiteMapa.Length; i++) 
        { 
            inicalDistance[i] = limiteMapa[i].transform.position.x;
        }
    }

    private void Update()
    {
        distance = gameController.GetDistance();
        playerLeft = gameController.GetPlayerLeft().position.x; // deu erro aqui
        playerRight = gameController.GetPlayerRight().position.x;
        if (distance <= maxDistance) 
        {
            LimiteMapa();
        }
    }
    void LimiteMapa()
    {
        float postionX_0 = playerLeft - adicionarDistance * multiDistance;
        float postionX_1 = playerRight + adicionarDistance * multiDistance;
        limiteMapa[0].transform.position = new Vector3(postionX_0, limiteMapa[0].transform.position.y, limiteMapa[0].transform.position.z);
        limiteMapa[1].transform.position = new Vector3(postionX_1, limiteMapa[1].transform.position.y, limiteMapa[1].transform.position.z);
    }
}
