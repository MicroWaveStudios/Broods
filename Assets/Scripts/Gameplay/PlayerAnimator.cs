using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    PlayerMoveRigidbody playerMoveRigidbody;
    PlayerCombat playerCombat;

    Rigidbody rb;
    Animator anim;
    private void Awake()
    {
        //anim = transform.GetChild(0).GetComponent<Animator>();
        anim = GetComponent<Animator>();
        playerMoveRigidbody = GetComponent<PlayerMoveRigidbody>();
        playerCombat = GetComponent<PlayerCombat>();
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

    public void TriggerAttack(string AttackName)
    {
        anim.SetTrigger(AttackName);
    }
    public void ContinueCombo(int number)
    {
        playerCombat.ContinueCombo(number);
        Debug.Log("Continue Combo Active");
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
