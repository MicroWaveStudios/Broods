using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    float pontos1 = 0;
    float pontos2 = 0;


    GameObject Player1;
    GameObject Player2;
    Transform PlayerLeft;
    Transform PlayerRight;
    [SerializeField] GameObject[] PrefabPlayer;
    [SerializeField] Transform[] InstancePosition;
    [SerializeField] Transform mid;
    GameObject Canvas;
    Image healthBarPlayer1;
    Image healthBarPlayer2;
    [SerializeField] Slider energyBarPlayer1;
    [SerializeField] Slider energyBarPlayer2;
    public float distance;
    public bool ChangedSide;

    GameObject focusedPlayer;
    GameObject notFocusedPlayer;
    private bool isPaused = false;


    PlayerInputManager playerInputManager;

    private void Awake()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
    }
    
    private void Update()
    {
        Canvas = GameObject.FindGameObjectWithTag("Canvas");
        Player1 = GameObject.FindGameObjectWithTag("Player1");
        Player2 = GameObject.FindGameObjectWithTag("Player2");

        if (Player1 != null && Player2 != null)
        {
            ChangePlayer();
            MidPosition();
            PlayerLife();
            PlayerEnergy();
        }

        //if (Player1 == null || Player2 == null)
        //{
        //    if (Player1 == null)
        //    {
        //        pontos2++;
        //        PlayerPrefs.SetFloat("pontosPlayer2", pontos2);
        //    }

        //    if (Player2 == null)
        //    {
        //        pontos1++;
        //        PlayerPrefs.SetFloat("pontosPlayer1", pontos1);
        //    }

        //    MudarRodada();
        //}

        //if (PlayerPrefs.GetFloat("pontosPlayer1") == 2f || PlayerPrefs.GetFloat("pontosPlayer2") == 2f)
        //{
        //    Acabou();
        //}
    }

    void PlayerLife()
    {
        float lifePlayer1 = Player1.GetComponent<PlayerStats>().life / Player1.GetComponent<PlayerStats>().maxLife;
        float lifePlayer2 = Player2.GetComponent<PlayerStats>().life / Player2.GetComponent<PlayerStats>().maxLife;
        healthBarPlayer1 = Canvas.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        healthBarPlayer2 = Canvas.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        healthBarPlayer1.fillAmount = lifePlayer1;
        healthBarPlayer2.fillAmount = lifePlayer2;
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
        PlayerLeft.localScale = new Vector3(1f, 1f, 1f);
        PlayerRight.localScale = new Vector3(1f, 1f, -1f);
    }

    void MidPosition()
    {
        distance = Vector3.Distance(PlayerLeft.position, PlayerRight.position)/2f;
        mid.position = new Vector3(PlayerLeft.position.x + distance, 1.5f, mid.position.z);
    }

    public void Pause(GameObject newFocusedPlayer, bool value)
    {
        if (focusedPlayer == null)
            focusedPlayer = newFocusedPlayer;
        if (focusedPlayer.tag == newFocusedPlayer.tag)
        {
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
        Debug.Log(notFocusedPlayer.tag);
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

    public void MudarRodada()
    {
        SceneManager.LoadScene(12);

        Debug.Log(PlayerPrefs.GetFloat("A " + "pontosPlayer1"));
        Debug.Log(PlayerPrefs.GetFloat("B " + "pontosPlayer2"));
    }

    public void Acabou()
    {
        SceneManager.LoadScene(1);
    }
}
