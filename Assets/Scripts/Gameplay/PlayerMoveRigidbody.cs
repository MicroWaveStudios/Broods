using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveRigidbody : MonoBehaviour
{
    private PlayerInputs playerInputs;
    private InputAction move;

    Animator anim;

    GameObject player1;

    private Rigidbody rb;
    public float directionX;
    [SerializeField] float moveForce;
    GameController gameController;
    PlayerStats playerStats;
    [SerializeField] float jumpForce;
    public bool InJump = false;
    [SerializeField] Transform GroundCheck;
    [SerializeField] LayerMask ground;
    PlayerInput playerInput;

    [SerializeField] bool InAttack = false;

    public float isPlayer2 = 1;

    [SerializeField] float forcaEmpurrar;
    //[SerializeField] float forcaEmpurrarAtacar;

    GameObject gameManager;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        rb = GetComponent<Rigidbody>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        playerInputs = new PlayerInputs();
        player1 = GameObject.FindGameObjectWithTag("Player1");
        GameObject GameController = GameObject.FindGameObjectWithTag("GameController");
        gameController = GameController.GetComponent<GameController>();
        playerStats = GetComponent<PlayerStats>();
        playerInput = GetComponent<PlayerInput>();
    }
    //private void Start()
    //{
    //    if (player1 != null)
    //    {
    //        gameObject.tag = "Player2";
    //    }
    //    else
    //    {
    //        gameObject.tag = "Player1";
    //    }
    //}

    private void Update()
    {
        if (this.gameObject.CompareTag("Player2"))
        {
            if (gameController.TrocouLado)
            {
                isPlayer2 = 1;
            }
            else
            {
                isPlayer2 = -1;
            }
        }
        else
        {
            if (gameController.TrocouLado)
            {
                isPlayer2 = -1;
            }
            else
            {
                isPlayer2 = 1;
            }
        }

        if (directionX * isPlayer2 <= -1)
        {
            playerStats.defendendo = true;
            Debug.Log("Defendendo");
        }
        else
        {
            playerStats.defendendo = false;
        }
    }
    private void FixedUpdate()
    {
        if (IsGrounded() && !InAttack)
        { 
            rb.velocity = new Vector3(directionX * moveForce, rb.velocity.y, rb.velocity.z);
        }
        else
            rb.velocity = new Vector3(rb.velocity.x ,rb.velocity.y ,rb.velocity.z);
        //WasPressedThisFrame() pesquisar em casa
        if (InJump)
        {
            rb.AddForce(Vector3.up * jumpForce);
            InJump = false;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        directionX = context.ReadValue<Vector2>().x;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (IsGrounded() && !InAttack)
        {
            rb.AddForce(Vector3.up * jumpForce);
        }
    }

    public void OnTogglePause(InputAction.CallbackContext context)
    {
        if (!gameManager.GetComponent<GameManager>().GetBooleanIsPaused())
            gameManager.GetComponent<GameManager>().Pause(this.gameObject, true);
        else
            gameManager.GetComponent<GameManager>().Pause(this.gameObject, false);
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
        if (Physics.CheckSphere(GroundCheck.position, 0.1f, ground))
            return true;
        else
            return false;
    }

    public void inAttack(bool value)
    { 
        InAttack = value;
    }


    public void MoverAoAtacar()
    {
        rb.AddForce(Vector3.right * forcaEmpurrar * isPlayer2);
    }

    public void MoverAoLevarDano()
    {
        rb.AddForce(Vector3.left * forcaEmpurrar * isPlayer2);
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
