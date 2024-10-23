using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


enum Etapa
{ 
    conectar, selecaoDePersonagem, selecaoDeMapa
}
public class ConnectPlayerInMenu : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] PlayerInputManager playerInputManager;
    [SerializeField] sceneManager sceneManager;
    [SerializeField] PersonagensManager personagensManager;
    [SerializeField] PanelsManager panelsManager;

    [Header("Connect Screen")]
    [SerializeField] GameObject[] PlayerMenu;
    [SerializeField] GameObject[] KeyboardMenu;
    [SerializeField] GameObject[] GamepadMenu;
    [SerializeField] GameObject[] EnterMessage;
    [SerializeField] GameObject ContinueButton;
    [SerializeField] GameObject BackButton;

    [SerializeField] GameObject TxtSplitKeyboard;

    [Header("Characters Screen")]
    [SerializeField] GameObject[] player;
    [SerializeField] Transform[] spawn; //Variavel para nn bugar os players ao ir para a cena jogar

    [SerializeField] GameObject eventSystemManager;

    [Header("Prefabs")]
    [SerializeField] GameObject[] prefabPersonagens;
    int[] playerPersonagem;

    public int limitPlayer;
    public int playerInScene;
    Etapa etapa;

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
            if (controlScheme == "KeyboardLeft" || controlScheme == "KeyboardRight")
            {
                playerInScene = 0;
            }
            Destroy(Player);
        }
        EnterMessage[playerID].SetActive(!value);
        PlayerMenu[playerID].SetActive(value);
        if (controlScheme == "Gamepad")
        {
            GamepadMenu[playerID].SetActive(value);
        }
        else
        {
            KeyboardMenu[playerID].SetActive(value);
            TxtSplitKeyboard.SetActive(value);
        }
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
        {
            playerNumber = 0;
        }
        else
        { 
            playerNumber = 1;
        }
        player[playerNumber] = spawnedPlayer;

        int tagNumber = playerNumber + 1;
        spawnedPlayer.tag = "Player" + tagNumber;
        spawnedPlayer.GetComponent<ConnectPlayer>().SetupPlayer(playerNumber);
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
    private void Update()
    {
        if (player[0] == null || player[1] == null)
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

    public GameObject GetPlayer(int playerIndex)
    {
        return player[playerIndex];
    }
    public void Voltar(int playerIndex)
    {
        switch (etapa)
        {
            case Etapa.conectar:
                if (player[0] == null && player[1] == null)
                {
                    sceneManager.VoltarMenu();
                }
                break;
            case Etapa.selecaoDePersonagem:
                if (personagensManager.GetConfirmouPersonagem(playerIndex))
                {
                    personagensManager.ConfirmouPersonagem(playerIndex, false);
                }
                else if (personagensManager.GetSelecionouPersonagem(playerIndex)) 
                {
                    personagensManager.SelecionouPersonagem(playerIndex, false);
                }
                else if (!personagensManager.GetSelecionouPersonagem(0) && !personagensManager.GetSelecionouPersonagem(1))
                {
                    personagensManager.AlterarCenaPersonagens(false);
                    personagensManager.DestroyPersonagens();
                    panelsManager.ChangePanel(0);
                    etapa = Etapa.conectar;
                    StartCoroutine(player[0].GetComponent<ConnectPlayer>().PlayerCanDoIt());
                    StartCoroutine(player[1].GetComponent<ConnectPlayer>().PlayerCanDoIt());
                    //Invoke("SetEtapaConectar", 0.2f);
                }
                break;
            case Etapa.selecaoDeMapa:
                panelsManager.ChangePanel(1);
                personagensManager.AlterarCenaPersonagens(true);
                etapa = Etapa.selecaoDePersonagem;
                //Invoke("SetEtapaSelecaoPersonagem", 0.2f);
                break;
        }
    }
    public void SetEtapaConectar()
    {
        etapa = Etapa.conectar;
    }
    public void SetEtapaSelecaoPersonagem()
    {
        etapa = Etapa.selecaoDePersonagem;
    }
    public void SetEtapaSelecaoMapa()
    {
        etapa = Etapa.selecaoDeMapa;
    }

    public string GetEtapa()
    { 
        return etapa.ToString();
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
    public void DisableSplitKeyboard(int i)
    {
        ConnectDisconnectPlayer(player[0], 0, null, false);
        ConnectDisconnectPlayer(player[1], 1, null, false);
        //PlayerInput.Instantiate(playerInputManager.playerPrefab, i, "Keyboard", -1, Keyboard.current, Mouse.current);
        //playerInScene = 1;
        playerInScene = 0;
        Pontos.SplitKeyboard = false;
    }
    public int GetPlayerInScene()
    {
        return playerInScene;
    }

}
