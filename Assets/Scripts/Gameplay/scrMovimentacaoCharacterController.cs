using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class scrMovimentacaoCharacterController : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] float speed;
    public CharacterController control;
    public bool isMoving;
    public float Horizontal;
    public float Vertical;

    [Header("Jump")]
    [SerializeField] Transform GroundCheck;
    [SerializeField] LayerMask ground;
    [SerializeField] float jumpForce;
    public bool isGrounded;
    float gravity = -9.8f;
    Vector3 velocity;

    // leonardo
    void Start()
    {
        control = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(GroundCheck.position, 0.1f, ground);
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");

        if (Horizontal != 0f) isMoving = true;
        else isMoving = false;
        
        Vector3 movement = new Vector3(Horizontal, 0f, 0f);
        velocity.y += gravity * Time.deltaTime;

        if (Vertical > 0 && isGrounded)
        {
            velocity.y = jumpForce;
        }
        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }

        control.Move(movement * speed * Time.deltaTime);
        control.Move(velocity * Time.deltaTime);
    }
}
