using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Linq;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] GameController gameController;

    GameObject[] player = new GameObject[2];
    [SerializeField] Transform[] InstancePosition;
    [SerializeField] TMP_Text countdownText;
    [SerializeField] TMP_Text TxtTimerGame;
    float countdownTime = 3f;
    bool FinishedStart = false;
    bool isPaused;
    bool gameFinished;
    bool podeComecar;

    [SerializeField] int tempo;

    private void Update()
    {
        player[0] = GameObject.FindGameObjectWithTag("Player1");
        player[1] = GameObject.FindGameObjectWithTag("Player2");

        if (player[0] != null && player[1] != null && !FinishedStart && podeComecar)
        {
            StartCoroutine(CountdownStart());
            FinishedStart = true;
        }
        //if (podeComecar)
        //{
        //    Player1.transform.position = InstancePosition[0].position;
        //    Player2.transform.position = InstancePosition[1].position;
        //}
    }

    private IEnumerator CountdownStart()
    {
        isPaused = true;
        EnablePlayerInputs(false);
        for (int i = 0; i < player.Length; i++)
        {
            player[i].transform.position = InstancePosition[i].position;
        }

        countdownText.transform.gameObject.SetActive(true);


        while (countdownTime > 0f)
        { 
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }
        countdownText.text = "CAI PRA PORRADA!";
        yield return new WaitForSeconds(1f);
        countdownText.transform.gameObject.SetActive(false);

        EnablePlayerInputs(true);
        isPaused = false;
        podeComecar = false;
        StartCoroutine(TimerGame());
        yield break;
    }


    int i;
    IEnumerator TimerGame()
    {
        for (i = tempo; i > 1; i--)
        {
            if (i >= 10)
                TxtTimerGame.text = i.ToString();
            else
                TxtTimerGame.text = "0" + i.ToString();
            yield return new WaitForSeconds(1f);
            yield return new WaitUntil(() => !gameController.GetBooleanIsPaused());
        }
        gameController.GameFinished();
    }
    public bool GetBooleanIsPaused()
    {
        return isPaused;
    }

    public void SetPodeComecar(bool value)
    { 
        podeComecar = value;
    }

    void EnablePlayerInputs(bool value)
    {
        for (int i = 0; i < player.Length; i++)
        {
            if (value)
            {
                player[i].GetComponent<PlayerController>().EnableMapActionPlayer();
            }
            else
            {
                player[i].GetComponent<PlayerController>().EnableMapActionStatic();
            }
        }
    }

}
