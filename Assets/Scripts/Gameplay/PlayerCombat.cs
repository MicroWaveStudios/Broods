using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    PlayerInputs playerInputs;
    PlayerMoveRigidbody playerMove;
    public bool InAttack = false;
    public bool InCombo = false;
    
    Animator anim;

    [SerializeField] int actualNumber = 0;
    float timer = 0f;
    [SerializeField] float delayAtaques;
    [SerializeField] int[] ordemCombo;
    int x;
    PlayerMoveRigidbody moveRigidbody;

    private void Awake()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
        playerMove = GetComponent<PlayerMoveRigidbody>();
        playerInputs = new PlayerInputs();
        moveRigidbody = GetComponent<PlayerMoveRigidbody>();
    }

    void ActiveBooleanInAttack(bool value)
    { 
        InAttack = value;
        playerMove.inAttack(value);
        anim.SetBool("InAttack", value);
    }



    public void Punch0(InputAction.CallbackContext context)
    {
        StartCoroutine(ChangeActualNumber(0));
        if (!InAttack && !InCombo)
            StartCoroutine(FirstCombo());
    }
    public void Punch1(InputAction.CallbackContext context)
    {
        StartCoroutine(ChangeActualNumber(1));
        if (!InAttack && !InCombo)
            StartCoroutine(SecondAttack());
    }
    public void Punch2(InputAction.CallbackContext context)
    {
        StartCoroutine(ChangeActualNumber(2));
        if (!InAttack && !InCombo)
            StartCoroutine(ThirdAttack());
    }



    IEnumerator ChangeActualNumber(int number)
    {
        actualNumber = number;
        yield return new WaitForSeconds(0.02f);
        actualNumber = -1;
        yield break;
    }

    IEnumerator FirstAttack()
    {
        anim.SetTrigger("Punch0");
        ActiveBooleanInAttack(true);
        yield return new WaitForSeconds(0.3f);
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
        yield return new WaitForSeconds(1.2f);
        ActiveBooleanInAttack(false);
    }

    IEnumerator FirstCombo()
    {
        yield return FirstAttack();
        InCombo = true;
        anim.SetBool("InCombo", InCombo);
        yield return Continued(0.2f, ordemCombo[x]);
        yield return new WaitForSeconds(0.5f);
        yield return Continued(0.2f, ordemCombo[x]);
        yield return new WaitForSeconds(1.3f);
        Debug.Log("fez");
        yield return ResetCombo();
        yield break;
    }

    IEnumerator Continued(float delay, int number)
    {
        while (timer < delay - 0.05f)
        {
            timer += 1 * Time.deltaTime;

            if (actualNumber == number)
            {
                moveRigidbody.MoverAoAtacar(1f);
                x++;
                timer = 0f;

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
        x = 0;
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
