using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerControls;

    [Header("Action Map Name References")]
    [SerializeField] private string actionMapName = "Player";

    [Header("Action Name References")]
    [SerializeField] private string move = "Move";
    [SerializeField] private string jump = "Jump";
    [SerializeField] private string crounched = "Crounched";
    [SerializeField] private string attack_ButtonNorth = "Attack/ButtonNorth";
    [SerializeField] private string attack_ButtonEast = "Attack/ButtonEast";
    [SerializeField] private string attack_ButtonWest = "Attack/ButtonWest";

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction crounchedAction;
    private InputAction attack_ButtonNorthAction;
    private InputAction attack_ButtonEastAction;
    private InputAction attack_ButtonWestAction;

    public Vector2 MoveInput { get; private set; }
    public float CrounchedValue { get; private set; }
    public bool JumpTriggered { get; private set; }
    public bool Attack_ButtonNorthTriggered { get; private set; }
    public bool Attack_ButtonEastTriggered { get; private set; }
    public bool Attack_ButtonWestTriggered { get; private set; }

    public static PlayerInputHandler Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
        jumpAction = playerControls.FindActionMap(actionMapName).FindAction(jump);
        crounchedAction = playerControls.FindActionMap(actionMapName).FindAction(crounched);
        attack_ButtonNorthAction = playerControls.FindActionMap(actionMapName).FindAction(attack_ButtonNorth);
        attack_ButtonEastAction = playerControls.FindActionMap(actionMapName).FindAction(attack_ButtonEast);
        attack_ButtonWestAction = playerControls.FindActionMap(actionMapName).FindAction(attack_ButtonWest);

        RegisterInputAction();
    }
    void RegisterInputAction()
    {
        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MoveInput = Vector2.zero;

        crounchedAction.performed += context => CrounchedValue = context.ReadValue<float>();
        crounchedAction.canceled += context => CrounchedValue = 0f;

        jumpAction.performed += context => JumpTriggered = true;
        jumpAction.canceled += context => JumpTriggered = false;

        attack_ButtonNorthAction.performed += context => Attack_ButtonNorthTriggered = true;
        attack_ButtonNorthAction.canceled += context => Attack_ButtonNorthTriggered = false;
        attack_ButtonEastAction.performed += context => Attack_ButtonEastTriggered = true;
        attack_ButtonEastAction.canceled += context => Attack_ButtonEastTriggered = false;
        attack_ButtonWestAction.performed += context => Attack_ButtonWestTriggered = true;
        attack_ButtonWestAction.canceled += context => Attack_ButtonWestTriggered = false;
    }
    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
        crounchedAction.Enable();
        attack_ButtonNorthAction.Enable();
        attack_ButtonEastAction.Enable();
        attack_ButtonWestAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
        crounchedAction.Disable();
        attack_ButtonNorthAction.Disable();
        attack_ButtonEastAction.Disable();
        attack_ButtonWestAction.Disable();
    }
}
