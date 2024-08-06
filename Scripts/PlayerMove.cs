using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using Unity.VisualScripting;

public class PlayerMove : MonoBehaviour
{

    private PlayerInputs playerInputs;
    private InputAction move;

    Animator anim;

    private CharacterController characterController;
    [SerializeField] float moveForce;
    [SerializeField] float jumpForce;
    private Vector3 velocity;
    Vector3 movement;

    [SerializeField] Transform GroundCheck;
    [SerializeField] LayerMask ground;
    float gravity = -9.8f;

    public bool InAttack = false;
    [SerializeField] Transform Hand;
    GameObject FinishAttack;
    [SerializeField] GameObject Attack;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        playerInputs = new PlayerInputs();
    }

    private void OnEnable()
    {
        playerInputs.Player.Jump.started += OnJump;
        playerInputs.Player.AttackButtonWest.started += Punch0; 
        move = playerInputs.Player.Move;
        playerInputs.Player.Enable();
    }
    private void OnDisable()
    {
        playerInputs.Player.Jump.started -= OnJump;
        playerInputs.Player.AttackButtonWest.started -= Punch0;
        playerInputs.Player.Disable();
    }

    #region Movement
    private void Update()
    {
        if (IsGrounded() && !InAttack)
        { 
            movement = new Vector3(move.ReadValue<Vector2>().x, 0f, 0f);
            characterController.Move(movement * moveForce * Time.deltaTime);
        }

        velocity.y += gravity * Time.deltaTime;

        if (velocity.y < 0f && IsGrounded())
            velocity.y = -2f;

        characterController.Move(velocity * Time.deltaTime);
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        anim.SetTrigger("Jump");
        if (IsGrounded())
            velocity.y += jumpForce;
    }

    public bool IsGrounded()
    {
        if (Physics.CheckSphere(GroundCheck.position, 0.1f, ground))
            return true;
        else
            return false;
    }
#endregion

    #region Attacks
    private void Punch0(InputAction.CallbackContext context)
    {
        if (!InAttack)
            StartCoroutine(PrimeiroAtaque());
    }

    IEnumerator PrimeiroAtaque()
    {
        anim.SetTrigger("Punch0");
        InAttack = true;
        yield return new WaitForSeconds(0.2f);
        FinishAttack = Instantiate(Attack, Hand.position, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        Destroy(FinishAttack);
        yield return new WaitForSeconds(0.2f);
        InAttack = false;
    }

    #endregion
}
