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
    //Mapas de ação PlayerInputs
    private string actionMapPlayer = "Player"; 
    private string actionMapUI = "UI";


    GameObject gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        gameManager.GetComponent<GameManager>().SetupPlayer(this.gameObject);
    }

    public void SetupPlayer(int newPlayerID)
    {
        playerID = newPlayerID;

        ConnectPlayer.SetupPlayer(playerID, playerInput);
    }

    public void OnTogglePause(InputAction.CallbackContext value)
    {
        if (value.started)
            gameManager.GetComponent<GameManager>().TogglePauseState(this);
    }

    public void EnableMapActionPlayer()
    {
        playerInput.SwitchCurrentActionMap(actionMapPlayer);
    }
    public void EnableMapActionUI()
    {
        playerInput.SwitchCurrentActionMap(actionMapUI);
    }
}
