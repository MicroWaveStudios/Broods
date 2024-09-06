using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    PlayerInputs playerInputs;
    PlayerMoveRigidbody playerMove;
    PlayerAnimator playerAnimator;
    bool _InAttack = false;
    public bool _InCombo = false;

    Animator anim;

    [SerializeField] int actualNumber = 0;
    float timer = 0f;
    [SerializeField] float delayAtaques;
    [SerializeField] int[] ordemCombo;
    public int ordem;
    [SerializeField] ParticleSystem[] rastrosAtaque;

    private void Awake()
    {
        //anim = transform.GetChild(0).GetComponent<Animator>();
        anim = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMoveRigidbody>();
        playerAnimator = GetComponent<PlayerAnimator>();
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

    // Botão X / Cross (Playstation) - A (Xbox) - K (Teclado/Teclado Direito) - G (Teclado Esquerdo) 
    // Retornará o número 0
    public void LowAttack(InputAction.CallbackContext context)
    {
        StartCoroutine(ChangeActualNumber(0));
        if (!_InAttack && !_InCombo)
            playerAnimator.TriggerAttack("LowAttack");
            //StartCoroutine(LowAttack());
    }


    // Botão Quadrado / Square (Playstation) - X (Xbox) - J (Teclado/Teclado Direito) - F (Teclado Esquerdo)
    // Retornará o número 1
    public void LightAttack(InputAction.CallbackContext context)
    {
        StartCoroutine(ChangeActualNumber(1));
        if (!_InAttack && !_InCombo)
            playerAnimator.TriggerAttack("LightAttack");
            //StartCoroutine(_LightAttack());
    }


    // Botão Triângulo / Triangle (Playstation) - Y (Xbox) - I (Teclado/Teclado Direito) - T (Teclado Esquerdo)
    // Retornará o número 2
    public void MediumAttack(InputAction.CallbackContext context)
    {
        StartCoroutine(ChangeActualNumber(2));
        if (!_InAttack && !_InCombo)
            playerAnimator.TriggerAttack("MediumAttack");
            //StartCoroutine(_MediumAttack());
    }

    // Botão O / Circle (Playstation) - B (Xbox) - L (Teclado/Teclado Direito) - H (Teclado Esquerdo)
    // Retornará o número 3
    public void HeavyAttack(InputAction.CallbackContext context)
    {
        StartCoroutine(ChangeActualNumber(3));
        if (!_InAttack && !_InCombo)
            playerAnimator.TriggerAttack("HeavyAttack");
            //StartCoroutine(_HeavyAttack());
    }


    public IEnumerator ChangeActualNumber(int number)
    {
        actualNumber = number;
        yield return new WaitForSeconds(0.02f);
        actualNumber = -1;
        yield break;
    }

    IEnumerator _LowAttack()
    {
        _InAttack = true;
        anim.SetTrigger("Punch0");
        yield return new WaitForSeconds(0.3f);
    }
    IEnumerator _LightAttack()
    {
        _InAttack = true;
        anim.SetTrigger("Punch1");
        yield return new WaitForSeconds(0.3f);
    }
    IEnumerator _MediumAttack()
    {
        _InAttack = true;
        anim.SetTrigger("Punch2");
        yield return new WaitForSeconds(1.28f);
        _InAttack = false;
    }
    IEnumerator _HeavyAttack()
    {
        _InAttack = true;
        anim.SetTrigger("Punch3");
        yield return new WaitForSeconds(1.2f);
        _InAttack = false;
    }

    IEnumerator FirstCombo()
    {
        yield return _LightAttack();
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
                playerMove.MoverAoAtacar();
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
