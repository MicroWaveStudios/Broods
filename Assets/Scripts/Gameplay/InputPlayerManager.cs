using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.iOS;

public class InputPlayerManager : MonoBehaviour
{
    GameObject Player1;
    [SerializeField] GameObject[] PrefabPlayer;
    [SerializeField] Transform[] InstancePosition;
    PlayerInputManager playerInputManager;
    PlayerInput playerInput;

    [Header("Split-Keyboard")]
    public bool EnableSplitKeyboard;

    private void Awake()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
        if (EnableSplitKeyboard)
            playerInputManager.enabled = false;
    }

    private void Start()
    {
        if (EnableSplitKeyboard)
        {
            var p1 = PlayerInput.Instantiate(PrefabPlayer[0], playerIndex: 0, controlScheme: "KeyboardLeft", pairWithDevice: Keyboard.current);
            var p2 = PlayerInput.Instantiate(PrefabPlayer[1], playerIndex: 1, controlScheme: "KeyboardRight", pairWithDevice: Keyboard.current);
        }
    }

    private void Update()
    {
        if (!EnableSplitKeyboard) 
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
    }
}
