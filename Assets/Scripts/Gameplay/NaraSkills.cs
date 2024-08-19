using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NaraSkills : MonoBehaviour
{
    int actualNumber = -1;
    [SerializeField] int[] ordemCombo;
    PlayerMoveRigidbody scrpRigidbody;
    PlayerStats scrpPlayerStats;
    GameObject outroPlayer;
    float timer;
    int ordem = 0;
    bool InMeiaLua = false;
    Vector3 posicaoRaycast;
    Vector3 posicaoRaycastPlayer2;
    
    

    [Header("Skill Laser")]
    [SerializeField] float custoLaser;
    [SerializeField] float danoLaser;
    ParticleSystem laser;

    private void Awake()
    {
        scrpRigidbody = GetComponent<PlayerMoveRigidbody>();       
        laser = GameObject.FindGameObjectWithTag("Laser").GetComponent<ParticleSystem>();
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

        posicaoRaycast = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);

        if (outroPlayer != null)
        {
            posicaoRaycastPlayer2 = new Vector3(outroPlayer.transform.position.x + 0.5f, outroPlayer.transform.position.y + 1f, outroPlayer.transform.position.z);

            //Debug.DrawLine(transform.position, outroPlayer.transform.position, Color.magenta);
        }

        
    }

    public void MeiaLuaStart(InputAction.CallbackContext context)
    {
        if (!InMeiaLua)
            StartCoroutine(MeiaLua());
            InMeiaLua = true;
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

    IEnumerator ChangeActualNumber(int number)
    {
        actualNumber = number;
        yield return new WaitForSeconds(0.02f);
        actualNumber = -1;
        yield break;
    }

    IEnumerator MeiaLua()
    {
        Debug.Log("Baixo");
        yield return Continued(0.2f, ordemCombo[ordem]);
        Debug.Log("Lado");
        yield return Continued(0.2f, ordemCombo[ordem]);
        Debug.Log("Hit");
        yield return FirstSkill();
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
                if (ordem != ordemCombo.Length)
                {
                    ordem++;
                }              
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

    

    IEnumerator FirstSkill()
    {
        if (scrpPlayerStats.energy < custoLaser)
        {
            Debug.Log("Sem Energia");
            yield break;
        }     

        yield return new WaitForSeconds(0.2f);

        RaycastHit hit;

        //Vector3 posicaoAtualPlayer2 = posicaoRaycastPlayer2;
        StartCoroutine(scrpPlayerStats.ResetScripts(false, 0.7f));
        laser.Play();
        Physics.Raycast(transform.position, outroPlayer.transform.position, out hit, 4f);
        scrpPlayerStats.UsouSkill(custoLaser);

        if (hit.collider != null)
        {
            GameObject Player2 = hit.collider.gameObject;

            if (Player2.GetComponent<PlayerStats>() != null && Player2.name != this.gameObject.name)
            {             
                Player2.GetComponent<PlayerStats>().SufferDamage(danoLaser);              
            }
                        
        }
        else
        {
            Debug.Log("Não foi");
        }


        yield return new WaitForSeconds(1f);
        yield break;
    }

    IEnumerator ResetCombo()
    {
        InMeiaLua = false;
        ordem = 0;
        timer = 0f;
        actualNumber = -1;
        StopAllCoroutines();
        yield break;
    }
}
