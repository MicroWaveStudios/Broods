using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    PlayerMoveRigidbody playerMoveRigidbody;
    PlayerCombat playerCombat;

    Rigidbody rb;
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMoveRigidbody = GetComponent<PlayerMoveRigidbody>();
        playerCombat = GetComponent<PlayerCombat>();
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        anim.SetFloat("Move", rb.velocity.x * playerMoveRigidbody.isPlayer2);
        anim.SetBool("NoChao", playerMoveRigidbody.GetNoChao());
        anim.SetBool("Crouched", playerMoveRigidbody.GetCrouched());
        anim.SetBool("InCombo", playerCombat.GetInCombo());
        anim.SetBool("InAttack", playerCombat.GetInAttack());
        anim.SetFloat("OrdemCombo", playerCombat.GetAtualOrdemCombo() + 1f);
    }

    public void Ganhou()
    {
        anim.SetTrigger("Ganhou");
    }

    public void Perdeu()
    {
        anim.SetBool("Derrota", true);
    }

    public void SetIntro(bool value)
    {
        anim.SetBool("Intro", value);
    }

    public void TriggerAction(string AttackName)
    {
        anim.SetTrigger(AttackName);
    }
}
