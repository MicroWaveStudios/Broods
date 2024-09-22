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
    PlayerCombat playerCombat;
    Rigidbody rb;
    Animator anim;

    private void Awake()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
        playerMove = GetComponent<PlayerMoveRigidbody>();
        playerCombat = GetComponent<PlayerCombat>();
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
<<<<<<< Updated upstream
        anim.SetFloat("Move", rb.velocity.x * playerMove.isPlayer2);
        anim.SetBool("IsGrounded", playerMove.IsGrounded());
        anim.SetBool("Crouched", playerMove.IsCrouched());
        anim.SetBool("InCombo", playerCombat.InCombo());
        anim.SetBool("InAttack", playerCombat.InAttack());
=======
        anim.SetFloat("Move", rb.velocity.x * playerMoveRigidbody.isPlayer2);
        anim.SetBool("IsGrounded", playerMoveRigidbody.GetIsGrounded());
        anim.SetBool("Crouched", playerMoveRigidbody.GetCrouched());
        anim.SetBool("InCombo", playerCombat.GetInCombo());
        anim.SetBool("InAttack", playerCombat.GetInAttack());
        anim.SetFloat("OrderCombo", playerCombat.GetOrderCombo() + 1f);
>>>>>>> Stashed changes
    }
    public void Jump()
    {
<<<<<<< Updated upstream
        anim.SetTrigger("Jump");
=======
        anim.SetTrigger(AttackName);
    }
    public void ContinuedCombo()
    { 
        anim.SetTrigger("Continued");
    }
    public void NotContinued()
    {
        anim.SetTrigger("NotContinued");
>>>>>>> Stashed changes
    }
}
