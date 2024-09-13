using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] GameController gameController;

    GameObject Player1;
    GameObject Player2;
    [SerializeField] Transform[] InstancePosition;
    [SerializeField] TMP_Text countdownText;
    [SerializeField] TMP_Text TxtTimerGame;
    float countdownTime = 3f;
    bool FinishedStart = false;
    bool isPaused;
    bool gameFinished;

    private void Update()
    {
        Player1 = GameObject.FindGameObjectWithTag("Player1");
        Player2 = GameObject.FindGameObjectWithTag("Player2");

        if (Player1 != null && Player2 != null && !FinishedStart)
        {
            StartCoroutine(CountdownStart());
            FinishedStart = true;
        }
    }

    private IEnumerator CountdownStart()
    {
        isPaused = true;
        EnablePlayerInputs(false);
        Player1.transform.position = InstancePosition[0].position;
        Player2.transform.position = InstancePosition[1].position;

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
        StartCoroutine(TimerGame());
        yield break;
    }


    int i;
    IEnumerator TimerGame()
    {
        for (i = 99; i > 1; i--)
        {
            if (i >= 10)
                TxtTimerGame.text = i.ToString();
            else
                TxtTimerGame.text = "0" + i.ToString();
            yield return new WaitForSeconds(1f);
        }
        gameController.GameFinished();
    }



    public bool GetBooleanIsPaused()
    {
        return isPaused;
    }

    void EnablePlayerInputs(bool value)
    {
        Player1.GetComponent<PlayerInput>().enabled = value;
        Player2.GetComponent<PlayerInput>().enabled = value;
    }

}
