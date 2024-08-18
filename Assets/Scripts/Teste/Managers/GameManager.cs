using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public int limitPlayer;
    [SerializeField] int playerInScene;
    PlayerController playerController;
    bool isPaused;

    [SerializeField] GameObject ContinueButton;

    private void Start()
    {
        isPaused = false;
    }

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

    public void TogglePauseState(PlayerController newFocusedPlayerController)
    {
        playerController = newFocusedPlayerController;

        isPaused = !isPaused;

        //ToggleTimeScale();

        //UpdateActivePlayerInputs();

        SwitchFocusedPlayerControlScheme();

        //UpdateUIMenu();

    }

    void SwitchFocusedPlayerControlScheme()
    {
        switch (isPaused)
        {
            case true:
                playerController.EnableMapActionUI();
                break;
            case false:
                playerController.EnableMapActionPlayer();
                break;
        }
    }
}

