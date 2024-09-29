using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class PersonagensManager : MonoBehaviour
{
    #region Variáveis
    [SerializeField] Image[] spritePersonagens;
    [SerializeField] GameObject poseNara1;
    [SerializeField] GameObject poseXimas1;
    [SerializeField] GameObject confirm1;

    [SerializeField] GameObject poseNara2;
    [SerializeField] GameObject poseXimas2;
    [SerializeField] GameObject confirm2;

    public int indexPersonagem1;
    public int indexPersonagem2;
    public bool p2PodeEscolher = false;
    #endregion

    #region Confirmações
    public void Confirmou()
    {
        if (!p2PodeEscolher)
        {
            if (poseNara1.gameObject.activeInHierarchy)
            {
                indexPersonagem1 = 0;
            }

            if (poseXimas1.gameObject.activeInHierarchy)
            {
                indexPersonagem1 = 1;
            }

            p2PodeEscolher = true;
        }

        else
        {
            if (poseNara2.gameObject.activeInHierarchy)
            {
                indexPersonagem2 = 0;
            }

            if (poseXimas2.gameObject.activeInHierarchy)
            {
                indexPersonagem2 = 1;
            }
        }
    }
    #endregion

    #region Selecionar Personagens
    public void SelecionouNara()
    {
        if (!p2PodeEscolher)
        {
            poseNara1.SetActive(true);
            poseXimas1.SetActive(false);
            confirm1.SetActive(true);
        }
        else
        {
            poseNara2.SetActive(true);
            poseXimas2.SetActive(false);
            confirm2.SetActive(true);
        }
    }
    public void SelecionouXimas()
    {
        if (!p2PodeEscolher)
        {
            poseNara1.SetActive(false);
            poseXimas1.SetActive(true);
            confirm1.SetActive(true);
        }

        else
        {
            poseNara2.SetActive(false);
            poseXimas2.SetActive(true);
            confirm2.SetActive(true);
        }
    }
    #endregion
}

