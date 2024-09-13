using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    PlayerMoveRigidbody playerMoveRigidbody;
    PlayerCombat playerCombat;
    PlayerStats playerStats;

    Rigidbody rb;
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMoveRigidbody = GetComponent<PlayerMoveRigidbody>();
        playerCombat = GetComponent<PlayerCombat>();
        playerStats = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        anim.SetFloat("Move", rb.velocity.x * playerMoveRigidbody.isPlayer2);
        anim.SetBool("IsGrounded", playerMoveRigidbody.GetIsGrounded());
        anim.SetBool("Crouched", playerMoveRigidbody.GetCrouched());
        anim.SetBool("InCombo", playerCombat.GetInCombo());
        anim.SetBool("InAttack", playerCombat.GetInAttack());
    }

    public void TriggerAction(string AttackName)
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
        playerCombat.ResetCombo(0f);
    }
    public void ConfirmedNotContinued()
    {
        anim.SetTrigger("NotContinued");
    }
    public void FinishedAttack()
    {
        anim.SetTrigger("NotContinued");
        playerCombat.ResetCombo(0f);
    }
    public void ResetAttack()
    {
        playerCombat.SetInAttack(false);
    }
}
