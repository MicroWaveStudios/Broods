using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class PersonagensManager : MonoBehaviour
{
    #region Variáveis
    [SerializeField] Image[] spritePersonagens;
    //[SerializeField] GameObject poseNara1;
    //[SerializeField] GameObject poseXimas1;
    //[SerializeField] GameObject confirm1;

    //[SerializeField] GameObject poseNara2;
    //[SerializeField] GameObject poseXimas2;
    //[SerializeField] GameObject confirm2;

    int[] indexPersonagem = new int[] {-1, -1};
    bool p2PodeEscolher = false;

    [System.Serializable]
    struct Lista
    {
        public GameObject[] posePersonagem;
        public GameObject confirmar;
    }
    [SerializeField] List<Lista> ListaPose = new List<Lista>();
    #endregion

    #region Confirmações
    public void Confirmou()
    {
        int player;
        if (!p2PodeEscolher)
        {
            player = 0;
            p2PodeEscolher = true;
        }
        else
        {
            player = 1;
        }

        for (int i = 0; i < ListaPose[player].posePersonagem.Length; i++)
        {
            if (ListaPose[player].posePersonagem[i].activeInHierarchy)
            {
                indexPersonagem[player] = i;
            }
        }

        ListaPose[player].confirmar.SetActive(false);


        //if (!p2PodeEscolher)
        //{
        //    if (ListaPose[0].Personagem[0].gameObject.activeInHierarchy)
        //    {
        //        indexPersonagem1 = 0;
        //    }

        //    if (ListaPose[0].poseXimas.gameObject.activeInHierarchy)
        //    {
        //        indexPersonagem1 = 1;
        //    }

        //    p2PodeEscolher = true;
        //}

        //else
        //{
        //    if (ListaPose[1].poseNara.gameObject.activeInHierarchy)
        //    {
        //        indexPersonagem2 = 0;
        //    }

        //    if (ListaPose[1].poseXimas.gameObject.activeInHierarchy)
        //    {
        //        indexPersonagem2 = 1;
        //    }
        //}
    }
    #endregion

    #region Selecionar Personagens
    public void SelecionouPersonagem(int value)
    {
        int player;
        if (!p2PodeEscolher)
        {
            player = 0;
        }
        else
        {
            player = 1;
        }

        ResetarPosePersonagem(player);
        ListaPose[player].posePersonagem[value].SetActive(true);
        ListaPose[player].confirmar.SetActive(true); 
    }
    void ResetarPosePersonagem(int value)
    {
        for (int i = 0; i < ListaPose[value].posePersonagem.Length; i++)
        {
            ListaPose[value].posePersonagem[i].SetActive(false);
        }
    }
    //public void SelecionouNara()
    //{
    //    if (!p2PodeEscolher)
    //    {
    //        poseNara1.SetActive(true);
    //        poseXimas1.SetActive(false);
    //        confirm1.SetActive(true);
    //    }
    //    else
    //    {
    //        poseNara2.SetActive(true);
    //        poseXimas2.SetActive(false);
    //        confirm2.SetActive(true);
    //    }
    //}
    //public void SelecionouXimas()
    //{
    //    if (!p2PodeEscolher)
    //    {
    //        poseNara1.SetActive(false);
    //        poseXimas1.SetActive(true);
    //        confirm1.SetActive(true);
    //    }

    //    else
    //    {
    //        poseNara2.SetActive(false);
    //        poseXimas2.SetActive(true);
    //        confirm2.SetActive(true);
    //    }
    //}
    #endregion

    public int GetIndexPlayer(int player)
    {
        return indexPersonagem[player];
    }
    public void SetIndexPlayer(int player, int value)
    {
        indexPersonagem[player] = value;
    }
}

