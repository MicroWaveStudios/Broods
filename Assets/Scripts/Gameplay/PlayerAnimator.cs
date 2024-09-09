using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    PlayerMoveRigidbody playerMove;
    PlayerCombat playerCombat;

    Rigidbody rb;
    Animator anim;
    private void Awake()
    {
        //anim = transform.GetChild(0).GetComponent<Animator>();
        anim = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMoveRigidbody>();
        playerCombat = GetComponent<PlayerCombat>();
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        anim.SetFloat("Move", rb.velocity.x * playerMove.isPlayer2);
        anim.SetBool("IsGrounded", playerMove.IsGrounded());
        anim.SetBool("Crouched", playerMove.GetCrouched());
        anim.SetBool("InCombo", playerCombat.GetInCombo());
        anim.SetBool("InAttack", playerCombat.GetInAttack());
    }

    public void TriggerAttack(string AttackName)
    {
        anim.SetTrigger(AttackName);
    }
    public void ContinueCombo(int number)
    {
        playerCombat.ContinueCombo(number);
    }
    public void ConfirmedContinuedCombo()
    { 
        anim.SetTrigger("Continued");
    }
    public void NotContinued()
    {
        playerCombat.ResetCombo();
        anim.SetTrigger("NotContinued");
    }
    public void ConfirmedNotContinued()
    {
        if (!playerCombat.GetInCombo())
        { 
            anim.SetTrigger("NotContinued");
        }
    }
    public void FinishedAttack()
    {
        anim.SetTrigger("NotContinued");
        playerCombat.ResetCombo();
    }
}
