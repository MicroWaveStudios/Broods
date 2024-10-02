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
        anim.SetBool("IsGrounded", playerMoveRigidbody.GetNoChao());
        anim.SetBool("Crouched", playerMoveRigidbody.GetEstaAgachado());
        anim.SetBool("InCombo", playerCombat.GetInCombo());
        anim.SetBool("InAttack", playerCombat.GetInAttack());
        anim.SetFloat("OrdemCombo", playerCombat.GetAtualOrdemCombo() + 1f);
    }

    public void TriggerAction(string AttackName)
    {
        anim.SetTrigger(AttackName);
    }
}
