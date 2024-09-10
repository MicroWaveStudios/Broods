using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    PlayerInputs playerInputs;
    PlayerMoveRigidbody playerMove;
    bool _InAttack = false;
    public bool _InCombo = false;

    Animator anim;

    [SerializeField] int actualNumber = 0;
    float timer = 0f;
    [SerializeField] float delayAtaques;
    [SerializeField] int[] ordemCombo;
    public int ordem;
    PlayerMoveRigidbody moveRigidbody;
    [SerializeField] ParticleSystem[] rastrosAtaque;

    private void Awake()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
        playerMove = GetComponent<PlayerMoveRigidbody>();
        moveRigidbody = GetComponent<PlayerMoveRigidbody>();
    }

    private void Update()
    {

        if (_InAttack == true)
        {
            for (int i = 0; i < rastrosAtaque.Length; i++)
            {
                rastrosAtaque[i].Play();
            }
        }
        else
        {
            for (int i = 0; i < rastrosAtaque.Length; i++)
            {
                rastrosAtaque[i].Stop();
            }
        }
    }

    //void ActiveBooleanInAttack(bool value)
    //{
    //    _InAttack = value;
    //    anim.SetBool("InAttack", value);
    //}

    public void Punch0(InputAction.CallbackContext context)
    {
        StartCoroutine(ChangeActualNumber(0));
        if (!_InAttack && !_InCombo)
            StartCoroutine(FirstCombo());
    }
    public void Punch1(InputAction.CallbackContext context)
    {
        StartCoroutine(ChangeActualNumber(1));
        if (!_InAttack && !_InCombo)
            StartCoroutine(SecondAttack());
    }
    public void Punch2(InputAction.CallbackContext context)
    {
        StartCoroutine(ChangeActualNumber(2));
        if (!_InAttack && !_InCombo)
            StartCoroutine(ThirdAttack());
    }



    public IEnumerator ChangeActualNumber(int number)
    {
        actualNumber = number;
        yield return new WaitForSeconds(0.02f);
        actualNumber = -1;
        yield break;
    }

    IEnumerator FirstAttack()
    {
        anim.SetTrigger("Punch0");
        _InAttack = true;
        yield return new WaitForSeconds(0.3f);
    }
    IEnumerator SecondAttack()
    {
        _InAttack = true;
        anim.SetTrigger("Punch1");
        yield return new WaitForSeconds(1.28f);
        _InAttack = false;
    }

    IEnumerator ThirdAttack()
    {
        _InAttack = true;
        anim.SetTrigger("Punch2");
        yield return new WaitForSeconds(1.2f);
        _InAttack = false;
    }

    IEnumerator FirstCombo()
    {
        yield return FirstAttack();
        _InCombo = true;
        yield return Continued(0.2f, ordemCombo[ordem]);
        yield return new WaitForSeconds(0.5f);
        yield return Continued(0.2f, ordemCombo[ordem]);
        yield return new WaitForSeconds(1.05f);
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
                moveRigidbody.MoverAoAtacar();
                ordem++;
                timer = 0f;

                anim.SetTrigger("Continued");
                yield break;
            }
            else
            {
                yield return null;
            }
        }
        yield return ResetCombo();
        yield break;
    }

    public IEnumerator ResetCombo()
    {
        _InAttack = false;
        anim.SetTrigger("NotContinued");
        ordem = 0;
        timer = 0f;
        actualNumber = -1;
        _InCombo = false;
        StopAllCoroutines();
        yield break;
    }

    public void BreakAnimation()
    {
        anim.SetTrigger("NotContinued");
    }

    public bool InAttack()
    {
        return _InAttack;
    }
    public bool InCombo()
    {
        return _InCombo;
    }

    public void HitAnimation()
    {
        anim.SetTrigger("Hit");
    }
}
