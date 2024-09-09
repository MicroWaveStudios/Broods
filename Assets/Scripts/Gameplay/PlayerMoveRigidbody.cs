using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveRigidbody : MonoBehaviour
{
    Rigidbody rb;
    float directionX;
    PlayerStats playerStats;
    PlayerCombat playerCombat;
    PlayerInput playerInput;

    bool _OnJump;
    bool crouched = false;

    public float isPlayer2 = 1;
    int jumpCount;

    [Header("Forces")]
    [SerializeField] float MoveForce;
    [SerializeField] float JumpForce;
    [SerializeField] float ForcaEmpurrar;

    GameObject gameManager;
    GameController gameController;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerCombat = GetComponent<PlayerCombat>();
        playerStats = GetComponent<PlayerStats>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        if (gameManager != null)
        {
            gameController = gameManager.GetComponent<GameController>();
            if (this.gameObject.CompareTag("Player2"))
            {
                if (gameController.ChangedSide)
                    isPlayer2 = 1;
                else
                    isPlayer2 = -1;
            }
            else
            {
                if (gameController.ChangedSide)
                    isPlayer2 = -1;
                else
                    isPlayer2 = 1;
            }
        }

        if (directionX * isPlayer2 <= -1)
        {
            playerStats.defendendo = true;
            //Debug.Log("Defendendo");
        }
        else
        {
            playerStats.defendendo = false;
        }
    }
    private void FixedUpdate()
    {
        if (jumpCount == 1 && !GetComponent<PlayerCombat>().GetInAttack() && !crouched)
            rb.velocity = new Vector3(directionX * MoveForce, rb.velocity.y, rb.velocity.z);
        else
            rb.velocity = new Vector3(rb.velocity.x ,rb.velocity.y ,rb.velocity.z);
        //WasPressedThisFrame() pesquisar em casa
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        directionX = context.ReadValue<Vector2>().x;
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (jumpCount == 1 && context.performed && !playerCombat.GetInAttack())
            crouched = true;
        else if (jumpCount == 0 || context.canceled)
            crouched = false;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (jumpCount > 0 && !playerCombat.GetInAttack())
        { 
            rb.AddForce(Vector3.up * JumpForce);
            jumpCount--;
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

    public bool IsGrounded()
    {
        return _OnJump;
    }
    public bool GetCrouched()
    { 
        return crouched;
    }
    public void SetCrouched(bool value)
    {
        crouched = value;
    }
    public void MoverAoAtacar()
    {
        rb.AddForce(Vector3.right * ForcaEmpurrar * isPlayer2);
    }

    public void MoverAoLevarDano()
    {
        rb.AddForce(Vector3.left * ForcaEmpurrar * isPlayer2);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        { 
            _OnJump = true;
            jumpCount = 1;
        }
    }
    private void OnCollisionExit(Collision collision) 
    {
        if (collision.gameObject.CompareTag("Ground"))
            _OnJump = false;
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
