using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NaraSkills : MonoBehaviour
{
    [SerializeField] int actualNumber = 0;
    [SerializeField] int[] ordemCombo;
    PlayerMoveRigidbody scrpRigidbody;
    float timer;
    int x = 0;

    private void Awake()
    {
        scrpRigidbody = GetComponent<PlayerMoveRigidbody>();
    }

    public void MeiaLuaStart(InputAction.CallbackContext context)
    {   
        StartCoroutine(MeiaLua());
    }
    public void MeiaLuaFinal(InputAction.CallbackContext context)
    {
        if (context.ReadValue<Vector2>().x * scrpRigidbody.isPlayer2 < 0)
            StartCoroutine(ChangeActualNumber(1));
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
        yield return Continued(0.5f, ordemCombo[x]);
        Debug.Log("Esquerda");
        yield return Continued(0.5f, ordemCombo[x]);
        Debug.Log("Hit");
        yield return new WaitForSeconds(1f);
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
                x++;
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
        Debug.Log("LASER LASER LASER");
        yield break;
    }

    IEnumerator ResetCombo()
    {
        x = 0;
        timer = 0f;
        actualNumber = -1;
        StopAllCoroutines();
        yield break;
    }
}
