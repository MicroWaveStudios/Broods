using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    GameObject Player1;
    GameObject Player2;
    Transform PlayerLeft;
    Transform PlayerRight;
    [SerializeField] Transform mid;
    GameObject Canvas;
    Image healthBarPlayer1;
    Image healthBarPlayer2;
    public float distance;
    public bool value;

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
            PlayerLeft = Player1.transform;
            PlayerRight = Player2.transform;
        }
        else
        {
            PlayerLeft = Player2.transform;
            PlayerRight = Player1.transform;
        }
        PlayerLeft.rotation = Quaternion.Euler(0f, 90f, 0f);
        PlayerRight.rotation = Quaternion.Euler(0f, -90f, 0f);
    }

    void MidPosition()
    {
        distance = Vector3.Distance(PlayerLeft.position, PlayerRight.position)/2f;
        mid.position = new Vector3(PlayerLeft.position.x + distance, 1f, mid.position.z);
    }

}
