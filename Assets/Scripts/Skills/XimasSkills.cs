using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class XimasSkills : MonoBehaviour
{
    int actualNumber = -1;

    [SerializeField] GameObject attackGameObject;

    PlayerMoveRigidbody scrpRigidbody;
    PlayerStats scrpPlayerStats;

    PlayerAnimator playerAnimator;
    PlayerCombat playerCombat;

    GameObject outroPlayer;

    float timer;
    bool InMeiaLua = false;

    bool trava = true;

    [SerializeField] GameObject vfxEnergiaMate;
    [SerializeField] VisualEffect vfxCorte1;
    [SerializeField] VisualEffect vfxCorte2;

    [Header("Skill Dash com Corte")]
    [SerializeField] float custoDash;
    [SerializeField] float danoDash;
    [SerializeField] int[] ordemComboDash;
    Vector3 posicaoTpDash;
    int ordemDash = 0;
    //VisualEffect vfxLaser;

    [Header("Parry")]
    [SerializeField] int[] ordemComboParry;
    int ordemParry = 0;

    [Header("Sopro")]
    [SerializeField] float custoSopro;
    [SerializeField] float danoSopro;
    [SerializeField] int rangeSopro;
    [SerializeField] VisualEffect vfxExplosaoMate;


    private void Awake()
    {
        scrpRigidbody = GetComponent<PlayerMoveRigidbody>();
        playerCombat = GetComponent<PlayerCombat>();
        playerAnimator = GetComponent<PlayerAnimator>();
        //objTpDash = GameObject.FindGameObjectWithTag("TpDash").transform;
        //objLaser = transform.GetChild(1).gameObject;
        //vfxLaser = objLaser.GetComponent<VisualEffect>();
        scrpPlayerStats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        if (this.gameObject.CompareTag("Player1"))
        {
            outroPlayer = GameObject.FindGameObjectWithTag("Player2");
        }
        else
        {
            outroPlayer = GameObject.FindGameObjectWithTag("Player1");
        }

        if (outroPlayer != null)
        {
            posicaoTpDash = new Vector3(outroPlayer.transform.position.x + (2f * scrpRigidbody.isPlayer2), transform.position.y, transform.position.z);
        }
        

        if (scrpPlayerStats.GetEnergyFull() == true)
        {
            vfxEnergiaMate.SetActive(true);
        }
        else
        {
            vfxEnergiaMate.SetActive(false);
        }

        if (playerCombat.GetNomeAtaqueAtual() == "AtaqueMedio" && playerCombat.GetInCombo() == true)
        {
            if (playerCombat.GetOrdemCombo() < 3 && playerCombat.GetOrdemCombo() != 0)
            {
                scrpRigidbody.GravidadeZero(true);
                outroPlayer.GetComponent<PlayerMoveRigidbody>().TravarNoAr();
                scrpRigidbody.ColisaoZero();
            }

            if (playerCombat.GetOrdemCombo() == 4 && trava == false)
            {
                scrpRigidbody.GravidadeNormal();
                outroPlayer.GetComponent<PlayerMoveRigidbody>().GravidadeNormal();
                scrpRigidbody.ColisaoNormal();
                transform.position = new Vector3(outroPlayer.transform.position.x + (-1.5f * scrpRigidbody.isPlayer2), transform.position.y, transform.position.z);
                trava = true;
            }
        }

        else
        {
            scrpRigidbody.GravidadeNormal();
            outroPlayer.GetComponent<PlayerMoveRigidbody>().GravidadeNormal();
            scrpRigidbody.ColisaoNormal();
            trava = false;
        }

        if (playerCombat.GetNomeAtaqueAtual() == "AtaqueFraco" && playerCombat.GetInCombo() == true)
        {
            if (playerCombat.GetOrdemCombo() == 2)
            {
                scrpRigidbody.GravidadeZero(false);
                scrpRigidbody.ColisaoZero();
            }
            else
            {
                scrpRigidbody.ColisaoNormal();
                scrpRigidbody.GravidadeNormal();
            }
        }
        else
        {
            scrpRigidbody.ColisaoNormal();
            scrpRigidbody.GravidadeNormal();
        }

        //if (playerCombat.GetNomeAtaqueAtual() == "AtaqueForte" && playerCombat.GetInCombo() == true)
        //{
        //    if (playerCombat.GetOrdemCombo() == 3 && trava == false)
        //    {
        //        vfxCorte.Play();
        //        trava = true;
        //    }
        //}
        //else
        //{
        //    trava = false;
        //}

    }

    public void PlayVfxCorte()
    {
        vfxCorte1.Play();
    }

    public void PlayVfxCorte2()
    {
        vfxCorte2.Play();
    }

    public void MeiaLuaStart(InputAction.CallbackContext context)
    {
        if (!InMeiaLua)
        {
            StartCoroutine(ConfirmacaoSkill(0.2f));
        }        
    }
    public void MeiaLuaEsquerda(InputAction.CallbackContext context)
    {
        if (context.ReadValue<Vector2>().x * scrpRigidbody.isPlayer2 < 0)
            StartCoroutine(ChangeActualNumber(1));
    }

    public void MeiaLuaDireita(InputAction.CallbackContext context)
    {
        if (context.ReadValue<Vector2>().x * scrpRigidbody.isPlayer2 > 0)
            StartCoroutine(ChangeActualNumber(2));
    }
    public void MeiaLuaAtaqueLeve(InputAction.CallbackContext context)
    {
        StartCoroutine(ChangeActualNumber(3));
    }

    public void PoderMarretada(InputAction.CallbackContext context)
    {
        if (context.started)
            StartCoroutine(SkillSopro());
    }

    //public void MeiaLuaAtaqueForte(InputAction.CallbackContext context)
    //{
    //    StartCoroutine(ChangeActualNumber(4));
    //}

    //public void Tartico(InputAction.CallbackContext context)
    //{
    //    if (!InMeiaLua)
    //        StartCoroutine(SkillTartico());
    //        InMeiaLua = true;
    //}

    IEnumerator ChangeActualNumber(int number)
    {
        actualNumber = number;
        yield return new WaitForSeconds(0.02f);
        actualNumber = -1;
        yield break;
    }

    IEnumerator MeiaLuaDash()
    {
        yield return Continued(0.2f, ordemComboDash[ordemDash]);
        ordemDash = 1;
        yield return Continued(0.2f, ordemComboDash[ordemDash]);
        yield return SkillDash();
        yield return ResetCombo();
        yield break;
    }

    IEnumerator MeiaLuaParry()
    {
        yield return Continued(0.2f, ordemComboParry[ordemParry]);
        ordemParry = 1;
        yield return Continued(0.2f, ordemComboParry[ordemParry]);
        yield return SkillParry();
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
                timer = 0f;
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

    //void Meditar()
    //{
    //    scrpPlayerStats.AddEnergy(15);
    //}

    IEnumerator SkillDash()
    {
        
        if (scrpPlayerStats.energy < custoDash)
        {
            //Debug.Log("Sem Energia");
            yield break;
        }

        playerCombat.SetInAttack(true);

        transform.position = posicaoTpDash;

        playerAnimator.TriggerAction("AtaqueFraco");

        yield return new WaitForSeconds(0.2f);     


        StartCoroutine(scrpPlayerStats.ResetScripts(0.5f));

        //vfxLaser.Play();

        scrpPlayerStats.UsouSkill(custoDash);


        yield return new WaitForSeconds(1f);

        playerCombat.SetInAttack(false);
        yield return ResetCombo();
        yield break;
    }

    IEnumerator SkillParry()
    {

        //playerCombat.SetInAttack(true);
        playerCombat.SetInParry(true);

        playerAnimator.TriggerAction("Perry");      

        yield return new WaitForSeconds(1f);

        //playerCombat.SetInAttack(false);
        playerCombat.SetInParry(false);

        yield return ResetCombo();
        yield break;
    }

    IEnumerator SkillSopro()
    {
        if (scrpPlayerStats.energy < custoSopro)
        {
            //Debug.Log("Sem Energia");
            yield break;
        }

        playerCombat.SetInAttack(true);

        attackGameObject.GetComponent<Damage>().SetAttack(danoSopro, rangeSopro, 10f, 10f, 0f, false, 0f, "Sopro");

        scrpPlayerStats.UsouSkill(custoSopro);

        playerAnimator.TriggerAction("Sopro");

        yield return new WaitForSeconds(0.3f);

        vfxExplosaoMate.Play();

        yield return new WaitForSeconds(0.5f);

        playerCombat.SetInAttack(false);

        yield return ResetCombo();
        yield break;
    }



    IEnumerator ResetCombo()
    {
        ordemDash = 0;
        ordemParry = 0;     
        timer = 0f;
        InMeiaLua = false;
        playerCombat.SetInMeiaLua(false);
        actualNumber = -1;
        StopAllCoroutines();
        yield break;
    }

    IEnumerator ConfirmacaoSkill(float delay)
    {
        while (timer < delay - 0.05f)
        {
            timer += 1 * Time.deltaTime;

            if (actualNumber == ordemComboDash[ordemDash])
            {
                StartCoroutine(MeiaLuaDash());
                timer = 0f;
                InMeiaLua = true;
                playerCombat.SetInMeiaLua(true);
                yield break;
            }
            else
            {
                if (actualNumber == ordemComboParry[ordemParry])
                {
                    StartCoroutine(MeiaLuaParry());
                    timer = 0f;
                    InMeiaLua = true;
                    playerCombat.SetInMeiaLua(true);
                    yield break;
                }

                yield return null;
            }
        }

        //Meditar();
        yield return ResetCombo();
        yield break;
    }

    public bool GetInMeiaLua()
    {
        return InMeiaLua;
    }
}
