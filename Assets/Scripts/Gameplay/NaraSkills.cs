using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NaraSkills : MonoBehaviour
{
    int actualNumber = -1;
    

    PlayerMoveRigidbody scrpRigidbody;
    PlayerStats scrpPlayerStats;
    GameObject outroPlayer;
    float timer;
    
    bool InMeiaLua = false;
    Vector3 posicaoRaycast;
    Vector3 posicaoRaycastPlayer2;
    
    

    [Header("Skill Laser")]
    [SerializeField] float custoLaser;
    [SerializeField] float danoLaser;
    [SerializeField] GameObject objLaser;
    [SerializeField] int[] ordemComboLaser;
    LaserPosition laser;
    int ordemLaser = 0;

    [Header("Tarticos")]
    [SerializeField] GameObject[] tarticos;
    [SerializeField] float custoTartico;
    [SerializeField] int[] ordemComboTarticos;
    public int tarticosContagem = 0;
    int ordemTarticos = 0;

    private void Awake()
    {
        scrpRigidbody = GetComponent<PlayerMoveRigidbody>();
        //objLaser = GameObject.FindGameObjectWithTag("Laser");
        laser = objLaser.GetComponent<LaserPosition>();
        scrpPlayerStats = GetComponent<PlayerStats>();
        //tarticos = GameObject.FindGameObjectsWithTag("Tartico");
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
        }


        posicaoRaycast = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        
        Debug.DrawLine(posicaoRaycast, outroPlayer.transform.position, Color.red);
    }

    public void MeiaLuaStart(InputAction.CallbackContext context)
    {
        if (!InMeiaLua)
        {
            StartCoroutine(ConfirmacaoSkill(0.2f, 10));
            InMeiaLua = true;
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
    public void MeiaLuaAtaque(InputAction.CallbackContext context)
    {
        StartCoroutine(ChangeActualNumber(3));
    }

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
        ordemLaser++;
        yield return Continued(0.2f, ordemComboLaser[ordemLaser]);
        yield return SkillLaser();
        yield return ResetCombo();
        yield break;
    }

    IEnumerator MeiaLuaTarticos()
    {
        yield return Continued(0.2f, ordemComboTarticos[ordemTarticos]);
        ordemTarticos++;
        yield return Continued(0.2f, ordemComboTarticos[ordemTarticos]);
        yield return SkillTartico();
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

    void Meditar()
    {
        scrpPlayerStats.AddEnergy(15);
    }

    IEnumerator SkillLaser()
    {
        
        if (scrpPlayerStats.energy < custoLaser)
        {
            Debug.Log("Sem Energia");
            yield break;
        }

        Vector3 NovaPosicaoOutroPlayer = outroPlayer.transform.position;       

        yield return new WaitForSeconds(0.2f);     

        RaycastHit hit;

        StartCoroutine(scrpPlayerStats.ResetScripts(false, 0.5f));

        laser.StartLaser(this.gameObject);

        for (int i = 0; i < tarticos.Length; i++)
        {
            if (tarticos[i].activeSelf == true)
            {
                tarticos[i].transform.LookAt(posicaoRaycastPlayer2);
                tarticos[i].GetComponent<Tarticos>().Laser();             
            }           
        }

        Physics.Raycast(posicaoRaycast, NovaPosicaoOutroPlayer, out hit, 4f);

        scrpPlayerStats.UsouSkill(custoLaser);

        

        if (hit.collider != null)
        {
            

            GameObject Player2 = hit.collider.gameObject;

            if (Player2.GetComponent<PlayerStats>() != null)
            {
                Player2.GetComponent<PlayerStats>().SufferDamage(danoLaser);
                Player2.GetComponent<PlayerStats>().AddEnergy(danoLaser/2);
            }                    
        }
        else
        {
            Debug.Log("Não foi");
        }

        yield return new WaitForSeconds(1f);
        yield break;
    }

    IEnumerator SkillTartico()
    {
        if (scrpPlayerStats.energy < custoTartico)
        {
            Debug.Log("Sem Energia");
            yield break;
        }

        if (tarticosContagem >= 5)
        {
            Debug.Log("Transcendido");
            yield break;
        }

        tarticos[tarticosContagem].SetActive(true);

        tarticosContagem++;

        danoLaser += tarticosContagem;

        Debug.Log(danoLaser);

        scrpPlayerStats.UsouSkill(custoTartico);

        yield return new WaitForSeconds(1f);

        yield return ResetCombo();
        yield break;
    }



    IEnumerator ResetCombo()
    {
        ordemLaser = 0;
        ordemTarticos = 0;     
        timer = 0f;
        InMeiaLua = false;
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
                yield break;
            }
            else
            {
                if (actualNumber == ordemComboTarticos[ordemTarticos])
                {
                    StartCoroutine(MeiaLuaTarticos());
                    timer = 0f;
                    yield break;
                }

                yield return null;
            }
        }

        Meditar();
        yield return ResetCombo();
        yield break;
    }
}
