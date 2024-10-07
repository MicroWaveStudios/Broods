using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] PlayerMoveRigidbody playerMove;
    [SerializeField] PlayerAnimator playerAnimator;
    [SerializeField] PlayerStats playerStats;
    [SerializeField] NaraSkills NaraSkills;

    [SerializeField] Animator anim;

    bool _InAttack = false;
    bool InCombo = false;
    bool InParry = false;
    bool InMeiaLua = false;
    int actualNumber = 0;
    float timer = 0f;

    int ListaDeAtaqueAtual = -1;
    int OrdemCombo;

    bool atacouLeve;
    bool atacouMedio;
    bool atacouPesado;
    bool atacouBaixo;
    bool atacouAgachado;

    [SerializeField] public int ordem;


    [SerializeField] ParticleSystem[] rastrosAtaque;
    [SerializeField] GameObject attackGameObject;

    [System.Serializable]
    public struct AttackList
    {
        public string NomeDoAtaque;
        public int[] AtaqueRange; // Variavel que definirá onde o ataque acontecerá | 0 = Parte Inferior / 1 = Parte Superior / 2 = Corpo Todo \\
        public int[] Dano;
        public int[] OrdemCombo;
        public float[] FramesContinuar;
        public float[] VelocidadeDaAnimacao;
        public float[] SampleRate;
        public float[] MoveDamage;
    }
    [SerializeField] List<AttackList> _AttackList = new List<AttackList>();

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
        if (context.started)
        {
            Attack(0);
            atacouBaixo = true;
        }
    }


    // Botão Quadrado / Square (Playstation) - X (Xbox) - Y (Nintendo Switch) - J (Teclado/Teclado Direito) - F (Teclado Esquerdo)
    // Retornará o número 1
    public void LightAttack(InputAction.CallbackContext context)
    {
        StartCoroutine(ChangeActualNumber(1));
        if (context.started)
        {
            Attack(1);
            atacouLeve = true;
        }
    }


    // Botão Triângulo / Triangle (Playstation) - Y (Xbox) - X (Nintendo Switch) - I (Teclado/Teclado Direito) - T (Teclado Esquerdo)
    // Retornará o número 2
    public void MediumAttack(InputAction.CallbackContext context)
    {
        StartCoroutine(ChangeActualNumber(2));
        if (context.started)
        {
            Attack(2);
            atacouMedio = true;
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
            atacouPesado = true;
        }
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
    void Attack(int numberAttack)
    {
        if (!_InAttack && !InCombo && playerMove.GetIsGrounded() && !playerStats.GetDefendedOrSuffered() && ListaDeAtaqueAtual == -1)
        {
            if (playerMove.GetCrouched())
            {
                ListaDeAtaqueAtual = numberAttack + 4;
            }
            else
            {
                ListaDeAtaqueAtual = numberAttack;
            }
            if (ListaDeAtaqueAtual < _AttackList.Count)
            {
                _InAttack = true;
                playerAnimator.TriggerAction(_AttackList[ListaDeAtaqueAtual].NomeDoAtaque);

            }
            StartCoroutine(Combo());
        }
    }

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

    IEnumerator Combo()
    {
        if (ListaDeAtaqueAtual >= _AttackList.Count)
        {
            ResetCombo();
            yield break;
        }
        attackGameObject.GetComponent<Damage>().SetAttack(_AttackList[ListaDeAtaqueAtual].Dano[ordem], _AttackList[ListaDeAtaqueAtual].AtaqueRange[ordem], _AttackList[ListaDeAtaqueAtual].MoveDamage[ordem], true);
        playerMove.MoverAoAtacar(_AttackList[ListaDeAtaqueAtual].MoveDamage[ordem]);
        yield return new WaitForSeconds(0.01f);
        InCombo = true;
        yield return StartCoroutine(WaitForFrames(_AttackList[ListaDeAtaqueAtual].FramesContinuar[ordem], _AttackList[ListaDeAtaqueAtual].VelocidadeDaAnimacao[ordem], _AttackList[ListaDeAtaqueAtual].SampleRate[ordem]));
    }
    IEnumerator ContinuarCombo()
    {
        while (timer < 0.2f && !playerStats.GetDefendedOrSuffered() && ordem < _AttackList[ListaDeAtaqueAtual].OrdemCombo.Length)
        {
            timer += 1 * Time.deltaTime;
            if (actualNumber == _AttackList[ListaDeAtaqueAtual].OrdemCombo[ordem])
            {
                ordem++;
                timer = 0;
                StartCoroutine(Combo());
                yield break;
            }
            else
            {
                yield return null;
            }
        }
        ResetCombo();
        yield break;
    }

    public int FrameAtual;

    IEnumerator WaitForFrames(float frameCount, float velocidade, float sampleRate)
    { 
        yield return new WaitForSeconds(frameCount * velocidade / sampleRate);
        StartCoroutine(ContinuarCombo());
        yield break;
    }
    public void ResetCombo()
    {
        StartCoroutine(ResetCombo_());
    }

    IEnumerator ResetCombo_()
    {
        ListaDeAtaqueAtual = -1;
        ordem = 0;
        timer = 0f;
        actualNumber = -1;
        yield return new WaitForSeconds(0.3f);
        _InAttack = false;
        yield return new WaitForSeconds(0.2f);
        InCombo = false;
    }

    // Voids para POO
    public float GetAtualOrdemCombo()
    {
        return ordem;
    }
    public void SetInAttackInCombo()
    {
        InCombo = false;
        _InAttack = false;
    }
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
    public bool GetAtacouLeve()
    {
        return atacouLeve;
    }

    public bool GetAtacouMedio()
    {
        return atacouMedio;
    }

    public bool GetAtacouPesado()
    {
        return atacouPesado;
    }

    public bool GetAtacouBaixo()
    {
        return atacouBaixo;
    }

    public void SetInParry(bool value)
    {
        InParry = value;
    }
    public bool GetInParry()
    {
        return InParry;
    }

    public void SetInMeiaLua(bool value)
    {
        InMeiaLua = value;
    }
    public bool GetInMeiaLua()
    {
        return InMeiaLua;
    }
}