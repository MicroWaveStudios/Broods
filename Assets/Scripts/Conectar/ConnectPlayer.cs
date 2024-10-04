using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ConnectPlayer : MonoBehaviour
{
    //PlayerID
    int playerID; //numero do player
    PlayerInput playerInput;
    string controlScheme; //Mapa de ação utilizado pelo player
    string deviceName; //dispositivo do player
    string rawPathName;
    int numeroPersonagem;

    Gamepad dispositivo;

    [Header("Device Display Settings")]
    [SerializeField] DeviceDisplayConfigurator deviceDisplaySettings;

    GameObject connectManager;
    bool CanDoIt = false;

    private void Awake()
    {
        StartCoroutine(PlayerCanDoIt());
        playerInput = GetComponent<PlayerInput>();
        connectManager = GameObject.FindGameObjectWithTag("ConnectManager");
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


        //if (controlScheme == "Gamepad")
        //{
        //    var device = new Gamepad();
        //    InputSystem.AddDevice(device);
        //}
        //else
        //{
        //    var device = new Keyboard();
        //    InputSystem.AddDevice(device);
        //}
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
            //connectManager.GetComponent<ConnectPlayerInMenu>().EnableSplitKeyboard();
        }
    }

    public void SetarPersonagem(int newNumeroPersonagem)
    { 
        numeroPersonagem = newNumeroPersonagem;
    }

    IEnumerator PlayerCanDoIt()
    {
        yield return new WaitForSeconds(0.1f);
        CanDoIt = true;
        yield break;
    }

    public int GetNumeroPersonagem()
    { 
        return numeroPersonagem;
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
    public string GetDispositivo()
    {
        return dispositivo.ToString();
    }
}
