using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

enum GameMode
{ 
    Splikeyboard,
    ConnectWithButton
}
enum PlayerOne
{ 
    Gamepad,
    Keyboard
}

public class GameTest : MonoBehaviour
{
    [SerializeField] GameMode gameMode;
    [SerializeField] PlayerOne playerOne;
    private void Start()
    {

        switch (gameMode)
        { 
            case GameMode.Splikeyboard:
                GetComponent<PlayerInputManager>().DisableJoining();
                SplitKeyboard();
                break; 
            case GameMode.ConnectWithButton:
                GetComponent<PlayerInputManager>().DisableJoining();
                Keyboard_Gamepad();
                break;
        }
    }

    void SplitKeyboard()
    {
        var p1 = PlayerInput.Instantiate(GetComponent<PlayerInputManager>().playerPrefab, 0, "KeyboardLeft", -1, Keyboard.current, Mouse.current);
        var p2 = PlayerInput.Instantiate(GetComponent<PlayerInputManager>().playerPrefab, 1, "KeyboardRight", -1, Keyboard.current);
    }

    int playerGamepad;
    int playerKeyboard;
    void Keyboard_Gamepad()
    {
        switch (playerOne)
        { 
            case PlayerOne.Gamepad:
                playerGamepad = 0;
                playerKeyboard = 1;
                break;
            case PlayerOne.Keyboard:
                playerKeyboard = 0;
                playerGamepad = 1;
                break;
        }
        var pGamepad = PlayerInput.Instantiate(GetComponent<PlayerInputManager>().playerPrefab, playerGamepad, "Keyboard", -1, Keyboard.current, Mouse.current);
        var pKeyboard = PlayerInput.Instantiate(GetComponent<PlayerInputManager>().playerPrefab, playerKeyboard, "Gamepad", -1,  Gamepad.current);
    }

}
