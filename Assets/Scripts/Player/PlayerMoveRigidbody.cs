using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class PlayerMoveRigidbody : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] Collider colisaoCone;
    [SerializeField] float directionX;
    PlayerStats playerStats;
    PlayerCombat playerCombat;
    PlayerInput playerInput;
    [SerializeField] Sons scrSons;

    [SerializeField] GameObject objFumaca;
    VisualEffect vfxFumaca;
    FumacaChao scrpFumaca;

    [SerializeField] bool noChao;
    [SerializeField] bool crouched = false;

    string otherPlayerTag;

    public float isPlayer2 = 1;
    [SerializeField] int jumpCount;

    [Header("Forces")]
    [SerializeField] float MoveForce;
    [SerializeField] float JumpForce;

    GameObject gameManager;
    GameController gameController;

    bool trava = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerCombat = GetComponent<PlayerCombat>();
        playerStats = GetComponent<PlayerStats>();
        playerInput = GetComponent<PlayerInput>();
        scrSons = GetComponent<Sons>();

        vfxFumaca = objFumaca.GetComponent<VisualEffect>();
        scrpFumaca = objFumaca.GetComponent<FumacaChao>();
    }
    private void Update()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        if (gameManager != null)
        {
            gameController = gameManager.GetComponent<GameController>();
            if (this.gameObject.CompareTag("Player2"))
            {
                otherPlayerTag = "Player1";
                if (gameController.ChangedSide)
                    isPlayer2 = 1;
                else
                    isPlayer2 = -1;
            }
            else
            {
                otherPlayerTag = "Player2";
                if (gameController.ChangedSide)
                    isPlayer2 = -1;
                else
                    isPlayer2 = 1;
            }
        }


        if (directionX * isPlayer2 <= -1 && !playerCombat.GetInAttack() && !playerCombat.GetInCombo())
        {
            playerStats.SetDefendendo(true);
        }
        else
        {
            playerStats.SetDefendendo(false);
        }

        if (crouched)
        {
            if (!trava)
            {
                scrSons.TocarSom("Agachar");
                trava = true;
            }
        }
        else
        {
            trava = false;
        }
    }
    private void FixedUpdate()
    {
        if (jumpCount == 1 && !GetComponent<PlayerCombat>().GetInAttack() && !crouched && !playerStats.GetInAction())
        { 
            rb.velocity = new Vector3(directionX * MoveForce, rb.velocity.y, rb.velocity.z);
        }
        else if (jumpCount == 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);
        }
        else
        {
            rb.velocity = new Vector3(0f, rb.velocity.y, rb.velocity.z);
        }
        //WasPressedThisFrame() pesquisar em casa
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        directionX = context.ReadValue<Vector2>().x;
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (jumpCount == 1 && context.performed && !playerCombat.GetInAttack() && !playerCombat.GetInCombo())
        {
            crouched = true;
            SetCrouched(true, 0f);
        }
        else if (jumpCount == 0 || context.canceled)
        {
            SetCrouched(false, 0.2f);
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (jumpCount > 0 && !playerCombat.GetInAttack())
        {
            Jump(JumpForce, true);

            vfxFumaca.Play();
        }
    }
    public void SetInputActive(bool value)
    {
        switch (value)
        {
            case true:
                playerInput.DeactivateInput();
                break;
            case false:
                playerInput.ActivateInput();
                break;
        }
    }

    public void Jump(float force, bool value)
    {
        rb.AddForce(Vector3.zero);
        rb.AddForce(Vector3.up * force);
        if (value)
        {
            jumpCount--;
        }
    }

    public bool GetIsGrounded()
    {
        return noChao;
    }
    public bool GetCrouched()
    {
        return crouched;
    }
    public void SetCrouched(bool value, float timer)
    {
        crouched = value;
        //StartCoroutine(Agachar(value, timer));
    }

    //IEnumerator Agachar(bool value, float timer)
    //{ 
    //    yield return new WaitForSeconds(timer);
    //    crouched = value;
    //}

    public void MoverAoAtacar(float MoverAoAtacar_, bool isAtaqueMedioXimas)
    {
        rb.AddForce(Vector3.zero);

        if (isAtaqueMedioXimas == false)
        {          
            rb.AddForce(Vector3.right * MoverAoAtacar_ * 2 * isPlayer2);
        }
        else
        {
            Vector3 diagonal = new Vector3(1.5f, 1, 0);
            rb.AddForce(diagonal * MoverAoAtacar_ * 2 * isPlayer2);
        }
        
        SetOtherPlayerForce(MoverAoAtacar_);
    }

    GameObject otherPlayer;

    [SerializeField] float MoveForceSufferAttack;

    [SerializeField] bool moveUp = false;

    public void SetOtherPlayerForce(float force)
    {
        if (this.gameObject.CompareTag("Player2"))
        {
            otherPlayerTag = "Player1";
            
        }
        else
        {
            otherPlayerTag = "Player2";
            
        }
        //otherPlayer = GameObject.FindGameObjectWithTag(otherPlayerTag);
        //otherPlayer.GetComponent<PlayerMoveRigidbody>().SetForce(force);
    }
    public void SetMoveUp(bool value)
    {
        moveUp = value;
    }

    public void MoveUpOtherPlayer(int i)
    {
        bool value;
        if (i == 1)
            value = true;
        else
            value = false;

        otherPlayer = GameObject.FindGameObjectWithTag(otherPlayerTag);
        otherPlayer.GetComponent<PlayerMoveRigidbody>().SetMoveUp(value);
    }

    public void MoveUp(float moveUp)
    {
        Jump(moveUp, false);
    }

    public void MoverAoLevarDano(float moveDamage)
    {
        //StartCoroutine(moverAoLevarDano());
        rb.AddForce(Vector3.zero);
        rb.AddForce(Vector3.left * moveDamage * isPlayer2, ForceMode.Force);
    }

    //IEnumerator moverAoLevarDano()
    //{
    //    rb.AddForce(Vector3.zero);
    //    yield return new WaitForSeconds(0.02f);
    //    rb.AddForce(Vector3.zero);
    //    rb.AddForce(Vector3.left * MoveForceSufferAttack * isPlayer2);
    //    yield break;
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            noChao = true;
            jumpCount = 1;
            vfxFumaca.Play();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            noChao = false;
        }
    }

    public void TravarNoAr()
    {
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
    }

    public void GravidadeZero(bool tirarGravidade)
    {
        //rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
        rb.useGravity = false;

        if (tirarGravidade)
        {
            rb.useGravity = false;
        }
    }

    public void GravidadeNormal()
    {
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
    }

    public void ColisaoZero()
    {
        this.gameObject.GetComponent<Collider>().isTrigger = true;
        colisaoCone.isTrigger = true;
    }

    public void ColisaoNormal()
    {
        this.gameObject.GetComponent<Collider>().isTrigger = false;
        colisaoCone.isTrigger = false;
    }

    public float GetDirecaoX()
    {
        return directionX;
    }

    public bool GetNoChao()
    {
        return noChao;
    }
    public bool GetEstaAgachado()
    {
        return crouched;
    }
    /*public void MoveForce(bool Attacked)
    {
        float intensidade = 1f;
        if (this.gameObject.CompareTag("Player1"))
            intensidade = -1f;

        if (Attacked)
            rb.AddForce(Vector3.right * forcaEmpurrarAtacar * isPlayer2);
        else
            rb.AddForce(Vector3.left * forcaEmpurrar * isPlayer2 * intensidade);
    }*/

    //private void OnEnable()
    //{
    //    playerInputs.Player.Jump.started += OnJump;
    //    //playerInputs.Player.AttackButtonWest.started += Punch0;
    //    //playerInputs.Player.AttackButtonEast.started += Punch1;
    //    move = playerInputs.Player.Move;
    //    playerInputs.Player.Enable();
    //}
    //private void OnDisable()
    //{
    //    playerInputs.Player.Jump.started -= OnJump;
    //    //playerInputs.Player.AttackButtonWest.started -= Punch0;
    //    //playerInputs.Player.AttackButtonEast.started -= Punch1;
    //    playerInputs.Player.Disable();
    //}

    //----------------------------------------------------------------//

    //private void Punch0(InputAction.CallbackContext context)
    //{
    //    if (!InAttack)
    //        StartCoroutine(FirstAttack());
    //}
    //private void Punch1(InputAction.CallbackContext context)
    //{
    //    if (!InAttack)
    //        StartCoroutine(SecondAttack());
    //}

    //IEnumerator FirstAttack()
    //{
    //    anim.SetTrigger("Punch0");
    //    InAttack = true;
    //    yield return new WaitForSeconds(1f);
    //    InAttack = false;
    //}

    //IEnumerator SecondAttack()
    //{
    //    anim.SetTrigger("Punch1");
    //    InAttack = true;
    //    yield return new WaitForSeconds(2.14f);
    //    InAttack = false;
    //}
}
