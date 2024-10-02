using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botao : MonoBehaviour
{
    [SerializeField] bool Confirmar;
    [SerializeField] int NumeroBoneco;
    [SerializeField] bool SelecionarBoneco;
    [SerializeField] bool Continuar;
    [SerializeField] bool Voltar;

    public void Interagir()
    {
        if (SelecionarBoneco)
        {
            
        }
        if (Confirmar)
        {
            GameObject.FindGameObjectWithTag("PersonagensManager").GetComponent<PersonagensManager>().Confirmou();
        }
        if (Voltar)
        {

        }
        if (Continuar) 
        { 
        
        }
    }
}
