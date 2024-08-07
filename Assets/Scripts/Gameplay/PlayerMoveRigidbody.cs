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
    float directionX;
    [SerializeField] float moveForce;
    GameController gameController;
    PlayerStats playerStats;
    [SerializeField] float jumpForce;
    bool InJump = false;
    [SerializeField] Transform GroundCheck;
    [SerializeField] LayerMask ground;

    [SerializeField] bool InAttack = false;

    float isPlayer2 = 1;

    [SerializeField] float forcaEmpurrar;
    [SerializeField] float forcaEmpurrarAtacar;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        playerInputs = new PlayerInputs();
        player1 = GameObject.FindGameObjectWithTag("Player1");
        GameObject GameController = GameObject.FindGameObjectWithTag("GameController");
        gameController = GameController.GetComponent<GameController>();
        playerStats = GetComponent<PlayerStats>();
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
            isPlayer2 = -1;
        }
        else
        {
            isPlayer2 = 1;
        }

        if (gameController.TrocouLado == true)
        {
            if (directionX * isPlayer2 >= 1 )
            {
                playerStats.defendendo = true;
                Debug.Log("Defendendo");
            }
            else
            {
                playerStats.defendendo = false;
            }
        }
        else
        {
            if (directionX * isPlayer2 <= -1 )
            {
                playerStats.defendendo = true;
                Debug.Log("Defendendo");
            }
            else
            {
                playerStats.defendendo = false;
            }
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

    

    public void MoverAoLevarDano()
    {
        if (gameController.TrocouLado == false)
        {
            rb.AddForce(Vector3.right * forcaEmpurrar * isPlayer2 * -1);
        }
        else
        {
            rb.AddForce(Vector3.left * forcaEmpurrar * isPlayer2 * -1);
        }
    }

    public void MoverAoAtacar(float intensidade)
    {
        if (gameController.TrocouLado == false)
        {
            rb.AddForce(Vector3.right * forcaEmpurrarAtacar * isPlayer2 * intensidade);
        }
        else
        {
            rb.AddForce(Vector3.left * forcaEmpurrarAtacar * isPlayer2 * intensidade);
        }
    }

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
