using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    private PlayerInputs playerInputs;
    PlayerMoveRigidbody playerMove;
    public bool InAttack = false;
    public bool InCombo = false;
    bool continued;

    [SerializeField] InputAction[] action;
    
    Animator anim;

    [SerializeField] int actualNumber = -1;
    float timer = 0f;
    [SerializeField] float delayAtaques;

    private void Awake()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
        playerMove = GetComponent<PlayerMoveRigidbody>();
        playerInputs = new PlayerInputs();
        SyncActions();
    }

    void SyncActions()
    {
        action[0] = playerInputs.Player.AttackButtonWest;
        action[1] = playerInputs.Player.AttackButtonEast;
        action[2] = playerInputs.Player.AttackButtonNorth;
    }

    private void OnEnable()
    {
        playerInputs.Player.AttackButtonWest.started += Punch0;
        playerInputs.Player.AttackButtonEast.started += Punch1;
        playerInputs.Player.AttackButtonNorth.started += Punch2;
        playerInputs.Player.Enable();
    }
    private void OnDisable()
    {
        playerInputs.Player.AttackButtonWest.started -= Punch0;
        playerInputs.Player.AttackButtonEast.started -= Punch1;
        playerInputs.Player.AttackButtonNorth.started -= Punch2;
        playerInputs.Player.Disable();
    }

    void ActiveBooleanInAttack(bool value)
    { 
        InAttack = value;
        playerMove.inAttack(value);
        anim.SetBool("InAttack", value);
    }



    public void Punch0(InputAction.CallbackContext context)
    {
        if (!InAttack && !InCombo)
            StartCoroutine(FirstCombo());
    }
    public void Punch1(InputAction.CallbackContext context)
    {
        if (!InAttack && !InCombo)
            StartCoroutine(SecondAttack());
    }
    public void Punch2(InputAction.CallbackContext context)
    {
        if (!InAttack && !InCombo)
            StartCoroutine(ThirdAttack());
    }



    IEnumerator FirstAttack()
    {
        anim.SetTrigger("Punch0");
        yield return new WaitForSeconds(0.01f);
        ActiveBooleanInAttack(true);
        yield return new WaitForSeconds(0.5f);
    }
    IEnumerator SecondAttack()
    {
        ActiveBooleanInAttack(true);
        anim.SetTrigger("Punch1");
        yield return new WaitForSeconds(1.28f);
        ActiveBooleanInAttack(false);
    }

    IEnumerator ThirdAttack()
    {
        ActiveBooleanInAttack(true);
        anim.SetTrigger("Punch2");
        yield return new WaitForSeconds(1f);
        ActiveBooleanInAttack(false);
    }

    IEnumerator FirstCombo()
    {
        yield return FirstAttack();
        InCombo = true;
        anim.SetBool("InCombo", InCombo);
        yield return Continued(0.2f, 2);
        yield return new WaitForSeconds(1f);
        yield return Continued(0.2f, 1);
        yield return new WaitForSeconds(1.3f);

        yield return ResetCombo();
        yield break;
    }

    IEnumerator Continued(float delay, int number)
    {
        actualNumber = number;
        while (timer < delay - 0.05f)
        {
            timer += 1 * Time.deltaTime;
            if (action[actualNumber].triggered)
            {
                timer = 0f;
                Debug.Log("Continuou");
                anim.SetTrigger("Continued");
                yield break;
            }
            else
            {
                yield return null;
            }
        }
        Debug.Log("acabou");

        yield return ResetCombo();
        Debug.Log("resetou");
        yield break;
    }

    IEnumerator ResetCombo()
    {
        anim.SetTrigger("NotContinued");
        timer = 0f;
        actualNumber = -1;
        InCombo = false;
        anim.SetBool("InCombo", InCombo);
        ActiveBooleanInAttack(false);
        StopAllCoroutines();
        yield break;
    }
}
