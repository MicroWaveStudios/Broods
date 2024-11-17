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
    public string controlScheme; //Mapa de a��o utilizado pelo player
    string deviceName; //dispositivo do player
    string rawPathName;
    int numeroPersonagem;
    int VarianteSkin;

    Gamepad dispositivo;

    [Header("Device Display Settings")]
    //[SerializeField] DeviceDisplayConfigurator deviceDisplaySettings;

    GameObject connectManager;
    //ConnectPlayerInMenu connectManager;
    SelecaoPersonagem selecaoPersonagem;

    bool CanDoIt = false;

    private void Awake()
    {
        StartCoroutine(PlayerCanDoIt());
        playerInput = GetComponent<PlayerInput>();
        selecaoPersonagem = GetComponent<SelecaoPersonagem>();
        connectManager = GameObject.FindGameObjectWithTag("ConnectManager");
    }
    private void Start()
    {
        connectManager.GetComponent<ConnectPlayerInMenu>().JoinPlayer(this.gameObject);
    }
    public void SetupPlayer(int newPlayerID)
    { 
        playerID = newPlayerID;
        DevicePlayer();
        selecaoPersonagem.TrocarCorDaBorda(newPlayerID);
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
        //deviceName = deviceDisplaySettings.GetDeviceName(playerInput); //Nome do dispositivo conectado

        deviceName = playerInput.devices[0].ToString();

        controlScheme = playerInput.GetComponent<PlayerInput>().currentControlScheme.ToString(); //Nome do controlScheme sendo usado pelo player
    }
    public void DisconnectPlayer(InputAction.CallbackContext context)
    {
        if (CanDoIt && connectManager.GetComponent<ConnectPlayerInMenu>().GetEtapa() == "conectar")
        {
            connectManager.GetComponent<ConnectPlayerInMenu>().DisableSplitKeyboard(playerID);
            //if (controlScheme == "KeyboardLeft" || controlScheme == "KeyboardRight")
            //{
            //    connectManager.GetComponent<ConnectPlayerInMenu>().DisableSplitKeyboard(playerID);
            //}
            //else
            //{ 
            //    connectManager.GetComponent<ConnectPlayerInMenu>().ConnectDisconnectPlayer(this.gameObject, playerID, controlScheme, false);
            //}
        }

    }
    public void ConnectSplitKeyboard(InputAction.CallbackContext context)
    {
        if (context.started && CanDoIt && controlScheme == "Keyboard" && context.started && connectManager.GetComponent<ConnectPlayerInMenu>().GetPlayerInScene() == 1)
        {
            connectManager.GetComponent<ConnectPlayerInMenu>().EnableSplitKeyboard();
        }
    }

    public void SetarPersonagem(int newNumeroPersonagem)
    { 
        numeroPersonagem = newNumeroPersonagem;
    }

    public IEnumerator PlayerCanDoIt()
    {
        CanDoIt = false;
        yield return new WaitForSeconds(0.3f);
        CanDoIt = true;
        yield break;
    }

    public void SetVarianteSkin(int value)
    {
        VarianteSkin = value;
    }

    public void SetNumeroPersonagem(int newNumeroPersonagem)
    {
        numeroPersonagem = newNumeroPersonagem;
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
