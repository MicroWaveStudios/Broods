using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Player ID
    private int playerID;

    [Header("Sub Behaviors")]
    public ConnectPlayer ConnectPlayer;

    [Header("Input Settings")]
    public PlayerInput playerInput;

    private string actionMapPlayer = "Player";
    private string actionMapUI = "UI";

    private void Start()
    {
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        gameController.GetComponent<GameManager>().SetupPlayer(this.gameObject);
    }

    public void SetupPlayer(int newPlayerID)
    {
        playerID = newPlayerID;

        ConnectPlayer.SetupPlayer(playerID, playerInput);
    }
}
