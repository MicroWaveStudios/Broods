using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NaraSkills : MonoBehaviour
{
    [SerializeField] int actualNumber = 0;
    [SerializeField] int[] ordemCombo;
    PlayerMoveRigidbody scrpRigidbody;
    GameObject outroPlayer;
    float timer;
    int ordem = 0;
    bool InMeiaLua = false;
    [SerializeField] float danoLaser;
    Vector3 posicaoRaycast;
    Vector3 posicaoRaycastPlayer2;

    private void Awake()
    {
        scrpRigidbody = GetComponent<PlayerMoveRigidbody>();       
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

        posicaoRaycast = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);

        posicaoRaycastPlayer2 = new Vector3(outroPlayer.transform.position.x, outroPlayer.transform.position.y + 1.5f, outroPlayer.transform.position.z);

        
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
            StartCoroutine(ChangeActualNumber(3));
    }
    public void MeiaLuaAtaque(InputAction.CallbackContext context)
    {
        StartCoroutine(ChangeActualNumber(2));
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
        yield return Continued(0.5f, ordemCombo[ordem]);
        Debug.Log("Esquerda");
        yield return Continued(0.5f, ordemCombo[ordem]);
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
        RaycastHit hit;
        
        Physics.Raycast(transform.position, outroPlayer.transform.position, out hit, 20f);

        GameObject Player2 = hit.collider.gameObject;

        Debug.DrawLine(posicaoRaycast, posicaoRaycastPlayer2, Color.magenta);
        Debug.Log("Laser");
        if (Player2.GetComponent<PlayerStats>() != null)
        {
            Player2.GetComponent<PlayerStats>().SufferDamage(danoLaser);
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
