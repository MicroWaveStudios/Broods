using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrAnimationController : MonoBehaviour
{
    scrMovimentacaoCharacterController scrPlayer;
    scrGolpesBasicos scrGolpesB;
    Animator anim;
    void Start()
    {
        scrPlayer = transform.parent.GetComponent<scrMovimentacaoCharacterController>();
        scrGolpesB = transform.parent.GetComponent<scrGolpesBasicos>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        anim.SetBool("isGrounded", scrPlayer.isGrounded);
        anim.SetFloat("velocity_Y", scrPlayer.control.velocity.y);
        anim.SetBool("isMoving", scrPlayer.isMoving);
        anim.SetFloat("Horizontal", scrPlayer.Horizontal);
        anim.SetFloat("Vertical", scrPlayer.Vertical);

        anim.SetFloat("Punch_0", scrGolpesB.Punch_[0]);
        anim.SetFloat("Punch_1", scrGolpesB.Punch_[1]);
        anim.SetFloat("Punch_2", scrGolpesB.Punch_[2]);
    }
}
