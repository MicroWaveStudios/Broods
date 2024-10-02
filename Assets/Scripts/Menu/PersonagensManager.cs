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
    //[SerializeField] GameObject poseNara1;
    //[SerializeField] GameObject poseXimas1;
    //[SerializeField] GameObject confirm1;

    //[SerializeField] GameObject poseNara2;
    //[SerializeField] GameObject poseXimas2;
    //[SerializeField] GameObject confirm2;

    int[] indexPersonagem = new int[] { -1, -1};
    GameObject[] playerGameObject;
    int player = 0;

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
        if (value)
        {
            playerGameObject[0] = GameObject.FindGameObjectWithTag("ConnectManager").GetComponent<ConnectPlayerInMenu>().GetPlayer1();
            playerGameObject[1] = GameObject.FindGameObjectWithTag("ConnectManager").GetComponent<ConnectPlayerInMenu>().GetPlayer2();

            playerGameObject[0].GetComponent<NavegacaoSelecaoDePersonagem>().EnableMapActionPersonagem();
            playerGameObject[1].GetComponent<NavegacaoSelecaoDePersonagem>().EnableMapActionPersonagem();

            playerGameObject[0].GetComponent<NavegacaoSelecaoDePersonagem>().TrocarBotaoAtual(BotaoInicial);
            playerGameObject[1].GetComponent<NavegacaoSelecaoDePersonagem>().TrocarBotaoAtual(BotaoInicial);
        }
    }
    void OrientacaoDosBotoes()
    {
        for (int i = 0; i < BotoesDaCena.Length; i++)
        {
            PersonagemSprite personagemSprite = BotoesDaCena[i].GetComponent<PersonagemSprite>();
            for (int x = 0; x < BotoesDaCena.Length; x++)
            {
                if (BotoesDaCena[i] != BotoesDaCena[x])
                {
                    for (int y = 0; y < 3; y++)
                    {
                        personagemSprite.BotoesProximos(BotoesDaCena[x], y);
                    }
                }
            }
        }
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
    void SetarPersonagem()
    {
        int tag = player + 1;
        GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player" + tag);
        playerGameObject.GetComponent<ConnectPlayer>().SetarPersonagem(indexPersonagem[player]);
        Debug.Log("player " + tag + " selecionou o personagem " + indexPersonagem[player]);
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

