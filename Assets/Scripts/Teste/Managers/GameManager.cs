using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public int limitPlayer;
    [SerializeField] int playerInScene;
    public GameObject focusedPlayer;
    private bool isPaused = false;

    //[SerializeField] GameObject ContinueButton;

    public void Pause(GameObject newFocusedPlayer, bool value)
    {
        if (focusedPlayer == null)
            focusedPlayer = newFocusedPlayer;
        if (focusedPlayer.tag == newFocusedPlayer.tag)
        {
            isPaused = value;
            SetActiveInputNotFocusedPlayer();
            SwitchControlScheme();
            UIPause(value);
        }
    }


    void SwitchControlScheme()
    {
        switch (isPaused)
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
    void SetActiveInputNotFocusedPlayer()
    {
        GameObject notFocusedPlayer;
        switch (focusedPlayer.CompareTag("Player1"))
        {
            case true:
                notFocusedPlayer = GameObject.FindGameObjectWithTag("Player2");
                notFocusedPlayer.GetComponent<PlayerController>().SetInputActive(isPaused);
                break;
            case false:
                notFocusedPlayer = GameObject.FindGameObjectWithTag("Player1");
                notFocusedPlayer.GetComponent<PlayerController>().SetInputActive(isPaused);
                break;
        }
    }
    public void UIPause(bool value)
    {
        GameObject UIManager = GameObject.FindGameObjectWithTag("UIManager");
        UIManager.GetComponent<UIManager>().UIStatePause(value);
    }
































    public void SetupPlayer(GameObject spawnedPlayer)
    {
        if (playerInScene < limitPlayer)
        {
            spawnedPlayer.GetComponent<PlayerController>().SetupPlayer(playerInScene);

            playerInScene++;
        }
        //if (playerInScene == limitPlayer)
        //ContinueButton.SetActive(true);
    }

    //public void OnPause(GameObject newFocusedPlayer, bool value)
    //{
    //    if (focusedPlayer == null && value)
    //        focusedPlayer = newFocusedPlayer;
    //    else
    //        focusedPlayer = null;
    //    StartCoroutine(Pause(value));
    //}

    //IEnumerator Pause(bool value)
    //{
    //    isPaused = value;
    //    yield return new WaitForSeconds(0.02f);
    //    SetInputActiveNotFocusedPlayer(focusedPlayer);
    //    SwitchFocusedPlayerControlScheme();
    //    UIPause(value);
    //}

    public bool GetBooleanIsPaused()
    {
        return isPaused;
    }

    //void SetInputActiveNotFocusedPlayer(GameObject FocusedPlayer)
    //{
    //    GameObject notFocusedPlayer;
    //    if (FocusedPlayer.tag == "Player1")
    //    {
    //        notFocusedPlayer = GameObject.FindGameObjectWithTag("Player2");
    //        notFocusedPlayer.GetComponent<PlayerController>().SetInputActive(isPaused);
    //    }
    //    else
    //    {
    //        notFocusedPlayer = GameObject.FindGameObjectWithTag("Player1");
    //        notFocusedPlayer.GetComponent<PlayerController>().SetInputActive(isPaused);
    //    }
    //}

    //void SwitchFocusedPlayerControlScheme()
    //{
    //    switch (isPaused)
    //    {
    //        case true:
    //            focusedPlayer.GetComponent<PlayerController>().EnableMapActionUI();
    //            break;
    //        case false:
    //            focusedPlayer.GetComponent<PlayerController>().EnableMapActionPlayer();
    //            break;
    //    }
    //}

    //public void UIPause(bool value)
    //{
    //    GameObject UIManager = GameObject.FindGameObjectWithTag("UIManager");
    //    UIManager.GetComponent<UIManager>().UIStatePause(value);
    //}













    //public void TogglePauseState(GameObject newFocusedPlayerController)
    //{
    //    focusedPlayerController = newFocusedPlayerController.GetComponent<PlayerController>();
    //    isPaused = true;

    //    SetInputActiveNotFocusedPlayer(focusedPlayerController);

    //    SwitchFocusedPlayerControlScheme();

    //    UIPause(isPaused);
    //    Debug.Log(isPaused);
    //}
}

