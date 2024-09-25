using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ConnectPlayer : MonoBehaviour
{
    //PlayerID
    public int playerID; //numero do player
    PlayerInput playerInput;
    public string controlScheme; //Mapa de ação utilizado pelo player
    public string deviceName; //dispositivo do player
    public string rawPathName;

    [Header("Device Display Settings")]
    public DeviceDisplayConfigurator deviceDisplaySettings;

    GameObject connectManager;
    bool CanDoIt = false;

    private void Awake()
    {
        StartCoroutine(PlayerCanDoIt());
        connectManager = GameObject.FindGameObjectWithTag("ConnectManager");
        playerInput = this.gameObject.GetComponent<PlayerInput>();
    }
    private void Start()
    {
        connectManager.GetComponent<ConnectPlayerInMenu>().JoinPlayer(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }
    public void SetupPlayer(int newPlayerID)
    { 
        playerID = newPlayerID;
        DevicePlayer();
        connectManager.GetComponent<ConnectPlayerInMenu>().ConnectDisconnectPlayer(this.gameObject, playerID, controlScheme, true);
    }
    void DevicePlayer()
    {
        deviceName = deviceDisplaySettings.GetDeviceName(playerInput); //Nome do dispositivo conectado

        controlScheme = playerInput.GetComponent<PlayerInput>().currentControlScheme.ToString(); //Nome do controlScheme sendo usado pelo player
    }
    public void DisconnectPlayer(InputAction.CallbackContext context)
    {
        if (CanDoIt && !this.gameObject.GetComponent<PlayerController>().SceneGame())
        {
            connectManager.GetComponent<ConnectPlayerInMenu>().ConnectDisconnectPlayer(this.gameObject, playerID, controlScheme, false);
        }
    }
    public void ConnectSplitKeyboard(InputAction.CallbackContext context)
    {
        if (CanDoIt && controlScheme == "Keyboard" && context.started)
        {
            connectManager.GetComponent<ConnectPlayerInMenu>().EnableSplitKeyboard();
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
    public string GetControlScheme()
    { 
        return controlScheme;
    }
    public string GetDeviceName()
    { 
        return deviceName;
    }
}
