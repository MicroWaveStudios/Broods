using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.SceneManagement;
using TMPro;
//using System;
//using System.Linq;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class GameController : MonoBehaviour
{
    //float lifePlayer1;
    //float lifePlayer2;
    float[] vidaPlayer = new float[2];

    GameObject[] player = new GameObject[2];
    //GameObject Player2;
    Transform PlayerLeft;
    Transform PlayerRight;
    [SerializeField] GameObject[] PrefabPlayer;

    [SerializeField] GameObject PlayerTeste;
    [SerializeField] GameObject UIGame;

    [SerializeField] GameObject[] TarticosUI;

    [SerializeField] Transform[] InstancePosition;
    [SerializeField] Transform mid;
    GameObject Canvas;
    [SerializeField] Image[] barraVerde;
    [SerializeField] Image[] barraVermelha;
    [SerializeField] GameObject UI;
    [SerializeField] GameObject PauseUI;

    [System.Serializable]
    struct Pause
    {
        public GameObject[] PauseNara;
        public GameObject[] PauseXimas;
    }

    [SerializeField] List<Pause> Pauses = new List<Pause>();

    [SerializeField] string[] ControlScheme;

    [SerializeField] GameObject[] fotoPersonagem;
    [SerializeField] TMP_Text[] nomeDoPersonagem;
    [SerializeField] Sprite[] imagemPersonagem_P1;
    [SerializeField] Sprite[] imagemPersonagem_P2;
    [SerializeField] Sprite[] imagemPersonagem;

    [SerializeField] GameObject[] BarraJogador;

    [SerializeField] Image[] barraDeEnergia;
    [SerializeField] GameObject winnerPanel;
    [SerializeField] TMP_Text textPlayerWinner;

    [SerializeField] GameObject[] WinnerCountP1;
    [SerializeField] GameObject[] WinnerCountP2;

    [SerializeField] GameObject[] WinnerPanelPlayer;
    [SerializeField] TMP_Text pontosTelaWinner; 

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
            fotoPersonagem[i].GetComponent<Image>().sprite = imagemPersonagem[Pontos.personagem[i]];
            //switch (i)
            //{
            //    case 0:
            //        fotoPersonagem[i].GetComponent<Image>().sprite = imagemPersonagem_P1[Pontos.personagem[i]];
            //        //fotoPersonagem[i].transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            //        break;
            //    case 1:
            //        fotoPersonagem[i].GetComponent<Image>().sprite = imagemPersonagem_P2[Pontos.personagem[i]];
            //        //fotoPersonagem[i].transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            //        break;
            //}
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

            PlayerInput newPlayerInput;

            InputDevice deviceController = Pontos.devicePlayer[i];

            switch (Pontos.ControlSchemePlayer[i])
            {
                case "Gamepad":
                    Pontos.prefabPlayer[i].GetComponent<PlayerInput>().defaultControlScheme = "Gamepad";
                    newPlayerInput = PlayerInput.Instantiate(Pontos.prefabPlayer[i], i, Pontos.ControlSchemePlayer[i], pairWithDevices: new InputDevice[] { deviceController });
                    newPlayerInput.tag = "Player" + tagPlayer;
                    newPlayerInput.GetComponent<MaterialPlayer>().SetMaterialPersonagem(Pontos.variante[i]);
                    break;
                case "Keyboard":
                    Pontos.prefabPlayer[i].GetComponent<PlayerInput>().defaultControlScheme = "Keyboard";
                    newPlayerInput = PlayerInput.Instantiate(Pontos.prefabPlayer[i], i, Pontos.ControlSchemePlayer[i], -1, Keyboard.current, Mouse.current);
                    newPlayerInput.tag = "Player" + tagPlayer;
                    newPlayerInput.GetComponent<MaterialPlayer>().SetMaterialPersonagem(Pontos.variante[i]);
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
            //PlayerLife();
            PlayerVida_Energia();
            for (int i = 0; i < player.Length; i++)
            {
                if (Pontos.personagem[i] == 0)
                {
                    TarticosUI[i].SetActive(true);
                    int quantidadeTartico = player[i].GetComponent<NaraSkills>().GetTarticos();
                    if (quantidadeTartico > 0)
                    {
                        for (int x = 0; x < quantidadeTartico; x++)
                        {
                            TarticosUI[i].transform.GetChild(x).GetComponent<Image>().color = new Color(Color.white.r, Color.white.g, Color.white.b, 255f);
                        }
                    }
                }
                else
                {
                    TarticosUI[i].SetActive(false);
                }
            }
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

    //void PlayerLife()
    //{
    //    lifePlayer1 = player[0].GetComponent<PlayerStats>().GetVida() / player[0].GetComponent<PlayerStats>().GetVidaMax();
    //    greenBar1.fillAmount = lifePlayer1;
    //    StartCoroutine(DecreaseRedBar1(lifePlayer1));

    //    //lifePlayer2 = player[1].GetComponent<PlayerStats>().vida / player[1].GetComponent<PlayerStats>().vidaMax;
    //    //greenBar2.fillAmount = lifePlayer2;
    //    StartCoroutine(DecreaseRedBar2(lifePlayer2));
    //}
    IEnumerator DecreaseBarraVermelha(float lifePlayer, int playerIndex)
    {
        yield return new WaitForSeconds(0.5f);
        float barraVermelhaAmount = barraVermelha[playerIndex].fillAmount;
        while (barraVermelha[playerIndex].fillAmount > lifePlayer)
        {
            barraVermelhaAmount -= Time.deltaTime * 0.25f;
            barraVermelha[playerIndex].fillAmount = barraVermelhaAmount;
            yield return null;
        }
        barraVermelha[playerIndex].fillAmount = lifePlayer;
    }

    void PlayerVida_Energia()
    {
        for (int i = 0; i < player.Length; i++)
        {
            barraDeEnergia[i].fillAmount = player[i].GetComponent<PlayerStats>().GetEnergia() / player[i].GetComponent<PlayerStats>().GetEnergiaMax();
            vidaPlayer[i] = player[i].GetComponent<PlayerStats>().GetVida() / player[i].GetComponent<PlayerStats>().GetVidaMax();
            barraVerde[i].fillAmount = vidaPlayer[i];
            StartCoroutine(DecreaseBarraVermelha(vidaPlayer[i], i));
        }
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

    public void PauseOn_Off(GameObject newFocusedPlayer, bool value)
    {
        if (focusedPlayer == null)
        { 
            focusedPlayer = newFocusedPlayer;
        }
        if (focusedPlayer.tag == newFocusedPlayer.tag)
        {
            PanelManager.GetComponent<PanelsManager>().ChangePanel(0);
            isPaused = value;
            SetActiveInputNotFocusedPlayer(focusedPlayer.tag, value);
            SwitchControlScheme(value);
            PauseUI.SetActive(value);
            MostrarPauseCerto(newFocusedPlayer, value);
            UI.SetActive(!value);
            UIPause(value);
            if (!value)
            {
                focusedPlayer = null;
            }
        }
    }

    public void MostrarPauseCerto(GameObject newFocusedPlayer, bool value)
    {
        int playerID = newFocusedPlayer.GetComponent<PlayerController>().GetPlayerID();
        if (focusedPlayer == null)
            focusedPlayer = newFocusedPlayer;
        if (focusedPlayer.tag == newFocusedPlayer.tag)
        {
            for (int i = 0; i < ControlScheme.Length; i++)
            {
                //Debug.Log(Pontos.ControlSchemePlayer[playerID] + " " + ControlScheme[i]);
                //Debug.Log(focusedPlayer.name);
                if (Pontos.ControlSchemePlayer[playerID] == ControlScheme[i])
                {
                    if (Pontos.personagem[playerID] == 0)
                    {
                        Pauses[playerID].PauseNara[i].SetActive(value);
                    }
                    if (Pontos.personagem[playerID] == 1)
                    {
                        Pauses[playerID].PauseXimas[i].SetActive(value);
                    }
                }
            }
            //if (focusedPlayer.name == "Nará")
            //{
            //    if (Pontos.ControlSchemePlayer[playerID] == "Keyboard")
            //    {
            //        PauseP1[0].SetActive(value);
            //    }
            //    if (Pontos.ControlSchemePlayer[playerID] == "KeyboardLeft")
            //    {
            //        PauseNara[1].SetActive(value);
            //    }
            //    if (Pontos.ControlSchemePlayer[playerID] == "KeyboardRight")
            //    {
            //        PauseNara[2].SetActive(value);
            //    }
            //    if (Pontos.ControlSchemePlayer[playerID] == "Gamepad")
            //    {
            //        PauseNara[3].SetActive(value);
            //    }
            //}
            //if (focusedPlayer.name == "Ximas")
            //{
            //    if (Pontos.ControlSchemePlayer[playerID] == "Keyboard")
            //    {
            //        PauseXimas[0].SetActive(value);
            //    }
            //    if (Pontos.ControlSchemePlayer[playerID] == "KeyboardLeft")
            //    {
            //        PauseXimas[1].SetActive(value);
            //    }
            //    if (Pontos.ControlSchemePlayer[playerID] == "KeyboardRight")
            //    {
            //        PauseXimas[2].SetActive(value);
            //    }
            //    if (Pontos.ControlSchemePlayer[playerID] == "Gamepad")
            //    {
            //        PauseXimas[3].SetActive(value);
            //    }
            //}
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
        if (player[0].GetComponent<PlayerStats>().GetVida() != player[1].GetComponent<PlayerStats>().GetVida())
        {
            if (player[0].GetComponent<PlayerStats>().GetVida() > player[1].GetComponent<PlayerStats>().GetVida())
            {
                Pontos.vitoriaP[0]++;
                ganhador = 0;
            }
            else if (player[1].GetComponent<PlayerStats>().GetVida() > player[0].GetComponent<PlayerStats>().GetVida())
            {
                Pontos.vitoriaP[1]++;
                ganhador = 1;
            }


            player[ganhador].GetComponent<PlayerStats>().SomarPontos(1000);

            if (player[ganhador].GetComponent<PlayerStats>().GetVida() == player[ganhador].GetComponent<PlayerStats>().GetVidaMax())
            {
                Debug.Log("Perfect");
                player[ganhador].GetComponent<PlayerStats>().SomarPontos(1000);
            }

            if (Pontos.vitoriaP[0] > 1)
            {
                VitoriaTempo(0);
                StartCoroutine(FinishGame(0));
                finishGame = true;
            }
            if (Pontos.vitoriaP[1] > 1)
            {
                VitoriaTempo(1);
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
        WinnerPanelPlayer[novoGanhador].SetActive(true);
        pontosTelaWinner.text = player[novoGanhador].GetComponent<PlayerStats>().GetPontos().ToString();
        PanelManager.ChangePanel(1);
        //yield return new WaitForSeconds(2.5f);
        //SceneManager.LoadScene("Menu");
        yield return new WaitForSeconds(1f);
        ZerarPontosEVitorias();
        yield break;
    }

    public void ZerarPontosEVitorias()
    {
        for (int i = 0; i < player.Length; i++)
        {
            Pontos.pontosP[i] = 0;
            Pontos.vitoriaP[i] = 0;
            Pontos.tarticos[i] = 0;
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
        //ZerarPontosEVitorias();
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
