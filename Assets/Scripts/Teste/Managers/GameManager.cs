using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    GameObject focusedPlayer;
    private bool isPaused = false;

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
    public bool GetBooleanIsPaused()
    {
        return isPaused;
    }
}

