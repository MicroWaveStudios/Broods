using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerInputHandler inputHandler;

    [Header("Move")]
    [SerializeField] float speed;
    public CharacterController control;
    public Vector3 movement = Vector3.zero;
    public bool isMoving;

    [Header("Jump")]
    [SerializeField] Transform GroundCheck;
    [SerializeField] LayerMask ground;
    [SerializeField] float jumpForce;
    public bool isGrounded;
    public bool test;
    float gravity = -9.8f;
    Vector3 velocity;

    void Start()
    {
        control = GetComponent<CharacterController>();
        inputHandler = PlayerInputHandler.Instance.GetComponent<PlayerInputHandler>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(GroundCheck.position, 0.1f, ground);
        if (isGrounded) 
        {
            HandleMovement();
        }
        test = inputHandler.JumpTriggered;
    }

    void HandleMovement()
    {
        movement = new Vector3(inputHandler.MoveInput.x, inputHandler.MoveInput.y, transform.position.z);
        if (movement.x != 0f)
        {
            isMoving = true;
            Debug.Log("Se moveu");
        }
        else isMoving = false;

        velocity.y += gravity * Time.deltaTime;

        if (inputHandler.JumpTriggered)
        {
            velocity.y = jumpForce;
        }
        if (velocity.y < 0f)
        {
            velocity.y = -2f;
        }
        control.Move(movement * speed * Time.deltaTime);
        control.Move(velocity * Time.deltaTime);
    }

}
