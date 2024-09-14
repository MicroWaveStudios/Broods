using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ConnectPlayerInMenu : MonoBehaviour
{
    [Header("Player Input Manager")]
    [SerializeField] PlayerInputManager playerInputManager;

    [Header("Connect Screen")]
    [SerializeField] GameObject[] PlayerMenu;
    [SerializeField] GameObject[] KeyboardMenu;
    [SerializeField] GameObject[] GamepadMenu;
    [SerializeField] GameObject[] EnterMessage;
    [SerializeField] GameObject ContinueButton;
    [SerializeField] GameObject BackButton;

    [Header("Characters Screen")]


    [SerializeField] GameObject[] player;
    [SerializeField] Transform[] spawn; //Variavel para nn bugar os players ao ir para a cena jogar

    [SerializeField] GameObject eventSystemManager;
    public int limitPlayer;
    public int playerInScene;

    public void ConnectDisconnectPlayer(GameObject Player, int playerID, string controlScheme, bool value)
    {
        DesactivePlayerText(playerID);
        if (!value)
        {
            player[playerID] = null;
            playerInScene--;
            ContinueButton.SetActive(value);
            eventSystemManager.GetComponent<EventSystemManager>().SetCurrentSelectedButton(BackButton);
            Destroy(Player);
        }
        EnterMessage[playerID].SetActive(!value);
        PlayerMenu[playerID].SetActive(value);
        if (controlScheme == "Gamepad")
            GamepadMenu[playerID].SetActive(value);
        else
            KeyboardMenu[playerID].SetActive(value);
    }

    void DesactivePlayerText(int playerID)
    {
        PlayerMenu[playerID].SetActive(false);
        GamepadMenu[playerID].SetActive(false);
        KeyboardMenu[playerID].SetActive(false);
    }

    public void JoinPlayer(GameObject spawnedPlayer)
    {
        int playerNumber;
        if (player[0] == null)
            playerNumber = 0;
        else
            playerNumber = 1;
        player[playerNumber] = spawnedPlayer;

        int tagNumber = playerNumber + 1;
        spawnedPlayer.tag = "Player" + tagNumber;
        spawnedPlayer.GetComponent<ConnectPlayer>().SetupPlayer(playerNumber);

        spawnedPlayer.transform.position = spawn[playerNumber].transform.position;

        playerInScene++;
        if (player[0] != null && player[1] != null)
        {
            ContinueButton.SetActive(true);
            eventSystemManager.GetComponent<EventSystemManager>().SetCurrentSelectedButton(ContinueButton);
        }
    }

    public void EnableSplitKeyboard()
    {
        DestroyPlayersInScene();
        var p1 = PlayerInput.Instantiate(playerInputManager.playerPrefab, 0, "KeyboardLeft", -1, Keyboard.current, Mouse.current);
        var p2 = PlayerInput.Instantiate(playerInputManager.playerPrefab, 1, "KeyboardRight", -1, Keyboard.current);
    }
    public void DestroyPlayersInScene()
    {
        Destroy(player[0]);
        player[0] = null;
        Destroy(player[1]);
        player[1] = null;
    }
    public void ChangeMapAction()
    {
        player[0].GetComponent<PlayerController>().EnableMapActionPlayer();
        player[1].GetComponent<PlayerController>().EnableMapActionPlayer();

        Debug.Log(player[0].GetComponent<PlayerInput>().currentActionMap);
        Debug.Log(player[1].GetComponent<PlayerInput>().currentActionMap);
    }
}
