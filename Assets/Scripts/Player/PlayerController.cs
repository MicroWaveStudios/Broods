using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
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

    [Header("Input Device Configurator")]
    [SerializeField] InputDeviceConfigurator inputDeviceConfigurator;

    [Header("Input Action Maps")]
    private string actionMapPlayer = "Player";
    private string actionMapUI = "UI";
    private string actionMapStatic = "Static";

    //bool isPaused = false;
    GameObject gameManager;
    bool CanDoIt = false;

    Scene scene;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "GameAcademia" || SceneManager.GetActiveScene().name == "GameBarDoBanana")
        {
            scene = Scene.Game;
        }
    }

    private void Start()
    {
        if (this.gameObject.CompareTag("Player1"))
        {
            playerID = 0;
        }
        else
        {
            playerID = 1;
        }

        StartCoroutine(PlayerCanDoIt());
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
    }
    public void EnableMapActionUI()
    {
        playerInput.SwitchCurrentActionMap(actionMapUI);
    }
    public void EnableMapActionStatic()
    {
        playerInput.SwitchCurrentActionMap(actionMapStatic);
    }
    public void PauseOn(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (scene == Scene.Game)
            { 
                gameManager.GetComponent<GameController>().PauseOn_Off(this.gameObject, true);
            }
        }
    }
    public void PauseOff(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (scene == Scene.Game)
            { 
                gameManager.GetComponent<GameController>().PauseOn_Off(this.gameObject, false);
            }
        }
    }
    public bool GetSceneGame()
    {
        if (scene == Scene.Game)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void EnableSplitKeyboard(InputAction.CallbackContext context)
    {
        if (Pontos.SplitKeyboard && GetComponent<PlayerInput>().currentControlScheme.ToString() == "Keyboard" && CanDoIt && context.started)
        {
            //GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameController>().EnableSplitKeyboard(this.gameObject);
        }
    }

    IEnumerator PlayerCanDoIt()
    {
        yield return new WaitForSeconds(0.1f);
        CanDoIt = true;
        yield break;
    }

    public int GetPlayerID()
    {
        return playerID;
    }
}
