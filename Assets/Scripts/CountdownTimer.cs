using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEditor.Experimental.GraphView.GraphView;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

public class CountdownTimer : MonoBehaviour
{
    GameObject Player1;
    GameObject Player2;
    [SerializeField] Transform[] InstancePosition;
    [SerializeField] TMP_Text countdownText;
    float countdownTime = 3f;
    bool FinishedStart = false;

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
        //EnablePlayerInputs(false);
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

        //EnablePlayerInputs(true);
        yield break;
    }

    void EnablePlayerInputs(bool value)
    {
        Player1.GetComponent<PlayerInput>().enabled = value;
        Player2.GetComponent<PlayerInput>().enabled = value;
        Player1.GetComponent<PlayerController>().enabled = value;
        Player2.GetComponent<PlayerController>().enabled = value;
        Player1.GetComponent<PlayerMoveRigidbody>().enabled = value;
        Player2.GetComponent<PlayerMoveRigidbody>().enabled = value;
        Player1.GetComponent<PlayerCombat>().enabled = value;
        Player2.GetComponent<PlayerCombat>().enabled = value;
    }

}
