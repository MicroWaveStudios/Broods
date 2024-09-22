using System.Collections;
<<<<<<< Updated upstream
=======
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering.LookDev;
>>>>>>> Stashed changes
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static PlayerCombat;

public class PlayerCombat : MonoBehaviour
{
    PlayerInputs playerInputs;
    PlayerMoveRigidbody playerMove;
    bool _InAttack = false;
    public bool _InCombo = false;

    Animator anim;

    [SerializeField] int actualNumber = 0;
    float timer = 0f;
<<<<<<< Updated upstream
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

=======

    [SerializeField] public int ordem;
    string actualAttackName;
    int[] actualDamage = new int[5];
    float[] actualAttackRange = new float[5];
    float[] actualTimeToContinue = new float[5];
    int[] actualOrderCombo = new int[5];



    [SerializeField] ParticleSystem[] rastrosAtaque;
    [SerializeField] GameObject attackGameObject;

    [System.Serializable]
    public struct AttackList
    {
        public string AttackName;
        public int[] Damage;
        public int[] ComboOrder;
        public float[] TimeToContinue;
        public int[] AttackRange; // Variavel que definirá onde o ataque acontecerá | 0 = Parte Inferior / 1 = Parte Superior / 2 = Corpo Todo \\
        public float[] MoveDamage;
    }
    [SerializeField] List<AttackList> _AttackList = new List<AttackList>();

>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
        if (!_InAttack && !_InCombo)
            StartCoroutine(FirstCombo());
=======
        if (context.started)
        { 
            Attack(0);
        }
>>>>>>> Stashed changes
    }
    public void Punch1(InputAction.CallbackContext context)
    {
        StartCoroutine(ChangeActualNumber(1));
<<<<<<< Updated upstream
        if (!_InAttack && !_InCombo)
            StartCoroutine(SecondAttack());
=======
        if (context.started)
        { 
            Attack(1);
        }
>>>>>>> Stashed changes
    }
    public void Punch2(InputAction.CallbackContext context)
    {
        StartCoroutine(ChangeActualNumber(2));
<<<<<<< Updated upstream
        if (!_InAttack && !_InCombo)
            StartCoroutine(ThirdAttack());
    }

=======
        if (context.started)
        { 
            Attack(2);
        }
    }

    // Botão O / Circle (Playstation) - B (Xbox) - A (Nintendo Switch) - L (Teclado/Teclado Direito) - H (Teclado Esquerdo)
    // Retornará o número 3
    public void HeavyAttack(InputAction.CallbackContext context)
    {
        StartCoroutine(ChangeActualNumber(3));
        if (context.started)
        { 
            Attack(3);
        }
    }
>>>>>>> Stashed changes


    public IEnumerator ChangeActualNumber(int number)
    {
        actualNumber = number;
        yield return new WaitForSeconds(0.02f);
        actualNumber = -1;
        yield break;
    }

<<<<<<< Updated upstream
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
=======

    void Attack(int numberAttack)
    {
        if (!InCombo && !_InAttack && playerMove.GetIsGrounded() && !playerStats.GetDefendedOrSuffered() && !NaraSkills.GetInMeiaLua())
        {
            _InAttack = true;
            actualAttackName = _AttackList[numberAttack].AttackName;
            playerAnimator.TriggerAction(actualAttackName);
            for (int i = 0; i < _AttackList[numberAttack].ComboOrder.Length; i++)
            {
                actualOrderCombo[i] = _AttackList[numberAttack].ComboOrder[i];
            }
            for (int i = 0; i < _AttackList[numberAttack].Damage.Length; i++)
            {
                actualDamage[i] = _AttackList[numberAttack].Damage[i];
            }
            for (int i = 0; i < _AttackList[numberAttack].AttackRange.Length; i++)
            {
                actualAttackRange[i] = _AttackList[numberAttack].AttackRange[i];
            }
            for (int i = 0; i < _AttackList[numberAttack].TimeToContinue.Length; i++)
            {
                actualTimeToContinue[i] = _AttackList[numberAttack].TimeToContinue[i];
            }
            StartCoroutine(Combo());
        }
    }

    // ================================================================= \\

    // Mêcanica de Combo antiga \\

    //IEnumerator FirstCombo()
    //{
    //    yield return _LightAttack();
    //    _InCombo = true;
    //    yield return Continued(0.2f, ordemCombo[ordem]);
    //    yield return new WaitForSeconds(0.5f);
    //    yield return Continued(0.2f, ordemCombo[ordem]);
    //    yield return new WaitForSeconds(1.05f);
    //    yield return ResetCombo();
    //    yield break;
    //}

    // ================================================================= //

    public int GetOrdem()
>>>>>>> Stashed changes
    {
        _InAttack = true;
        anim.SetTrigger("Punch2");
        yield return new WaitForSeconds(1.2f);
        _InAttack = false;
    }

<<<<<<< Updated upstream
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
=======
    IEnumerator Combo()
    {
        yield return Continue(actualTimeToContinue[ordem]);
    }

    IEnumerator Continue(float delay)
    {
        yield return new WaitForSeconds(0.02f);
        InCombo = true;
        yield return new WaitForSeconds(delay);
        while (timer < 0.2 && !playerStats.GetDefendedOrSuffered() && ordem < actualOrderCombo.Length)
        {
            timer += 1 * Time.deltaTime;
            if (actualNumber == actualOrderCombo[ordem])
            {
                ordem++;
                timer = 0;
                playerAnimator.ContinuedCombo();
                StartCoroutine(Combo());
                yield break;
            }
            else
                yield return null;
        }
        ResetCombo();
        yield break;
    }

    public void ResetCombo()
    {
        StartCoroutine(ResetCombo_());
    }

    IEnumerator ResetCombo_()
    {
        actualAttackName = null;
        ordem = 0;
        timer = 0f;
        actualNumber = -1;
        //if (!playerStats.GetDefendedOrSuffered())
        //    playerAnimator.NotContinued();
        yield return new WaitForSeconds(0.3f);
        _InAttack = false;
        yield return new WaitForSeconds(0.2f);
        InCombo = false;
        //StopAllCoroutines();
        yield break;
    }

    // Voids para POO
    public void SetActualDamage(float damage)
    {
        attackGameObject.GetComponent<Damage>().SetDamage(damage);
    }
    public void SetInAttackInCombo()
    {
        InCombo = false;
        _InAttack = false;
    }
    public float GetOrderCombo()
    {
        return ordem;
    }
    public bool GetInAttack()
>>>>>>> Stashed changes
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
