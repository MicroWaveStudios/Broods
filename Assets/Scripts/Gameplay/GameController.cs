using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

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
    public float distance;
    public bool value;
    public bool TrocouLado;

    [SerializeField] TMP_Text countdownText;

#pragma warning disable CS0414 // O campo "GameController.x" é atribuído, mas seu valor nunca é usado
    int x = 0;
#pragma warning restore CS0414 // O campo "GameController.x" é atribuído, mas seu valor nunca é usado
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
            //if (!FinishTimer)
            //{ 
            //    StartCoroutine(CountdownStart());
            //}
            
            ChangePlayer();
            MidPosition();
            PlayerLife();
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


    void ChangePlayer()
    {
        if (Player1.transform.position.x < Player2.transform.position.x)
        {
            //Esta na esquerda (posicao inicial)
            TrocouLado = false;
            PlayerLeft = Player1.transform;
            PlayerRight = Player2.transform;
        }
        else
        {
            //Esta na direita (posicao modificada)
            TrocouLado = true;
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

    //IEnumerator CountdownStart()
    //{
    //    while (Player1 != null && Player2 != null) 

    //    Debug.Log("START GAME");
    //    Player1.transform.position = InstancePosition[0].position;
    //    Player2.transform.position = InstancePosition[1].position;
    //    Player1.GetComponent<PlayerMoveRigidbody>().enabled = false;
    //    Player2.GetComponent<PlayerMoveRigidbody>().enabled = false;
    //    Player1.GetComponent<PlayerCombat>().enabled = false;
    //    Player2.GetComponent<PlayerCombat>().enabled = false;

    //    countdownText.transform.gameObject.SetActive(true);
    //    while (countdownTime > 0f)
    //    {
    //        Debug.Log(countdownTime);
    //        countdownText.text = countdownTime.ToString();
    //        yield return new WaitForSeconds(1f);
    //        countdownTime--;
    //    }
    //    countdownText.text = "FIGHT!";
    //    yield return new WaitForSeconds(1f);
    //    countdownText.transform.gameObject.SetActive(false);
    //    FinishTimer = true;

    //    Player1.GetComponent<PlayerMoveRigidbody>().enabled = true;
    //    Player2.GetComponent<PlayerMoveRigidbody>().enabled = true;
    //    Player1.GetComponent<PlayerCombat>().enabled = true;
    //    Player2.GetComponent<PlayerCombat>().enabled = true;
    //}
}
