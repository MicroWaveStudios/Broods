using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ConnectPlayer : MonoBehaviour
{
    //PlayerID
    private int playerID; //numero do player
    PlayerInput playerInput;
    private string controlScheme; //Mapa de ação utilizado pelo player
    private string deviceName; //dispositivo do player

    [Header("Device Display Settings")]
    public DeviceDisplayConfigurator deviceDisplaySettings;

    GameObject connectManager;

    private void Awake()
    {
        connectManager = GameObject.FindGameObjectWithTag("ConnectManager");
        playerInput = this.gameObject.GetComponent<PlayerInput>();
    }
    private void Start()
    {
        connectManager.GetComponent<ConnectPlayerInMenu>().SetupPlayer(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetupPlayer(int newPlayerID)
    { 
        playerID = newPlayerID;
        DevicePlayer();
        connectManager.GetComponent<ConnectPlayerInMenu>().ConnectPlayer(playerID, controlScheme);
    }

    void DevicePlayer()
    {
        deviceName = deviceDisplaySettings.GetDeviceName(playerInput); //Nome do dispositivo conectado

        controlScheme = playerInput.GetComponent<PlayerInput>().currentControlScheme.ToString(); //Nome do controlScheme sendo usado pelo player
    }
}
