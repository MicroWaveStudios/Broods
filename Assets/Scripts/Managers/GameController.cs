using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.SceneManagement;
using TMPro;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameController : MonoBehaviour
{
    float lifePlayer1;
    float lifePlayer2;

    GameObject Player1;
    GameObject Player2;
    Transform PlayerLeft;
    Transform PlayerRight;
    [SerializeField] GameObject[] PrefabPlayer;
    [SerializeField] Transform[] InstancePosition;
    [SerializeField] Transform mid;
    GameObject Canvas;
    [SerializeField] Image greenBar1;
    [SerializeField] Image redBar1;
    [SerializeField] Image greenBar2;
    [SerializeField] Image redBar2;

    [SerializeField] GameObject[] BarraJogador;

    [SerializeField] Slider energyBarPlayer1;
    [SerializeField] Slider energyBarPlayer2;
    [SerializeField] GameObject winnerPanel;
    [SerializeField] TMP_Text textPlayerWinner;

    [SerializeField] GameObject[] WinnerCountP1;
    [SerializeField] GameObject[] WinnerCountP2;

    public float distance;
    public bool ChangedSide;

    public GameObject focusedPlayer;
    public GameObject notFocusedPlayer;
    private bool isPaused = false;

    [SerializeField] GameObject eventSystemManager;
    [SerializeField] GameObject PanelManager;

    PlayerInputManager playerInputManager;

    private void Start()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
        //if (Pontos.pontosP1 != 0 || Pontos.pontosP2 != 0)
        //{
        //    playerInputManager.DisableJoining();
        //}
        if (Pontos.SplitKeyboard)
        {
            //PlayerInput player1 = PlayerInput.Instantiate(Pontos.prefabPlayer[0], 0, "KeyboardLeft", -1, Keyboard.current);
            //PlayerInput player2 = PlayerInput.Instantiate(Pontos.prefabPlayer[1], 1, "KeyboardRight", -1, Keyboard.current);
            //player1.tag = "Player1";
            //player2.tag = "Player2";
            //playerInputManager.DisableJoining();
        }
        else
        {
            InstanciarPlayer();
        }
        BarraJogador[0].SetActive(true);
        BarraJogador[1].SetActive(true);
        Pontos.cenaAtual = SceneManager.GetActiveScene().name;
    }

    //IEnumerator InstanciarComDelay()
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    PlayerInput playerInput1 = PlayerInput.Instantiate(Pontos.prefabPlayer[0], 0, Pontos.ControlSchemePlayer[0], -1, Keyboard.current);
    //    PlayerInput playerInput2 = PlayerInput.Instantiate(Pontos.prefabPlayer[1], 1, Pontos.ControlSchemePlayer[1], -1, Keyboard.current);
    //    playerInput1.tag = "Player1";
    //    playerInput2.tag = "Player2";
    //}

    void InstanciarPlayer()
    {
        for (int i = 0; i < 2; i++)
        {
            Pontos.prefabPlayer[i].GetComponent<PlayerInput>().defaultControlScheme = Pontos.ControlSchemePlayer[i];
            int tagPlayer = i + 1;
            //Pontos.prefabPlayer[i].tag = "Player" + tagPlayer; 
            //Debug.Log(Pontos.ControlSchemePlayer[i]);

            PlayerInput newPlayerInput;

            switch (Pontos.ControlSchemePlayer[i])
            {
                case "Gamepad":
                    newPlayerInput = PlayerInput.Instantiate(Pontos.prefabPlayer[i], i, Pontos.ControlSchemePlayer[i], -1, Gamepad.current);
                    newPlayerInput.tag = "Player" + tagPlayer;
                    break;
                case "Keyboard":
                    newPlayerInput = PlayerInput.Instantiate(Pontos.prefabPlayer[i], i, Pontos.ControlSchemePlayer[i], -1, Keyboard.current, Mouse.current);
                    newPlayerInput.tag = "Player" + tagPlayer;
                    break;
            }

            //switch (Pontos.ControlSchemePlayer[i])
            //{
            //    case "Gamepad":
            //        playerInput[i] = PlayerInput.Instantiate(Pontos.prefabPlayer[i], i, Pontos.ControlSchemePlayer[i], -1, Gamepad.current);
            //        //Debug.Log("GAMEPAD");
            //        break;
            //    case "Keyboard":
            //        playerInput[i] = PlayerInput.Instantiate(Pontos.prefabPlayer[i], i, Pontos.ControlSchemePlayer[i], -1, Keyboard.current, Mouse.current);
            //        //Debug.Log("KEYBOARD");
            //        break;
            //    case "KeyboardLeft":
            //        playerInput[i] = PlayerInput.Instantiate(Pontos.prefabPlayer[i], i, Pontos.ControlSchemePlayer[i], -1, Keyboard.current);
            //        InputUser.PerformPairingWithDevice(Keyboard.current, playerInput[i].user);
            //        //Debug.Log("KEYBOARD_LEFT");
            //        break;
            //    case "KeyboardRight":
            //        playerInput[i] = PlayerInput.Instantiate(Pontos.prefabPlayer[i], i, Pontos.ControlSchemePlayer[i], -1, Keyboard.current);
            //        //Debug.Log("KEYBOARD_RIGHT");
            //        break;
            //}
        }
    }

    private void Update()
    {
        Canvas = GameObject.FindGameObjectWithTag("Canvas");
        Player1 = GameObject.FindGameObjectWithTag("Player1");
        Player2 = GameObject.FindGameObjectWithTag("Player2");

        //if (Player1 == null && !Pontos.SplitKeyboard)
        //{
        //    playerInputManager.playerPrefab.GetComponent<PlayerInput>().defaultControlScheme = Pontos.ControlSchemePlayer[0];
        //    playerInputManager.playerPrefab = Pontos.prefabPlayer[0];
        //}
        //else if (Player1 != null && !Pontos.SplitKeyboard)
        //{
        //    playerInputManager.playerPrefab.GetComponent<PlayerInput>().defaultControlScheme = Pontos.ControlSchemePlayer[1];
        //    playerInputManager.playerPrefab = Pontos.prefabPlayer[1];
        //}

        if (Player1 != null && Player2 != null)
        {
            ChangePlayer();
            MidPosition();
            PlayerLife();
            PlayerEnergy();
        }
        else
        {
            if (Pontos.SplitKeyboard)
            {
                if (Player1 == null)
                {
                    playerInputManager.playerPrefab = Pontos.prefabPlayer[0];
                    playerInputManager.playerPrefab.tag = "Player1";                   
                    playerInputManager.playerPrefab.GetComponent<PlayerInput>().defaultControlScheme = "Keyboard";
                    //var player1 = PlayerInput.Instantiate(playerInputManager.playerPrefab, 0, null, -1, Keyboard.current, Mouse.current);
                    //player1.tag = "Player1";
                }
                //else if (Player1 != null)
                //{
                //    Destroy(Player1);
                //    PlayerInput player1 = PlayerInput.Instantiate(playerInputManager.playerPrefab, 0, "KeyboardLeft", -1, Keyboard.current, Mouse.current);
                //    PlayerInput player2 = PlayerInput.Instantiate(playerInputManager.playerPrefab, 1, "KeyboardRight", -1, Keyboard.current);
                //    player1.tag = "Player1";
                //    player2.tag = "Player2";
                //}
                //var allKeyboards = Keyboard.all;
                //InputDevice player1Device = allKeyboards[0];
                //InputDevice player2Device = allKeyboards[0];

                //InputUser playerInputUser1 = InputUser.CreateUserWithoutPairedDevices();
                //InputUser playerInputUser2 = InputUser.CreateUserWithoutPairedDevices();

                //InputUser.PerformPairingWithDevice(Keyboard.current, playerInputUser1);
                //InputUser.PerformPairingWithDevice(Keyboard.current, playerInputUser2);

                //Pontos.prefabPlayer[0].GetComponent<PlayerInput>().defaultControlScheme = "KeyboardLeft";
                //GameObject playerGameObject1 = Instantiate(Pontos.prefabPlayer[0]);
                //Pontos.prefabPlayer[1].GetComponent<PlayerInput>().defaultControlScheme = "KeyboardRight";
                //GameObject playerGameObject2 = Instantiate(Pontos.prefabPlayer[1]);

                //PlayerInput playerInput1 = playerGameObject1.GetComponent<PlayerInput>();
                //PlayerInput playerInput2 = playerGameObject2.GetComponent<PlayerInput>();

                //Pontos.prefabPlayer[0].GetComponent<PlayerInput>().defaultControlScheme = "KeyboardLeft";
                //playerInputManager.playerPrefab.GetComponent<PlayerInput>().defaultControlScheme = "KeyboardLeft";
                //var player1 = PlayerInput.Instantiate(playerInputManager.playerPrefab, 0, null, -1, Keyboard.current, Mouse.current);
                //playerInputManager.playerPrefab.GetComponent<PlayerInput>().defaultControlScheme = "KeyboardRight";
                //Pontos.prefabPlayer[1].GetComponent<PlayerInput>().defaultControlScheme = "KeyboardRight";
                //var player2 = PlayerInput.Instantiate(playerInputManager.playerPrefab, 1, null, -1, Keyboard.current);
                //InputUser.PerformPairingWithDevice(player2.GetComponent<PlayerInput>().devices[0], player2.user);

                //playerInput1.tag = "Player1";
                //playerInput2.tag = "Player2";

                //player1.tag = "Player1";
                //player2.tag = "Player2";

                //InputUser.PerformPairingWithDevice(player1Device, playerInput1.user);
                //playerInput1.SwitchCurrentActionMap(Pontos.ControlSchemePlayer[0]);

                //InputUser.PerformPairingWithDevice(player2Device, playerInput2.user);
                //playerInput2.SwitchCurrentActionMap(Pontos.ControlSchemePlayer[1]);

            }
        }

        if (Pontos.pontosP1 >= 1)
        {
            WinnerCountP1[0].SetActive(true);
            if (Pontos.pontosP1 == 2)
            {
                WinnerCountP1[1].SetActive(true);
            }
        }
        if (Pontos.pontosP1 == 0)
        {
            WinnerCountP1[0].SetActive(false);
            WinnerCountP1[1].SetActive(false);
        }
        if (Pontos.pontosP2 >= 1)
        {
            WinnerCountP2[0].SetActive(true);
            if (Pontos.pontosP2 == 2)
            {
                WinnerCountP2[1].SetActive(true);
            }
        }
        if (Pontos.pontosP2 == 0)
        {
            WinnerCountP2[0].SetActive(false);
            WinnerCountP2[1].SetActive(false);
        }
    }

    void PlayerLife()
    {
        lifePlayer1 = Player1.GetComponent<PlayerStats>().life / Player1.GetComponent<PlayerStats>().maxLife;
        greenBar1.fillAmount = lifePlayer1;
        StartCoroutine(DecreaseRedBar1(lifePlayer1));


        lifePlayer2 = Player2.GetComponent<PlayerStats>().life / Player2.GetComponent<PlayerStats>().maxLife;
        greenBar2.fillAmount = lifePlayer2;
        StartCoroutine(DecreaseRedBar2(lifePlayer2));
    }

    IEnumerator DecreaseRedBar1(float lifePlayer)
    {
        yield return new WaitForSeconds(0.5f);
        float redBar1Amount = redBar1.fillAmount;

        while (redBar1.fillAmount > lifePlayer)
        {
            redBar1Amount -= Time.deltaTime * 0.25f;
            redBar1.fillAmount = redBar1Amount;

            yield return null;
        }

        redBar1.fillAmount = lifePlayer;
    }

    IEnumerator DecreaseRedBar2(float lifePlayer)
    {
        yield return new WaitForSeconds(0.5f);
        float redBar2Amount = redBar2.fillAmount;

        while (redBar2.fillAmount > lifePlayer)
        {
            redBar2Amount -= Time.deltaTime * 0.25f;
            redBar2.fillAmount = redBar2Amount;

            yield return null;
        }

        redBar2.fillAmount = lifePlayer;
    }

    void PlayerEnergy()
    {
        energyBarPlayer1.value = Player1.GetComponent<PlayerStats>().energy;
        energyBarPlayer2.value = Player2.GetComponent<PlayerStats>().energy;
    }

    void ChangePlayer()
    {
        if (Player1.transform.position.x < Player2.transform.position.x)
        {
            //Esta na esquerda (posicao inicial)
            ChangedSide = false;
            PlayerLeft = Player1.transform;
            PlayerRight = Player2.transform;
        }
        else
        {
            //Esta na direita (posicao modificada)
            ChangedSide = true;
            PlayerLeft = Player2.transform;
            PlayerRight = Player1.transform;
        }

        if (PlayerLeft.GetComponent<PlayerStats>().GetXimas())
        {
            PlayerLeft.localScale = new Vector3(0.009999999f, 0.01f, 0.009999999f);
        }
        else
        {
            PlayerLeft.localScale = new Vector3(1f, 1f, 1f);
        }
        if (PlayerRight.GetComponent<PlayerStats>().GetXimas())
        {
            PlayerRight.localScale = new Vector3(0.009999999f, 0.01f, -0.009999999f);
        }
        else
        {
            PlayerRight.localScale = new Vector3(1f, 1f, -1f);
        }
    }

    void MidPosition()
    {
        distance = Vector3.Distance(PlayerLeft.position, PlayerRight.position)/2f;
        mid.position = new Vector3(PlayerLeft.position.x + distance, mid.position.y, mid.position.z);
    }

    public void Pause(GameObject newFocusedPlayer, bool value)
    {
        if (focusedPlayer == null)
            focusedPlayer = newFocusedPlayer;
        if (focusedPlayer.tag == newFocusedPlayer.tag)
        {
            PanelManager.GetComponent<PanelsManager>().ChangePanel(0);
            isPaused = value;
            SetActiveInputNotFocusedPlayer(focusedPlayer.tag, value);
            SwitchControlScheme(value);
            UIPause(value);
        }
    }
    void SwitchControlScheme(bool value)
    {
        switch (value)
        {
            case true:
                focusedPlayer.GetComponent<PlayerController>().EnableMapActionUI();
                break;
            case false:
                focusedPlayer.GetComponent<PlayerController>().EnableMapActionPlayer();
                focusedPlayer = null;
                notFocusedPlayer = null;
                break;
        }
    }
    void SetActiveInputNotFocusedPlayer(string tag, bool value)
    {
        switch (tag)
        {
            case "Player1":
                notFocusedPlayer = GameObject.FindGameObjectWithTag("Player2");
                notFocusedPlayer.GetComponent<PlayerMoveRigidbody>().SetInputActive(value);
                break;
            case "Player2":
                notFocusedPlayer = GameObject.FindGameObjectWithTag("Player1");
                notFocusedPlayer.GetComponent<PlayerMoveRigidbody>().SetInputActive(value);
                break;
        }
    }
    public void UIPause(bool value)
    {
        GameObject UIManager = GameObject.FindGameObjectWithTag("UIManager");
        UIManager.GetComponent<UIManager>().UIStatePause(value);
    }
    public bool GetBooleanIsPaused()
    {
        return isPaused;
    }

    public void DestroyPlayers()
    {
        Destroy(Player1);
        Player1 = null;
        Destroy(Player2);
        Player2 = null;
    }

    public void GameFinished()
    {
        if (lifePlayer1 == lifePlayer2)
        {
            
        }
        else
        { 
            if (lifePlayer1 > lifePlayer2)
                Pontos.pontosP1++;
            else if (lifePlayer2 > lifePlayer1)
                Pontos.pontosP2++;

            if (Pontos.pontosP1 > 1)
            { 
                textPlayerWinner.text = "Player 1 Ganhou!";
                StartCoroutine(FinishGame());
                finishGame = true;
            }
            if (Pontos.pontosP2 > 1)
            { 
                textPlayerWinner.text = "Player 2 Ganhou!";
                StartCoroutine(FinishGame());
                finishGame = true;
            }
        }
        if (!finishGame)
        {
            SceneManager.LoadScene(Pontos.cenaAtual);
        }
    }

    bool finishGame = false;

    IEnumerator FinishGame()
    {
        BarraJogador[0].SetActive(false);
        BarraJogador[1].SetActive(false);
        ZerarPontos();
        winnerPanel.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        ZerarPontos();
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("Menu");
    }

    public void ZerarPontos()
    {
        Pontos.pontosP1 = 0;
        Pontos.pontosP2 = 0;
    }


    public void SetTimeScale()
    {
        //StartCoroutine(ChangeTimeScale());
    }
    IEnumerator ChangeTimeScale()
    {
        yield return new WaitForSeconds(0.1f);
        float speedP1 = Player1.GetComponent<Animator>().speed;
        float speedP2 = Player2.GetComponent<Animator>().speed;
        Player1.GetComponent<Animator>().speed = 0f;
        Player2.GetComponent<Animator>().speed = 0f;
        yield return new WaitForSeconds(0.2f);
        Player1.GetComponent<Animator>().speed = speedP1;
        Player2.GetComponent<Animator>().speed = speedP2;
        yield break;
    }

    public void EnableSplitKeyboard(GameObject newGameObject)
    {
        playerInputManager.DisableJoining();
        Destroy(newGameObject);
        Pontos.prefabPlayer[0].GetComponent<PlayerInput>().defaultControlScheme = "KeyboardLeft";
        PlayerInput player1 = PlayerInput.Instantiate(playerInputManager.playerPrefab, 0, "KeyboardLeft", -1, Keyboard.current, Mouse.current);
        player1.tag = "Player1";
        Pontos.prefabPlayer[1].GetComponent<PlayerInput>().defaultControlScheme = "KeyboardRight";
        PlayerInput player2 = PlayerInput.Instantiate(playerInputManager.playerPrefab, 1, "KeyboardRight", -1, Keyboard.current);
        player2.tag = "Player2";

        PlayerInput player3 = PlayerInput.Instantiate(playerInputManager.playerPrefab, 0, "KeyboardLeft", -1, Keyboard.current, Mouse.current);
        player3.tag = "Player1";
        Pontos.prefabPlayer[1].GetComponent<PlayerInput>().defaultControlScheme = "KeyboardRight";
        PlayerInput player4 = PlayerInput.Instantiate(playerInputManager.playerPrefab, 1, "KeyboardRight", -1, Keyboard.current);
        player4.tag = "Player2";

        player3.transform.position = InstancePosition[0].transform.position;
        player4.transform.position = InstancePosition[1].transform.position;

        Destroy(player1.gameObject);
        Destroy(player2.gameObject);
    }
}
