using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ConnectPlayer : MonoBehaviour
{
    //PlayerID
    int playerID; //numero do player
    PlayerInput playerInput;
    string deviceName; //dispositivo do player

    [Header("Device Display Settings")]
    public DeviceDisplayConfigurator deviceDisplaySettings;

    public void SetupPlayer(int newPlayerID, PlayerInput newPlayerInput)
    { 
        playerID = newPlayerID;
        playerInput = newPlayerInput;

        DevicePlayer();

        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        gameController.GetComponent<ConnectPlayerInMenu>().ConnectPlayer(playerID, deviceName);
    }

    void DevicePlayer()
    {
        //deviceName = deviceDisplaySettings.GetDeviceName(playerInput); //Nome do dispositivo conectado

        deviceName = playerInput.GetComponent<PlayerInput>().currentControlScheme.ToString(); //Nome do controlScheme sendo usado pelo player
    }
}
