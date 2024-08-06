using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class scrGolpesBasicos : MonoBehaviour
{
    [SerializeField] float damage;
    public float[] Punch_; 
    public float timeDelay;
    Coroutine GolpeAtual;
    
    void Update()
    {
        Punch_[0] = Input.GetAxisRaw("Fire1");
        Punch_[1] = Input.GetAxisRaw("Fire2");
        Punch_[2] = Input.GetAxisRaw("Fire3");
    }

    void ContinuouGolpe(int i, float damage)
    {
        
    }

    /*public async void Golpe(int i)
    {
        Debug.Log("Golpe " + i.ToString());
        await Timer(i);
    }

    async Task Timer(int i)
    {
        while (time > 1f)
        {
            if (Punch_[i] != 0f)
            {
                Debug.Log("Golpe " + Punch_[i + 1]);
            }
        }
        time = 0f;
    }*/

}
