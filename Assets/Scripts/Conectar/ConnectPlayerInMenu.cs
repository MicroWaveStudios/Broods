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
    [SerializeField] AudioSource conectar;
    [SerializeField] AudioSource desconectar;

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
        for (int i = 0; i < Pontos.vitoriaP.Length; i++)
        {
            Pontos.pontosP[i] = 0;
            Pontos.vitoriaP[i] = 0;
        }
    }
    public void ConnectDisconnectPlayer(GameObject Player, int playerID, string controlScheme, bool value)
    {
        DesactivePlayerText(playerID);

        if (!value)
        {
            desconectar.Play();
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
        conectar.Play();

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
        for (int i = 0; i < 2; i++) 
        {
            Pontos.prefabPlayer[i] = prefabPersonagens[player[i].GetComponent<ConnectPlayer>().GetNumeroPersonagem()];
            Pontos.ControlSchemePlayer[i] = player[i].GetComponent<ConnectPlayer>().GetControlScheme();
            Pontos.variante[i] = personagensManager.GetVariante(i);
            Pontos.personagem[i] = player[i].GetComponent<ConnectPlayer>().GetNumeroPersonagem();
            Pontos.prefabPlayer[i].tag = player[i].tag;
        }
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
                    personagensManager.ConfirmouPersonagem(playerIndex, -1, false);
                }
                else if (personagensManager.GetSelecionouPersonagem(playerIndex)) 
                {
                    personagensManager.SelecionouPersonagem(playerIndex, -1, false);
                }
                else if (!personagensManager.GetSelecionouPersonagem(0) && !personagensManager.GetSelecionouPersonagem(1))
                {
                    personagensManager.AlterarCenaPersonagens(false);
                    personagensManager.DestroyPersonagens();
                    panelsManager.ChangePanel(0);
                    etapa = Etapa.conectar;
                    StartCoroutine(player[0].GetComponent<ConnectPlayer>().PlayerCanDoIt());
                    StartCoroutine(player[1].GetComponent<ConnectPlayer>().PlayerCanDoIt());
                }
                break;
            case Etapa.selecaoDeMapa:
                panelsManager.ChangePanel(1);
                personagensManager.AlterarCenaPersonagens(true);
                etapa = Etapa.selecaoDePersonagem;
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
        playerInScene = 0;
        Pontos.SplitKeyboard = false;
    }
    public int GetPlayerInScene()
    {
        return playerInScene;
    }
}
