using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
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
    public bool value;
    public bool ChangedSide;

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
}
