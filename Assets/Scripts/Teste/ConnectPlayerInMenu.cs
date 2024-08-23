using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ConnectPlayerInMenu : MonoBehaviour
{
    [Header("TextInCanvas")]
    [SerializeField] GameObject[] PlayerMenu;
    [SerializeField] GameObject[] KeyboardMenu;
    [SerializeField] GameObject[] GamepadMenu;

    [SerializeField] GameObject[] EnterMessage;
    [SerializeField] GameObject ContinueButton;

    public int limitPlayer;
    int playerInScene;

    public void SetupPlayer(GameObject spawnedPlayer)
    {
        if (playerInScene < limitPlayer)
        {
            int numberTag = playerInScene + 1;
            spawnedPlayer.tag = "Player" + numberTag;
            spawnedPlayer.GetComponent<ConnectPlayer>().SetupPlayer(playerInScene);

            playerInScene++;
        }
        if (playerInScene == limitPlayer)
            ContinueButton.SetActive(true);
    }

    public void ConnectPlayer(int playerID, string controlScheme)
    {

        EnterMessage[playerID].SetActive(false);
        PlayerMenu[playerID].SetActive(true);

        if (controlScheme == "Gamepad")
            GamepadMenu[playerID].SetActive(true);
        else
            KeyboardMenu[playerID].SetActive(true);
    }
}
