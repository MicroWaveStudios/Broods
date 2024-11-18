using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Input Device Configurator", menuName = "Scriptable Objects/Input Device Configurator", order = 1)]
public class InputDeviceConfigurator : ScriptableObject
{
    [System.Serializable]
    public struct DeviceSet
    {
        public string deviceRawPath;
        public InputDeviceSettings inputDeviceSettings;
    }

    public List<DeviceSet> listDeviceSets = new List<DeviceSet>();


    public string GetDeviceName(PlayerInput playerInput)
    {

        string currentDeviceRawPath = playerInput.devices[0].ToString();

        string newDisplayName = null;

        for (int i = 0; i < listDeviceSets.Count; i++)
        {

            if (listDeviceSets[i].deviceRawPath == currentDeviceRawPath)
            {
                newDisplayName = listDeviceSets[i].inputDeviceSettings.deviceDisplayName;
            }
        }

        if (newDisplayName == null)
        {
            newDisplayName = currentDeviceRawPath;
        }

        return newDisplayName;

    }


    //public Color GetDeviceColor(PlayerInput playerInput)
    //{

    //    string currentDeviceRawPath = playerInput.devices[0].ToString();

    //    Color newDisplayColor;

    //    for (int i = 0; i < listDeviceSets.Count; i++)
    //    {

    //        if (listDeviceSets[i].deviceRawPath == currentDeviceRawPath)
    //        {
    //            newDisplayColor = listDeviceSets[i].deviceDisplaySettings.deviceDisplayColor;
    //        }
    //    }

    //    return newDisplayColor;

    //}

    public Sprite GetDeviceBindingIcon(PlayerInput playerInput, string playerInputDeviceInputBinding)
    {

        string currentDeviceRawPath = playerInput.devices[0].ToString();

        Sprite displaySpriteIcon = null;

        for (int i = 0; i < listDeviceSets.Count; i++)
        {
            if (listDeviceSets[i].deviceRawPath == currentDeviceRawPath)
            {
                displaySpriteIcon = FilterForDeviceInputBinding(listDeviceSets[i], playerInputDeviceInputBinding);
            }
        }

        return displaySpriteIcon;
    }

    Sprite FilterForDeviceInputBinding(DeviceSet targetDeviceSet, string inputBinding)
    {
        Sprite spriteIcon = null;

        switch (inputBinding)
        {
            case "Button North":
                spriteIcon = targetDeviceSet.inputDeviceSettings.buttonNorthIcon;
                break;

            case "Button South":
                spriteIcon = targetDeviceSet.inputDeviceSettings.buttonSouthIcon;
                break;

            case "Button West":
                spriteIcon = targetDeviceSet.inputDeviceSettings.buttonWestIcon;
                break;

            case "Button East":
                spriteIcon = targetDeviceSet.inputDeviceSettings.buttonEastIcon;
                break;

            case "Right Shoulder":
                spriteIcon = targetDeviceSet.inputDeviceSettings.triggerRightFrontIcon;
                break;

            case "Right Trigger":
                spriteIcon = targetDeviceSet.inputDeviceSettings.triggerRightBackIcon;
                break;

            case "rightTriggerButton":
                spriteIcon = targetDeviceSet.inputDeviceSettings.triggerRightBackIcon;
                break;

            case "Left Shoulder":
                spriteIcon = targetDeviceSet.inputDeviceSettings.triggerLeftFrontIcon;
                break;

            case "Left Trigger":
                spriteIcon = targetDeviceSet.inputDeviceSettings.triggerLeftBackIcon;
                break;

            case "leftTriggerButton":
                spriteIcon = targetDeviceSet.inputDeviceSettings.triggerLeftBackIcon;
                break;

            default:

                for (int i = 0; i < targetDeviceSet.inputDeviceSettings.customContextIcons.Count; i++)
                {
                    if (targetDeviceSet.inputDeviceSettings.customContextIcons[i].customInputContextString == inputBinding)
                    {
                        if (targetDeviceSet.inputDeviceSettings.customContextIcons[i].customInputContextIcon != null)
                        {
                            spriteIcon = targetDeviceSet.inputDeviceSettings.customContextIcons[i].customInputContextIcon;
                        }
                    }
                }


                break;

        }

        return spriteIcon;
    }
}
