using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class PersonagensManager : MonoBehaviour
{
    #region Variáveis
    [SerializeField] Image[] spritePersonagens;
    [SerializeField] GameObject[] BotoesDaCena;
    [SerializeField] GameObject eventSystemManager;
    [SerializeField] GameObject BotaoInicial;

    [SerializeField] Transform[] posicaoPlayer;

    //[SerializeField] GameObject poseNara1;
    //[SerializeField] GameObject poseXimas1;
    //[SerializeField] GameObject confirm1;

    //[SerializeField] GameObject poseNara2;
    //[SerializeField] GameObject poseXimas2;
    //[SerializeField] GameObject confirm2;

    int[] indexPersonagem = new int[] { -1, -1};
    GameObject[] playerGameObject = new GameObject[2];
    int player = 0;

    bool naSelecaoDePersonagem;

    [System.Serializable]
    struct Lista
    {
        public GameObject[] posePersonagem;
        public GameObject confirmar;
    }
    [SerializeField] List<Lista> ListaPose = new List<Lista>();
    #endregion

    public void AlterarCenaPersonagens(bool value)
    {
        naSelecaoDePersonagem = value;
        playerGameObject[0] = GameObject.FindGameObjectWithTag("ConnectManager").GetComponent<ConnectPlayerInMenu>().GetPlayer1();
        playerGameObject[1] = GameObject.FindGameObjectWithTag("ConnectManager").GetComponent<ConnectPlayerInMenu>().GetPlayer2();
        playerGameObject[0].GetComponent<SelecaoPersonagem>().SetActiveBordaSelecao(value);
        playerGameObject[1].GetComponent<SelecaoPersonagem>().SetActiveBordaSelecao(value);
    }

    public GameObject GetBotaoInicial()
    {
        return BotaoInicial;
    }

    public void IniciarPainelPersonagem()
    {
        naSelecaoDePersonagem = true;
        playerGameObject[0] = GameObject.FindGameObjectWithTag("ConnectManager").GetComponent<ConnectPlayerInMenu>().GetPlayer1();
        playerGameObject[1] = GameObject.FindGameObjectWithTag("ConnectManager").GetComponent<ConnectPlayerInMenu>().GetPlayer2();
    }

    #region Confirmações
    public void Confirmou()
    {
        for (int i = 0; i < ListaPose[player].posePersonagem.Length; i++)
        {
            if (ListaPose[player].posePersonagem[i].activeInHierarchy)
            {
                indexPersonagem[player] = i;
                SetarPersonagem();
            }
        }
        ListaPose[player].confirmar.SetActive(false);
        if (player == 0)
        {
            player = 1;
        }
    }
    void SetarPersonagem()
    {
        int tag = player + 1;
        GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player" + tag);
        playerGameObject.GetComponent<ConnectPlayer>().SetarPersonagem(indexPersonagem[player]);
        //Debug.Log("player " + tag + " selecionou o personagem " + indexPersonagem[player]);
    }
    #endregion

    #region Selecionar Personagens
    public void SelecionouPersonagem(int value)
    {
        ResetarPosePersonagem(player);
        ListaPose[player].posePersonagem[value].SetActive(true);
        ListaPose[player].confirmar.SetActive(true);
        eventSystemManager.GetComponent<EventSystemManager>().SetCurrentSelectedButton(ListaPose[player].confirmar);
    }
    void ResetarPosePersonagem(int value)
    {
        for (int i = 0; i < ListaPose[value].posePersonagem.Length; i++)
        {
            ListaPose[value].posePersonagem[i].SetActive(false);
        }
    }
    #endregion

    public int GetIndexPlayer(int player)
    {
        return indexPersonagem[player];
    }
    public void SetIndexPlayer(int player, int value)
    {
        indexPersonagem[player] = value;
    }
    public bool GetNaSelecaoDePersonagem()
    {
        return naSelecaoDePersonagem;
    }
    public void SetNaSelecaoDePersonagem(bool value)
    { 
        naSelecaoDePersonagem = value;
    }
}

