using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrAnimationController : MonoBehaviour
{
    scrMovimentacaoCharacterController scrPlayer;
    PlayerController PlayerController;
    scrGolpesBasicos scrGolpesB;
    Animator anim;
    void Start()
    {
        scrPlayer = transform.parent.GetComponent<scrMovimentacaoCharacterController>();
        PlayerController = transform.parent.GetComponent<PlayerController>();
        scrGolpesB = transform.parent.GetComponent<scrGolpesBasicos>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //anim.SetBool("isGrounded", scrPlayer.isGrounded);
        //anim.SetFloat("velocity_Y", scrPlayer.control.velocity.y);
        //anim.SetBool("isMoving", scrPlayer.isMoving);
        //anim.SetFloat("Horizontal", scrPlayer.Horizontal);
        //anim.SetFloat("Vertical", scrPlayer.Vertical);

        anim.SetBool("isGrounded", PlayerController.isGrounded);
        anim.SetFloat("velocity_Y", PlayerController.control.velocity.y);
        anim.SetBool("isMoving", PlayerController.isMoving);
        anim.SetFloat("Horizontal", PlayerController.movement.x);
        anim.SetFloat("Vertical", PlayerController.movement.y);


        anim.SetFloat("Punch_0", scrGolpesB.Punch_[0]);
        anim.SetFloat("Punch_1", scrGolpesB.Punch_[1]);
        anim.SetFloat("Punch_2", scrGolpesB.Punch_[2]);
    }
}
