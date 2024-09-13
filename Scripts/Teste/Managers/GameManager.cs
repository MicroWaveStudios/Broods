using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    //GameObject focusedPlayer;
    //GameObject notFocusedPlayer;
    //private bool isPaused = false;

    //public void Pause(GameObject newFocusedPlayer, bool value)
    //{
    //    if (focusedPlayer == null)
    //        focusedPlayer = newFocusedPlayer;
    //    if (focusedPlayer.tag == newFocusedPlayer.tag)
    //    {
    //        isPaused = value;
    //        SetActiveInputNotFocusedPlayer(focusedPlayer.tag, value);
    //        SwitchControlScheme(value);
    //        UIPause(value);
    //    }
    //}
    //void SwitchControlScheme(bool value)
    //{
    //    switch (value)
    //    {
    //        case true:
    //            focusedPlayer.GetComponent<PlayerController>().EnableMapActionUI();
    //            break;
    //        case false:
    //            focusedPlayer.GetComponent<PlayerController>().EnableMapActionPlayer();
    //            focusedPlayer = null;
    //            break;
    //    }
    //}
    //void SetActiveInputNotFocusedPlayer(string tag, bool value)
    //{
    //    switch (tag)
    //    {
    //        case "Player1":
    //            notFocusedPlayer = GameObject.FindGameObjectWithTag("Player2");
    //            notFocusedPlayer.GetComponent<PlayerController>().SetInputActive(value);
    //            break;
    //        case "Player2":
    //            notFocusedPlayer = GameObject.FindGameObjectWithTag("Player1");
    //            notFocusedPlayer.GetComponent<PlayerController>().SetInputActive(value);
    //            break;
    //    }
    //    Debug.Log(notFocusedPlayer.tag);
    //}
    //public void UIPause(bool value)
    //{
    //    GameObject UIManager = GameObject.FindGameObjectWithTag("UIManager");
    //    UIManager.GetComponent<UIManager>().UIStatePause(value);
    //}
    //public bool GetBooleanIsPaused()
    //{
    //    return isPaused;
    //}
}

