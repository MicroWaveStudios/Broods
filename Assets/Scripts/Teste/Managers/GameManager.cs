using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public int limitPlayer;
    [SerializeField] int playerInScene;

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

}

