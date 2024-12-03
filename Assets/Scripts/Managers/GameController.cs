using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameController : MonoBehaviour
{
    float lifePlayer1;
    float lifePlayer2;

    GameObject[] player = new GameObject[2];
    //GameObject Player2;
    Transform PlayerLeft;
    Transform PlayerRight;
    [SerializeField] GameObject[] PrefabPlayer;

    [SerializeField] GameObject PlayerTeste;
    [SerializeField] GameObject UIGame;

    [SerializeField] Transform[] InstancePosition;
    [SerializeField] Transform mid;
    GameObject Canvas;
    [SerializeField] Image greenBar1;
    [SerializeField] Image redBar1;
    [SerializeField] Image greenBar2;
    [SerializeField] Image redBar2;

    [SerializeField] GameObject[] fotoPersonagem;
    [SerializeField] TMP_Text[] nomeDoPersonagem;
    [SerializeField] Sprite[] imagemPersonagem_P1;
    [SerializeField] Sprite[] imagemPersonagem_P2;

    [SerializeField] GameObject[] BarraJogador;

    [SerializeField] Slider energyBarPlayer1;
    [SerializeField] Slider energyBarPlayer2;
    [SerializeField] GameObject winnerPanel;
    [SerializeField] TMP_Text textPlayerWinner;

    [SerializeField] GameObject[] WinnerCountP1;
    [SerializeField] GameObject[] WinnerCountP2;

    public float distance;
    public bool ChangedSide;
    bool ChangeSide = true;

    public GameObject focusedPlayer;
    public GameObject notFocusedPlayer;
    private bool isPaused = false;

    [SerializeField] GameObject eventSystemManager;
    [SerializeField] PanelsManager PanelManager;

    [Header("Cameras")]
    [SerializeField] GameObject CameraPrincipal;
    [SerializeField] GameObject CameraVitoria;
    [SerializeField] float alturaLimite;
    [SerializeField] float adicionarAltura;


    PlayerInputManager playerInputManager;
    [SerializeField] AudioSource SomVitoria;
    [SerializeField] AudioSource SomBG;

    private void Start()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
        //if (Pontos.pontosP1 != 0 || Pontos.pontosP2 != 0)
        //{
        //    playerInputManager.DisableJoining();
        //}
        if (Pontos.SplitKeyboard)
        {
            EnableSplitKeyboard();
        }
        else
        {
            InstanciarPlayer();
        }
        BarraJogador[0].SetActive(true);
        BarraJogador[1].SetActive(true);
        for (int i = 0; i < player.Length; i++)
        {
            switch (i)
            {
                case 0:
                    fotoPersonagem[i].GetComponent<Image>().sprite = imagemPersonagem_P1[Pontos.personagem[i]];
                    break;
                case 1:
                    fotoPersonagem[i].GetComponent<Image>().sprite = imagemPersonagem_P2[Pontos.personagem[i]];
                    break;
            }
            switch (Pontos.personagem[i])
            {
                case 0:
                    nomeDoPersonagem[i].text = "NARÁ";
                    break;
                case 1:
                    nomeDoPersonagem[i].text = "XIMAS"; 
                    break;
            }
        }
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
                    Pontos.prefabPlayer[i].GetComponent<PlayerInput>().defaultControlScheme = "Gamepad";
                    newPlayerInput = PlayerInput.Instantiate(Pontos.prefabPlayer[i], i, Pontos.ControlSchemePlayer[i], -1, Gamepad.current);
                    newPlayerInput.tag = "Player" + tagPlayer;
                    break;
                case "Keyboard":
                    Pontos.prefabPlayer[i].GetComponent<PlayerInput>().defaultControlScheme = "Keyboard";
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
        player[0] = GameObject.FindGameObjectWithTag("Player1");
        player[1] = GameObject.FindGameObjectWithTag("Player2");

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

        if (player[0] != null && player[1] != null)
        {
            if (!Pontos.SplitKeyboard)
            {
                GetComponent<CountdownTimer>().SetPodeComecar(true);
            }
            ChangePlayer();
            MidPosition();
            PlayerLife();
            PlayerEnergy();
        }

        if (Pontos.vitoriaP[0] >= 1)
        {
            WinnerCountP1[0].SetActive(true);
            if (Pontos.vitoriaP[0] == 2)
            {
                WinnerCountP1[1].SetActive(true);
            }
        }
        if (Pontos.vitoriaP[0] == 0)
        {
            WinnerCountP1[0].SetActive(false);
            WinnerCountP1[1].SetActive(false);
        }
        if (Pontos.vitoriaP[1] >= 1)
        {
            WinnerCountP2[0].SetActive(true);
            if (Pontos.vitoriaP[1] == 2)
            {
                WinnerCountP2[1].SetActive(true);
            }
        }
        if (Pontos.vitoriaP[1] == 0)
        {
            WinnerCountP2[0].SetActive(false);
            WinnerCountP2[1].SetActive(false);
        }
    }

    void PlayerLife()
    {
        lifePlayer1 = player[0].GetComponent<PlayerStats>().life / player[0].GetComponent<PlayerStats>().maxLife;
        greenBar1.fillAmount = lifePlayer1;
        StartCoroutine(DecreaseRedBar1(lifePlayer1));

        lifePlayer2 = player[1].GetComponent<PlayerStats>().life / player[1].GetComponent<PlayerStats>().maxLife;
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
        energyBarPlayer1.maxValue = player[0].GetComponent<PlayerStats>().maxEnergy;
        energyBarPlayer1.value = player[0].GetComponent<PlayerStats>().energy;
        energyBarPlayer2.maxValue = player[1].GetComponent<PlayerStats>().maxEnergy;
        energyBarPlayer2.value = player[1].GetComponent<PlayerStats>().energy;
    }

    void ChangePlayer()
    {
        if (player[0].transform.position.x < player[1].transform.position.x)
        {
            //Esta na esquerda (posicao inicial)
            ChangedSide = false;
            PlayerLeft = player[0].transform;
            PlayerRight = player[1].transform;
        }
        else
        {
            //Esta na direita (posicao modificada)
            ChangedSide = true;
            PlayerLeft = player[1].transform;
            PlayerRight = player[0].transform;
        }
        if (ChangeSide)
        {
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
    }

    [SerializeField] float midY;
    void MidPosition()
    {
        distance = Vector3.Distance(PlayerLeft.position, PlayerRight.position)/2f;
        midY = player[0].transform.position.y + player[1].transform.position.y /2f + adicionarAltura;
        if (midY > alturaLimite)
        {
            midY = alturaLimite;
        }
        mid.position = new Vector3(PlayerLeft.position.x + distance, midY, mid.position.z);
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
        Destroy(player[0]);
        player = null;
        Destroy(player[1]);
        player[1] = null;
    }

    int ganhador;
    public void GameFinished()
    {
        if (lifePlayer1 != lifePlayer2)
        {
            if (lifePlayer1 > lifePlayer2)
            {
                Pontos.vitoriaP[0]++;
                ganhador = 0;
            }
            else if (lifePlayer2 > lifePlayer1)
            {
                Pontos.vitoriaP[1]++;
                ganhador = 1;
            }


            player[ganhador].GetComponent<PlayerStats>().SomarPontos(1000);

            if (player[ganhador].GetComponent<PlayerStats>().life == player[ganhador].GetComponent<PlayerStats>().maxLife)
            {
                Debug.Log("Perfect");
                player[ganhador].GetComponent<PlayerStats>().SomarPontos(1000);
            }

            if (Pontos.vitoriaP[0] > 1)
            {
                VitoriaTempo(0);
                textPlayerWinner.text = "Player 1 Ganhou!";
                StartCoroutine(FinishGame(0));
                finishGame = true;
            }
            if (Pontos.vitoriaP[1] > 1)
            {
                VitoriaTempo(1);
                textPlayerWinner.text = "Player 2 Ganhou!";
                StartCoroutine(FinishGame(1));
                finishGame = true;
            }
            for (int i = 0; i < player.Length; i++)
            {
                Pontos.pontosP[i] += player[i].GetComponent<PlayerStats>().GetPontos();

                if (!player[i].GetComponent<PlayerStats>().GetXimas())
                {
                    Pontos.tarticos[i] = player[i].GetComponent<NaraSkills>().GetTarticos();
                }
            }

        }
        if (!finishGame)
        {
            StartCoroutine(ReloadRound(ganhador));
        }
    }

    bool finishGame = false;
    float tempoAnimacaoDeVitoria;

    [SerializeField] float VitoriaXimas;
    [SerializeField] float VitoriaNara;
    void VitoriaTempo(int novoGanhador)
    {
        switch (Pontos.personagem[novoGanhador])
        {
            case 0:
                tempoAnimacaoDeVitoria = VitoriaNara;
                break;
            case 1:
                tempoAnimacaoDeVitoria = VitoriaXimas;
                break;
        }
    }

    IEnumerator ReloadRound(int novoGanhador)
    {
        for (int i = 0; i < player.Length; i++)
        {
            if (i != novoGanhador)
            {
                player[i].GetComponent<PlayerAnimator>().Perdeu();
            }
            player[i].GetComponent<PlayerController>().EnableMapActionUI();
        }
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(Pontos.cenaAtual);
    }

    IEnumerator FinishGame(int novoGanhador)
    {
        for (int i = 0; i < player.Length; i++)
        {
            if (i != novoGanhador)
            {
                player[i].GetComponent<PlayerAnimator>().Perdeu();
            }
            player[i].GetComponent<PlayerController>().EnableMapActionUI();
        }
        ChangeSide = false;
        yield return new WaitForSeconds(2f);
        if (player[novoGanhador].GetComponent<PlayerStats>().GetXimas())
        {
            player[novoGanhador].transform.localScale = new Vector3(0.009999999f, 0.01f, 0.009999999f);
        }
        else
        {
            player[novoGanhador].transform.localScale = new Vector3(1f, 1f, 1f);
        }
        player[novoGanhador].GetComponent<PlayerAnimator>().Ganhou();

        CameraPrincipal.SetActive(false);
        CameraVitoria.SetActive(true);
        SomVitoria.Play();
        SomBG.Stop();

        CameraVitoria.transform.position = player[novoGanhador].transform.position;
        Animator animCamera = CameraVitoria.GetComponent<Animator>();
        animCamera.SetTrigger("Vitoria" + Pontos.personagem[novoGanhador]);

        BarraJogador[0].SetActive(false);
        BarraJogador[1].SetActive(false);
        ZerarPontosEVitorias();

        UIGame.SetActive(false);

        //PanelManager.ChangePanel(1);

        //yield return new WaitForSeconds(tempoAnimacaoDeVitoria);

        yield return new WaitForSeconds(3f);
        winnerPanel.SetActive(true);
        PanelManager.ChangePanel(1);
        ZerarPontosEVitorias();
        //yield return new WaitForSeconds(2.5f);
        //SceneManager.LoadScene("Menu");
        yield break;
    }

    public void ZerarPontosEVitorias()
    {
        for (int i = 0; i < player.Length; i++)
        {
            Pontos.pontosP[i] = 0;
            Pontos.vitoriaP[i] = 0;
        }
    }


    public void SetTimeScale()
    {
        //StartCoroutine(ChangeTimeScale());
    }
    IEnumerator ChangeTimeScale()
    {
        yield return new WaitForSeconds(0.1f);
        float speedP1 = player[0].GetComponent<Animator>().speed;
        float speedP2 = player[1].GetComponent<Animator>().speed;
        player[0].GetComponent<Animator>().speed = 0f;
        player[1].GetComponent<Animator>().speed = 0f;
        yield return new WaitForSeconds(0.2f);
        player[0].GetComponent<Animator>().speed = speedP1;
        player[1].GetComponent<Animator>().speed = speedP2;
        yield break;
    }

    public void EnableSplitKeyboard()
    {
        Pontos.prefabPlayer[1].GetComponent<PlayerInput>().defaultControlScheme = "KeyboardLeft";
        playerInputManager.playerPrefab = Pontos.prefabPlayer[0];
        PlayerInput player1 = PlayerInput.Instantiate(playerInputManager.playerPrefab, 0, "KeyboardLeft", -1, Keyboard.current, Mouse.current);
        Pontos.prefabPlayer[1].GetComponent<PlayerInput>().defaultControlScheme = "KeyboardRight";
        playerInputManager.playerPrefab = Pontos.prefabPlayer[1];
        PlayerInput player2 = PlayerInput.Instantiate(playerInputManager.playerPrefab, 1, "KeyboardRight", -1, Keyboard.current);
        player1.GetComponent<MaterialPlayer>().SetMaterialPersonagem(Pontos.variante[0]);
        player2.GetComponent<MaterialPlayer>().SetMaterialPersonagem(Pontos.variante[1]);
        player1.tag = "Player1";
        player2.tag = "Player2";

        GetComponent<CountdownTimer>().SetPodeComecar(true);
        //StartCoroutine(InstantiatePlayerSplitKeyboard());
    }
    //IEnumerator InstantiatePlayerSplitKeyboard()
    //{
    //    playerInputManager.DisableJoining();

    //    yield return new WaitForSeconds(1f);

    //    Pontos.prefabPlayer[1].GetComponent<PlayerInput>().defaultControlScheme = "KeyboardLeft";
    //    playerInputManager.playerPrefab = Pontos.prefabPlayer[0];
    //    PlayerInput player3 = PlayerInput.Instantiate(playerInputManager.playerPrefab, 0, "KeyboardLeft", -1, Keyboard.current, Mouse.current);
    //    Pontos.prefabPlayer[1].GetComponent<PlayerInput>().defaultControlScheme = "KeyboardRight";
    //    playerInputManager.playerPrefab = Pontos.prefabPlayer[1];
    //    PlayerInput player4 = PlayerInput.Instantiate(playerInputManager.playerPrefab, 1, "KeyboardRight", -1, Keyboard.current);

    //    player3.transform.position = InstancePosition[0].transform.position;
    //    player4.transform.position = InstancePosition[1].transform.position;

    //    yield return new WaitForSeconds(1f);

    //    player3.GetComponent<MaterialPlayer>().SetMaterialPersonagem(Pontos.variante[0]);
    //    player3.tag = "Player1";
    //    player4.GetComponent<MaterialPlayer>().SetMaterialPersonagem(Pontos.variante[1]);
    //    player4.tag = "Player2";

    //    yield return new WaitForSeconds(2.5f);

    //    GetComponent<CountdownTimer>().SetPodeComecar(true);
    //}
    public void ReloadScene()
    {
        ZerarPontosEVitorias();
        SceneManager.LoadScene(Pontos.cenaAtual);
    }

    public GameObject GetPlayer(int index)
    {
        return player[index];
    }

    public Transform GetPlayerLeft()
    { 
        return PlayerLeft;
    }
    public Transform GetPlayerRight()
    {
        return PlayerRight;
    }

    public float GetDistance()
    {
        return distance;
    }
}
