using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
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
    }
    private void Update()
    {
        anim.SetFloat("Move", rb.velocity.x * playerMove.isPlayer2);
        anim.SetBool("IsGrounded", playerMove.IsGrounded());
        anim.SetBool("Crouched", playerMove.IsCrouched());
        anim.SetBool("InCombo", playerCombat.InCombo());
        anim.SetBool("InAttack", playerCombat.InAttack());
    }

    public void TriggerAttack(string AttackName)
    {
        anim.SetTrigger("LightAttack");
    }
    public void ContinueCombo()
    {
        anim.SetTrigger("Continue");
    }
}
