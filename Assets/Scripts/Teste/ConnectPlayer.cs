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

    [Header("Device Display Settings")]
    public DeviceDisplayConfigurator deviceDisplaySettings;

    GameObject connectManager;
    bool CanDisconnect = false;

    private void Awake()
    {
        StartCoroutine(PlayerCanDisconnect());
        connectManager = GameObject.FindGameObjectWithTag("ConnectManager");
        playerInput = this.gameObject.GetComponent<PlayerInput>();
    }
    private void Start()
    {
        connectManager.GetComponent<ConnectPlayerInMenu>().SetupNewPlayer(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }
    public void SetupPlayer(int newPlayerID)
    { 
        playerID = newPlayerID;
        DevicePlayer();
        connectManager.GetComponent<ConnectPlayerInMenu>().ConnectDisconnectPlayer(playerID, controlScheme, true);
    }
    void DevicePlayer()
    {
        deviceName = deviceDisplaySettings.GetDeviceName(playerInput); //Nome do dispositivo conectado

        controlScheme = playerInput.GetComponent<PlayerInput>().currentControlScheme.ToString(); //Nome do controlScheme sendo usado pelo player
    }
    public void DisconnectPlayer(InputAction.CallbackContext context)
    {
        if (CanDisconnect)
        { 
            connectManager.GetComponent<ConnectPlayerInMenu>().ConnectDisconnectPlayer(playerID, controlScheme, false);
            Destroy(gameObject);
        }
    }

    IEnumerator PlayerCanDisconnect()
    {
        yield return new WaitForSeconds(1f);
        CanDisconnect = true;
        yield break;
    }
}
