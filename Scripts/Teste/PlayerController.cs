using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

enum Scene
{ 
    Connect,
    Game
}

public class PlayerController : MonoBehaviour
{
    //Player ID
    private int playerID;
    private string controlScheme;
    private string deviceName;

    [Header("Input Settings")]
    public PlayerInput playerInput;

    [Header("Input Action Maps")]
    private string actionMapPlayer = "Player";
    private string actionMapUI = "UI";

    //bool isPaused = false;
    GameObject gameManager;

    Scene scene;

    private void Awake()
    {
        scene = Scene.Connect;
    }

    private void Update()
    {
        switch (scene)
        { 
            case Scene.Connect:

                break;
            case Scene.Game:
                gameManager = GameObject.FindGameObjectWithTag("GameManager");

                break;
        }
    }

    public bool SceneGame()
    {
        if (scene == Scene.Game)
            return true;
        else 
            return false;
    }

    public void EnableMapActionPlayer()
    {
        playerInput.SwitchCurrentActionMap(actionMapPlayer);
        scene = Scene.Game;
    }
    public void EnableMapActionUI()
    {
        playerInput.SwitchCurrentActionMap(actionMapUI);
    }
    public void PauseOn(InputAction.CallbackContext context)
    {
        if (scene == Scene.Game)
            gameManager.GetComponent<GameController>().Pause(this.gameObject, true);
    }
    public void PauseOff(InputAction.CallbackContext context)
    {
        if (scene == Scene.Game)
            gameManager.GetComponent<GameController>().Pause(this.gameObject, false);
    }
}
