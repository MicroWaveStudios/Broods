using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

public class PlayerAnimator : MonoBehaviour
{
    private PlayerInputs playerInputs;
    private InputAction move;
    PlayerMoveRigidbody playerMove;
    Rigidbody rb;
    Animator anim;

    private void Awake()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
        playerMove = GetComponent<PlayerMoveRigidbody>();
        rb = GetComponent<Rigidbody>();
        playerInputs = new PlayerInputs();
    }
    private void OnEnable()
    {
        move = playerInputs.Player.Move;
        playerInputs.Player.Enable();
    }
    private void OnDisable()
    {
        playerInputs.Player.Disable();
    }
    private void Update()
    {
        anim.SetFloat("Move", rb.velocity.x);
        //anim.SetFloat("Move", move.ReadValue<Vector2>().x);
        //anim.SetBool("InAttack", transform.GetComponent<PlayerMove>().InAttack);
        //anim.SetBool("IsGrounded", transform.GetComponent<PlayerMove>().IsGrounded());
        anim.SetBool("IsGrounded", transform.GetComponent<PlayerMoveRigidbody>().IsGrounded());
        //anim.SetBool("InCombo", transform.GetComponent<PlayerCombat>().InCombo);
    }
}
