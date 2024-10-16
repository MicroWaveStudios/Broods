using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ConnectPlayerInMenu : MonoBehaviour
{
    [Header("Player Input Manager")]
    [SerializeField] PlayerInputManager playerInputManager;
    [SerializeField] sceneManager sceneManager;

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

    [Header("Prefabs")]
    [SerializeField] GameObject[] prefabPersonagens;
    int[] playerPersonagem;

    public int limitPlayer;
    public int playerInScene;

    private void Start()
    {
        Pontos.pontosP1 = 0;
        Pontos.pontosP2 = 0;
    }


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

        //spawnedPlayer.transform.position = spawn[playerNumber].transform.position;

        playerInScene++;
        if (player[0] != null && player[1] != null)
        {
            ContinueButton.SetActive(true);
            playerInputManager.DisableJoining();
            eventSystemManager.GetComponent<EventSystemManager>().SetCurrentSelectedButton(ContinueButton);
        }
        else
        {
            playerInputManager.EnableJoining();
        }
    }

    public void TrocarCenaGame(string novaCenaAtual)
    {
        Pontos.prefabPlayer[0] = prefabPersonagens[player[0].GetComponent<ConnectPlayer>().GetNumeroPersonagem()];
        Pontos.prefabPlayer[1] = prefabPersonagens[player[1].GetComponent<ConnectPlayer>().GetNumeroPersonagem()];

        Pontos.ControlSchemePlayer[0] = player[0].GetComponent<ConnectPlayer>().GetControlScheme();
        Pontos.ControlSchemePlayer[1] = player[1].GetComponent<ConnectPlayer>().GetControlScheme();

        Pontos.prefabPlayer[0].tag = player[0].tag;
        Pontos.prefabPlayer[1].tag = player[1].tag;
    }
    public void SetarPrefabPlayer(int playerAtual, int prefabIndex)
    {
        playerPersonagem[playerAtual] = prefabIndex;
    }

    public GameObject GetPlayer1()
    {
        return player[0];
    }
    public GameObject GetPlayer2() 
    {
        return player[1];
    }

    public void EnableSplitKeyboard()
    {
        ConnectDisconnectPlayer(player[0], 0, null, false);
        PlayerInput player1 = PlayerInput.Instantiate(playerInputManager.playerPrefab, 0, "KeyboardLeft", -1, Keyboard.current, Mouse.current);
        PlayerInput player2 = PlayerInput.Instantiate(playerInputManager.playerPrefab, 1, "KeyboardRight", -1, Keyboard.current);
        player1.tag = "Player1";
        player2.tag = "Player2";
        Pontos.SplitKeyboard = true;
    }

    public int GetPlayerInScene()
    {
        return playerInScene;
    }

}
