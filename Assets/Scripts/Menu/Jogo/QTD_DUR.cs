using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QTD_DUR : MonoBehaviour
{
    float qtd_rodadas;
    float dur_rodadas;

    private void Start()
    {
        
    }
    public void Trinta_sec()
    {
        dur_rodadas = 30f;
    }

    public void Sessenta_sec()
    {
        dur_rodadas = 60f;
    }

    public void Noventa_sec()
    {
        dur_rodadas = 90f;
    }

    public void Um_round()
    {
        qtd_rodadas = 1f;
    }

    public void Tres_rounds()
    {
        qtd_rodadas = 3f;
    }

    public void Cinco_round()
    {
        qtd_rodadas = 5f;
    }

    private void Update()
    {
        Debug.Log("Rodadas: " + qtd_rodadas + "- Duração: " + dur_rodadas);
    }
}
