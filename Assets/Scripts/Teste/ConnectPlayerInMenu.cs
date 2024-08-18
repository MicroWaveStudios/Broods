using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConnectPlayerInMenu : MonoBehaviour
{
    [SerializeField] GameObject[] PlayerMenu;
    [SerializeField] GameObject[] KeyboardMenu;
    [SerializeField] GameObject[] GamepadMenu;

    [SerializeField] GameObject[] EnterMesage;

    public void ConnectPlayer(int playerID, string deviceName)
    {
        EnterMesage[playerID].SetActive(false);

        PlayerMenu[playerID].SetActive(true);

        if (deviceName == "Xbox" || deviceName == "Playstation" || deviceName == "NintendoSwitch")
            GamepadMenu[playerID].SetActive(true);
        else
            KeyboardMenu[playerID].SetActive(true);
    }
}
