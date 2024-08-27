using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ConnectPlayerInMenu : MonoBehaviour
{
    [Header("Player Input Manager")]
    [SerializeField] PlayerInputManager playerInputManager;

    [Header("TextInCanvas")]
    [SerializeField] GameObject[] PlayerMenu;
    [SerializeField] GameObject[] KeyboardMenu;
    [SerializeField] GameObject[] GamepadMenu;

    [SerializeField] GameObject[] EnterMessage;
    [SerializeField] GameObject ContinueButton;
    [SerializeField] GameObject SplitKeyboardPanel;

    [SerializeField] GameObject[] player;

    [SerializeField] GameObject PanelsManager;
    public int limitPlayer;
    int playerInScene;

    public void SetupNewPlayer(GameObject spawnedPlayer)
    {
        if (playerInScene < limitPlayer) 
        { 
            if (player[0] == null)
                SetupPlayer(spawnedPlayer, 0);
            else
                SetupPlayer (spawnedPlayer, 1);
        }
        if (player[0] != null && player[1] != null)
        { 
            ContinueButton.SetActive(true);
            PanelsManager.GetComponent<PanelsManager>().ChangePanel(1);
        }
    }

    public void SetupPlayer(GameObject newPlayer, int numberPlayer)
    {
        int numberTag = numberPlayer + 1;
        newPlayer.tag = "Player" + numberTag;
        newPlayer.GetComponent<ConnectPlayer>().SetupPlayer(numberPlayer);
        player[numberPlayer] = newPlayer;
        playerInScene++;
    }

    public void ConnectDisconnectPlayer(int playerID, string controlScheme, bool value)
    {
        if (!value)
        { 
            player[playerID] = null;
            playerInScene--;
            ContinueButton.SetActive(false);
        }
        EnterMessage[playerID].SetActive(!value);
        PlayerMenu[playerID].SetActive(value);
        if (controlScheme == "Gamepad")
            GamepadMenu[playerID].SetActive(value);
        else
            KeyboardMenu[playerID].SetActive(value);
    }

    IEnumerator InstantiateSplitKeyboardPlayers()
    {
        var p1 = PlayerInput.Instantiate(playerInputManager.playerPrefab, playerIndex: 0, controlScheme: "KeyboardLeft", pairWithDevice: Keyboard.current);
        yield return new WaitForSeconds(1f);
        var p2 = PlayerInput.Instantiate(playerInputManager.playerPrefab, playerIndex: 1, controlScheme: "KeyboardRight", pairWithDevice: Keyboard.current);
        yield break;
    }

    public void ChangeEnum()
    {
        player[0].GetComponent<PlayerController>().EnableMapActionPlayer();
        player[1].GetComponent<PlayerController>().EnableMapActionPlayer();
    }

    #region FunctionsButton
    public void EnablePairControls()
    {
        SplitKeyboardPanel.SetActive(false);
        playerInputManager.EnableJoining();
    }
    public void EnableSplitKeyboard()
    {
        SplitKeyboardPanel.SetActive(false);
        //StartCoroutine(InstantiateSplitKeyboardPlayers());
        //var p1 = PlayerInput.Instantiate(playerInputManager.playerPrefab, playerIndex: 0, controlScheme: "KeyboardLeft", pairWithDevice: Keyboard.current);
        //var p2 = PlayerInput.Instantiate(playerInputManager.playerPrefab, playerIndex: 1, controlScheme: "KeyboardRight", pairWithDevice: Keyboard.current);
        PlayerInput.Instantiate(playerInputManager.playerPrefab, 0, "KeyboardLeft", -1, Keyboard.current, Mouse.current);
        PlayerInput.Instantiate(playerInputManager.playerPrefab, 1, "KeyboardRight", -1, Keyboard.current, Mouse.current);
    }
    #endregion
}
