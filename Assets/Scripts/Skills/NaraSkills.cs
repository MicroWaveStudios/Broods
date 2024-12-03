using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class NaraSkills : MonoBehaviour
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
    bool transcendido = false;
    Vector3 posicaoRaycast;
    Vector3 posicaoRaycastPlayer2;

    [System.Serializable]

    public struct ListaDeAtaquesEspeciais
    {
        public string nomeDoAtaque;
        public float danoDoAtaque;
        public float rangeDoAtaque;
        public float custoDoAtaque;
        public int[] ordemDaMeiaLua;
        public float framesContinuar;
        public float velocidadeDaAnimacao;
        public float sampleRate;
        public VisualEffect visualEffectDoAtaque;
    }

    [SerializeField] Material[] tatuagens;
    [SerializeField] Color[] corBrilho;
    int numVariacao;

    [Header("Skill Laser")]
    [SerializeField] float custoLaser;
    [SerializeField] float danoLaser;
    [SerializeField] int[] ordemComboLaser;
    GameObject objLaser;
    VisualEffect vfxLaser;
    int ordemLaser = 0;
    
    [Header("Tarticos")]
    [SerializeField] GameObject[] tarticos;
    [SerializeField] float custoTartico;
    [SerializeField] int[] ordemComboTarticos;
    public int tarticosContagem = 0;
    int ordemTarticos = 0;

    [Header("Explosao")]
    [SerializeField] float custoExplosao;
    [SerializeField] float danoExplosao;
    [SerializeField] int rangeExplosao;
    //[SerializeField] int[] ordemComboExplosao;
    [SerializeField] VisualEffect vfxExplosao;
    //int ordemExplosao = 0;

    private void Awake()
    {
        scrpRigidbody = GetComponent<PlayerMoveRigidbody>();
        playerCombat = GetComponent<PlayerCombat>();
        playerAnimator = GetComponent<PlayerAnimator>();
        objLaser = transform.GetChild(1).gameObject;
        vfxLaser = objLaser.GetComponent<VisualEffect>();
        scrpPlayerStats = GetComponent<PlayerStats>();
    }

    private void Start()
    {
        numVariacao = Pontos.variante[GetComponent<PlayerController>().GetPlayerID()];
        ApagarTatuagem();
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
            posicaoRaycastPlayer2 = new Vector3(outroPlayer.transform.position.x + 0.5f, outroPlayer.transform.position.y + 1.3f, outroPlayer.transform.position.z);

            Debug.DrawLine(posicaoRaycast, outroPlayer.transform.position, Color.red);
        }

        if (tarticosContagem >= 5 && transcendido == false)
        {
            transcendido = true;
            BrilharTatuagem();

            scrpPlayerStats.SomarPontos(800);
        }

        posicaoRaycast = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
    }

    public void MeiaLuaStart(InputAction.CallbackContext context)
    {
        if (!playerCombat.GetInMeiaLua() && context.started)
        {
            StartCoroutine(ConfirmacaoSkill(0.2f, 10));
        }
    }
    public void MeiaLuaEsquerda(InputAction.CallbackContext context)
    {
        if (context.ReadValue<Vector2>().x * scrpRigidbody.isPlayer2 < 0 && context.started)
            StartCoroutine(ChangeActualNumber(1));
    }

    public void MeiaLuaDireita(InputAction.CallbackContext context)
    {
        if (context.ReadValue<Vector2>().x * scrpRigidbody.isPlayer2 > 0 && context.started)
            StartCoroutine(ChangeActualNumber(2));
    }
    public void MeiaLuaAtaqueLeve(InputAction.CallbackContext context)
    {
        if(context.started)
            StartCoroutine(ChangeActualNumber(3));
    }

    public void PoderExplosao(InputAction.CallbackContext context)
    {
        if (context.started)
            StartCoroutine(SkillExplosao());
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

    IEnumerator MeiaLuaLaser()
    {
        yield return Continued(0.2f, ordemComboLaser[ordemLaser]);
        ordemLaser = 1;
        yield return Continued(0.2f, ordemComboLaser[ordemLaser]);
        yield return SkillLaser();
        yield return ResetCombo();
        yield break;
    }

    IEnumerator MeiaLuaTarticos()
    {
        yield return Continued(0.2f, ordemComboTarticos[ordemTarticos]);
        ordemTarticos = 1;
        yield return Continued(0.2f, ordemComboTarticos[ordemTarticos]);
        yield return SkillTartico();
        yield return ResetCombo();
        yield break;
    }

    //IEnumerator MeiaLuaExplosao()
    //{
    //    yield return Continued(0.2f, ordemComboExplosao[ordemExplosao]);
    //    ordemTarticos++;
    //    yield return Continued(0.2f, ordemComboExplosao[ordemExplosao]);
    //    yield return SkillExplosao();
    //    yield return ResetCombo();
    //    yield break;
    //}

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

    IEnumerator SkillLaser()
    {
        
        if (scrpPlayerStats.energy < custoLaser)
        {
            //Debug.Log("Sem Energia");
            yield break;
        }

        attackGameObject.GetComponent<Damage>().SetAttack(20f, 3, 0f, 0f, 0f, false, 0f, "Laser");

        Vector3 NovaPosicaoOutroPlayer = outroPlayer.transform.position;

        playerCombat.SetInAttack(true);

        playerAnimator.TriggerAction("Laser");

        BrilharTatuagem();

        scrpPlayerStats.SomarPontos(150);

        yield return new WaitForSeconds(0f);     

        RaycastHit hit;

        StartCoroutine(scrpPlayerStats.ResetScripts(0f));

        vfxLaser.Play();

        //for (int i = 0; i < tarticos.Length; i++)
        //{
        //    if (tarticos[i].activeSelf == true)
        //    {
        //        tarticos[i].transform.LookAt(posicaoRaycastPlayer2);
        //        tarticos[i].GetComponent<Tarticos>().Laser();             
        //    }           
        //}

        Physics.Raycast(posicaoRaycast, NovaPosicaoOutroPlayer, out hit, 7f);

        

        scrpPlayerStats.UsouSkill(custoLaser);

        if (hit.collider != null)
        {

            GameObject Player2 = hit.collider.gameObject;

            if (Player2.GetComponent<PlayerStats>() != null)
            {
                Player2.GetComponent<PlayerStats>().SufferDamage(danoLaser, 2, 20f, 20f, 0f, this.gameObject);
                Player2.GetComponent<PlayerStats>().AddEnergy(danoLaser / 2);
            }                    
        }
        else
        {
            //Debug.Log("N�o foi");
        }

        yield return new WaitForSeconds(1f);

        playerCombat.SetInAttack(false);
        yield return ResetCombo();
        yield break;
    }

    IEnumerator SkillTartico()
    {
        if (scrpPlayerStats.energy < custoTartico)
        {
            //Debug.Log("Sem Energia");
            yield break;
        }

        if (tarticosContagem >= 5)
        {
            //Debug.Log("Transcendido");
            yield break;
        }


        tarticos[tarticosContagem].SetActive(true);

        playerCombat.SetInAttack(true);

        tarticosContagem++;

        scrpPlayerStats.SomarDamageMultiplier(0.3f * tarticosContagem);

        //danoLaser += tarticosContagem;

        //Debug.Log(danoLaser);

        scrpPlayerStats.UsouSkill(custoTartico);

        playerAnimator.TriggerAction("Tartico");

        BrilharTatuagem();

        scrpPlayerStats.SomarPontos(150);

        yield return new WaitForSeconds(0.5f);
        playerCombat.SetInAttack(false);
        yield return ResetCombo();
        yield break;
    }

    IEnumerator SkillExplosao()
    {
        if (scrpPlayerStats.energy < custoExplosao)
        {
            //Debug.Log("Sem Energia");
            yield break;
        }

        playerCombat.SetInAttack(true);

        attackGameObject.GetComponent<Damage>().SetAttack(danoExplosao, rangeExplosao, 0f, 220f, 0f, false, 0f, "Laser");

        scrpPlayerStats.UsouSkill(custoExplosao);

        playerAnimator.TriggerAction("Kaboom");

        BrilharTatuagem();

        scrpPlayerStats.SomarPontos(150);

        yield return new WaitForSeconds(0.3f);

        vfxExplosao.Play();

        yield return new WaitForSeconds(0.5f);

        playerCombat.SetInAttack(false);
        yield return ResetCombo();
        yield break;
    }



    IEnumerator ResetCombo()
    {
        yield return new WaitForSeconds(0.1f);
        ordemLaser = 0;
        ordemTarticos = 0;     
        timer = 0f;
        ApagarTatuagem();
        playerCombat.SetInMeiaLua(false);
        playerCombat.SetInAttack(false);
        actualNumber = -1;
        StopAllCoroutines();
        yield break;
    }

    IEnumerator ConfirmacaoSkill(float delay, int number)
    {
        while (timer < delay - 0.05f)
        {
            timer += 1 * Time.deltaTime;

            if (actualNumber == ordemComboLaser[ordemLaser])
            {
                StartCoroutine(MeiaLuaLaser());
                timer = 0f;
                playerCombat.SetInMeiaLua(true);
                yield break;
            }
            else
            {
                if (actualNumber == ordemComboTarticos[ordemTarticos])
                {
                    StartCoroutine(MeiaLuaTarticos());
                    timer = 0f;
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

    public void BrilharTatuagem()
    {
        tatuagens[numVariacao].SetColor("_Color1", corBrilho[numVariacao] * 19f);
    }

    public void ApagarTatuagem()
    {
        if (transcendido == false)
        {
            tatuagens[numVariacao].SetColor("_Color1", Color.white * 1f);
        }
        
    }
}

