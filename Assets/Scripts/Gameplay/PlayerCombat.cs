using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

enum Character
{
    Nara,
    Ximas
}

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] PlayerMoveRigidbody playerMove;
    [SerializeField] PlayerAnimator playerAnimator;
    [SerializeField] PlayerStats playerStats;

    [SerializeField] Animator anim;

    bool _InAttack = false;
    bool InCombo = false;
    int actualNumber = 0;
    float timer = 0f;


    [SerializeField] int[] ordemCombo;
    [SerializeField] public int ordem;


    [SerializeField] ParticleSystem[] rastrosAtaque;
    [SerializeField] GameObject attackGameObject;

    [System.Serializable]
    public struct AttackList
    {
        public string AttackName;
        public int AttackRange; // Variavel que definirá onde o ataque acontecerá | 0 = Parte Inferior / 1 = Parte Superior / 2 = Corpo Todo \\
        public int Damage;
        public bool MoveDamage;
    }
    [SerializeField] List<AttackList> _AttackList = new List<AttackList>();

    private void Awake()
    {
        //Debug.Log(_ComboList[0].OrdemCombo1);
        //Debug.Log(_ComboList[0].OrdemCombo2);
        //Debug.Log(_ComboList[0].OrdemCombo3);
        //Debug.Log(_ComboList[0].OrdemCombo4);
        //Debug.Log(_ComboList[0].OrdemCombo5);
        //anim = transform.GetChild(0).GetComponent<Animator>();
        //anim = GetComponent<Animator>();
        //playerMove = GetComponent<PlayerMoveRigidbody>();
        //playerAnimator = GetComponent<PlayerAnimator>();
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

    // Botão X / Cross (Playstation) - A (Xbox) - B (Nintendo Switch) - K (Teclado/Teclado Direito) - G (Teclado Esquerdo) 
    // Retornará o número 0
    public void LowAttack(InputAction.CallbackContext context)
    {
        StartCoroutine(ChangeActualNumber(0));
        if (!_InAttack && !InCombo && playerMove.GetIsGrounded() && context.started && !playerStats.GetDefendedOrSuffered())
            Attack(0);
        //Attack("LowAttack");
    }


    // Botão Quadrado / Square (Playstation) - X (Xbox) - Y (Nintendo Switch) - J (Teclado/Teclado Direito) - F (Teclado Esquerdo)
    // Retornará o número 1
    public void LightAttack(InputAction.CallbackContext context)
    {
        StartCoroutine(ChangeActualNumber(1));
        if (!_InAttack && !InCombo && playerMove.GetIsGrounded() && context.started && !playerStats.GetDefendedOrSuffered())
            Attack(1);
        //Attack("LightAttack");
    }


    // Botão Triângulo / Triangle (Playstation) - Y (Xbox) - X (Nintendo Switch) - I (Teclado/Teclado Direito) - T (Teclado Esquerdo)
    // Retornará o número 2
    public void MediumAttack(InputAction.CallbackContext context)
    {
        StartCoroutine(ChangeActualNumber(2));
        if (!_InAttack && !InCombo && playerMove.GetIsGrounded() && context.started && !playerStats.GetDefendedOrSuffered())
            Attack(2);
        //Attack("MediumAttack");
    }

    // Botão O / Circle (Playstation) - B (Xbox) - A (Nintendo Switch) - L (Teclado/Teclado Direito) - H (Teclado Esquerdo)
    // Retornará o número 3
    public void HeavyAttack(InputAction.CallbackContext context)
    {
        StartCoroutine(ChangeActualNumber(3));
        if (!_InAttack && !InCombo && playerMove.GetIsGrounded() && context.started && !playerStats.GetDefendedOrSuffered())
            Attack(3);
        //Attack("HeavyAttack");
    }

    // Esse void identificará qual botão de golpe está sendo retornado
    // 0 = SouthButton / 1 = WestButton / 2 = NorthButton / 3 = EastButton
    public IEnumerator ChangeActualNumber(int number)
    {
        actualNumber = number;
        yield return new WaitForSeconds(0.02f);
        actualNumber = -1;
        yield break;
    }

    // Void de Ataque
    //void Atack(string attackName)
    //{
    //    _InAttack = true;
    //    playerMove.SetCrouched(false);
    //    playerAnimator.TriggerAttack(attackName);
    //}

    void Attack(int numberAttack)
    {
        string _AttackName = _AttackList[numberAttack].AttackName;
        int _AttackRange = _AttackList[numberAttack].AttackRange;
        int _Damage = _AttackList[numberAttack].Damage;
        bool _MoveDamage = _AttackList[numberAttack].MoveDamage;

        attackGameObject.GetComponent<Damage>().SetAttack(_Damage, _AttackRange);
        _InAttack = true;

        playerAnimator.TriggerAction(_AttackName);
    }

    // Voids de Attack que foram utilizados para teste porém comentados por conta de repetição de código \\

    //void _LowAttack()
    //{
    //    _InAttack = true;
    //    playerAnimator.TriggerAttack("LowAttack");
    //}
    //void _LightAttack()
    //{
    //    _InAttack = true;
    //    playerAnimator.TriggerAttack("LightAttack");
    //}
    //void _MediumAttack()
    //{
    //    _InAttack = true;
    //    anim.SetTrigger("MediumAttack");
    //}
    //void _HeavyAttack()
    //{
    //    _InAttack = true;
    //    anim.SetTrigger("HeavyAttack");
    //}

    // ================================================================= //


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
    {
        return ordem;
    }

    public void ContinueCombo(int number)
    {
        InCombo = true;
        StartCoroutine(CoroutineContinue(0.1f, ordemCombo[number]));
    }
    IEnumerator CoroutineContinue(float delay, int number)
    {
        yield return new WaitForSeconds(0.1f);
        while (timer < delay)
        {
            timer += 1 * Time.deltaTime;
            if (actualNumber == number)
            {
                ordem++;
                timer = 0;

                playerAnimator.ConfirmedContinuedCombo();
                yield break;
            }
            else
                yield return null;
        }
        ResetCombo(0.2f);
        yield break;
    }

    public void ResetCombo(float delay)
    {
        StartCoroutine(ResetCombo_(delay));
    }

    IEnumerator ResetCombo_(float delay)
    {
        yield return new WaitForSeconds(delay);
        ordem = 0;
        timer = 0f;
        InCombo = false;
        actualNumber = -1;
        if (!playerStats.GetDefendedOrSuffered())
            playerAnimator.ConfirmedNotContinued();
        _InAttack = false;
        StopAllCoroutines();
    }


    // Voids para POO
    public bool GetInAttack()
    {
        return _InAttack;
    }
    public void SetInAttack(bool value)
    {
        _InAttack = value;
    }
    public void SetInCombo(bool value)
    {
        InCombo = value;
    }
    public bool GetInCombo()
    {
        return InCombo;
    }
}