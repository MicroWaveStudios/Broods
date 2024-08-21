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

    [Header("Input Action Maps")]
    private string actionMapPlayer = "Player"; 
    private string actionMapUI = "UI";

    bool isPaused = false;
    GameObject gameManager;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }

    private void Start()
    {
        //comentei a linha pois estou editando possiveis erros
        gameManager.GetComponent<GameManager>().SetupPlayer(this.gameObject);
    }

    public void SetupPlayer(int newPlayerID)
    {
        playerID = newPlayerID;

        ConnectPlayer.SetupPlayer(playerID, playerInput);
    }

    public void OnTogglePause(InputAction.CallbackContext context)
    {
        if (!gameManager.GetComponent<GameManager>().GetBooleanIsPaused())
            gameManager.GetComponent<GameManager>().Pause(this.gameObject, true);
        else
            gameManager.GetComponent<GameManager>().Pause(this.gameObject, false);
    }
    public void SetInputActive(bool value)
    {
        switch (value)
        {
            case true:
                playerInput.DeactivateInput();
                break;
            case false:
                playerInput.ActivateInput();
                break;
        }
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
