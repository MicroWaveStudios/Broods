using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputPlayerManager : MonoBehaviour
{
    GameObject Player1;
    [SerializeField] GameObject[] PrefabPlayer;
    [SerializeField] Transform[] InstancePosition;
    PlayerInputManager playerInputManager;
    int x;

    private void Awake()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
    }

    private void Update()
    {
        Player1 = GameObject.FindGameObjectWithTag("Player1");
        if (Player1 != null)
        {
            PrefabPlayer[1].tag = "Player2";
            PrefabPlayer[1].GetComponent<Transform>().position = InstancePosition[1].position;
            playerInputManager.playerPrefab = PrefabPlayer[1];
        }
        else
        {
            PrefabPlayer[0].tag = "Player1";
            PrefabPlayer[0].GetComponent<Transform>().position = InstancePosition[0].position;
            playerInputManager.playerPrefab = PrefabPlayer[0];
        }
    }



    public void JoinPlayer(PlayerInput input)
    {
        if (x == 0)
            PrefabPlayer[x].tag = "Player1";
        else
            PrefabPlayer[x].tag = "Player2";
        PrefabPlayer[x].GetComponent<Transform>().position = InstancePosition[x].position;
        playerInputManager.playerPrefab = PrefabPlayer[x];
        x++;
    }
}
