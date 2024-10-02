using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FumacaChao : MonoBehaviour
{
    [SerializeField] Transform posicaoFumaca;
    public bool Travado = false;

    GameObject player;
    PlayerMoveRigidbody moveRigidbody;

    Vector3 posicaoTravada;

    private void Awake()
    {
        player = transform.parent.gameObject;
        moveRigidbody = player.GetComponent<PlayerMoveRigidbody>();
    }

    private void Update()
    {
        Travado = moveRigidbody.GetNoChao();

        

        if (Travado != true)
        {
            transform.position = posicaoTravada;
        }
        else
        {
            posicaoTravada = transform.position;
            transform.position = posicaoFumaca.position;
        }        
    }



}
