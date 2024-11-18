using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class ChangeInputDevice : MonoBehaviour
{
    private InputActionAsset focusedInputActionAsset;
    private PlayerInput focusedPlayerInput;
    private InputAction focusedInputAction;
    private InputActionRebindingExtensions.RebindingOperation rebindOperation;

    [Header("Bind Settings")]
    public string actionName;

    [Header("Input Device Settings")]
    public InputDeviceConfigurator inputDeviceSettings;

    [Header("UI Display - Binding Text or Icon")]
    public Image bindingIconDisplayImage;

    [Header("UI Display - Buttons")]
    public GameObject rebindButtonObject;
    public GameObject resetButtonObject;


}
