using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] float pontos1 = 0;
    [SerializeField] float pontos2 = 0;

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
    [SerializeField] Image healthBarPlayer1;
    [SerializeField] Image healthBarPlayer2;
    [SerializeField] Slider energyBarPlayer1;
    [SerializeField] Slider energyBarPlayer2;
    public float distance;
    public bool ChangedSide;

    public GameObject focusedPlayer;
    public GameObject notFocusedPlayer;
    private bool isPaused = false;

    [SerializeField] GameObject eventSystemManager;
    [SerializeField] GameObject PanelManager;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
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
    }

    void PlayerLife()
    {
        lifePlayer1 = Player1.GetComponent<PlayerStats>().life / Player1.GetComponent<PlayerStats>().maxLife;
        lifePlayer2 = Player2.GetComponent<PlayerStats>().life / Player2.GetComponent<PlayerStats>().maxLife;
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
    public void Acabou()
    {
        SceneManager.LoadScene(1);
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
            Debug.Log("EMPATE");
        }
        if (lifePlayer1 > lifePlayer2)
            pontos1++;
        else
            pontos2++;

        SceneManager.LoadScene("Game");
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
}
