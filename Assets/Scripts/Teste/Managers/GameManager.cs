using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public int limitPlayer;
    [SerializeField] int playerInScene;
    PlayerController focusedPlayerController;
    private bool isPaused;

    [SerializeField] GameObject ContinueButton;

    public void SetupPlayer(GameObject spawnedPlayer)
    {
        if (playerInScene < limitPlayer)
        {
            spawnedPlayer.GetComponent<PlayerController>().SetupPlayer(playerInScene);

            playerInScene++;
        }
        if (playerInScene == limitPlayer)
            ContinueButton.SetActive(true);
    }

    public void TogglePauseState(GameObject newFocusedPlayerController)
    {
        focusedPlayerController = newFocusedPlayerController.GetComponent<PlayerController>();

        isPaused = !isPaused;

        UpdateActivePlayerInputs(focusedPlayerController);

        SwitchFocusedPlayerControlScheme();

        UIPause();
    }

    public void IsPaused()
    {
        isPaused = !isPaused;
    }

    void UpdateActivePlayerInputs(PlayerController focusedPlayer)
    {
        GameObject notFocusedPlayer;
        switch (focusedPlayer.gameObject.tag) 
        {
            case "Player1":
                notFocusedPlayer = GameObject.FindGameObjectWithTag("Player2");
                notFocusedPlayer.GetComponent<PlayerController>().SetInputActive(isPaused);
                break;
            case "Player2":
                notFocusedPlayer = GameObject.FindGameObjectWithTag("Player1");
                notFocusedPlayer.GetComponent<PlayerController>().SetInputActive(isPaused);
                break;

        }
    }

    void SwitchFocusedPlayerControlScheme()
    {
        switch (isPaused)
        {
            case true:
                focusedPlayerController.EnableMapActionUI();
                break;
            case false:
                focusedPlayerController.EnableMapActionPlayer();
                break;
        }
    }

    public void UIPause()
    {
        GameObject UIManager = GameObject.FindGameObjectWithTag("UIManager");
        UIManager.GetComponent<UIManager>().UIStatePause(isPaused);
    }
}

