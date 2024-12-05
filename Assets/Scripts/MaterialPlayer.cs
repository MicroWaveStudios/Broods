using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MaterialPlayer : MonoBehaviour
{
    [System.Serializable]
    struct ListaMaterial
    {
        public string nome;
        public Material[] material;
    }

    [System.Serializable]
    struct ListaMaterialTarticos
    {
        public Material[] material;
    }

    [System.Serializable]
    struct ListaTarticosGame
    {
        public GameObject[] material;
    }

    [Header("Tárticos da Intro")]
    [SerializeField] GameObject[] TarticoIntro;

    [Header("Tárticos do Game")]
    [SerializeField] List<ListaTarticosGame> ListaTarticos = new List<ListaTarticosGame>();

    [SerializeField] List<ListaMaterialTarticos> VarianteTartico = new List<ListaMaterialTarticos>();
    [SerializeField] List<ListaMaterial> VarianteDaSkin = new List<ListaMaterial>();

    [SerializeField] GameObject[] PartesDoCorpo;
    GameObject botao;

    [SerializeField] int playerID;
    [SerializeField] int otherPlayerID;

    [SerializeField] int materialAtual = 0;
    [SerializeField] int materialOutroPlayer = -1;

    [SerializeField] bool selecaoPersonagem;
    [SerializeField] bool hasTartico;

    private void Start()
    {
        if (selecaoPersonagem)
        {
            PersonagensManager personagensManager = GameObject.FindGameObjectWithTag("PersonagemManager").GetComponent<PersonagensManager>();
            TrocarMaterial(1);
        }
    }

    public void TrocarMaterial(float direcao)
    {
        PersonagensManager personagensManager = GameObject.FindGameObjectWithTag("PersonagemManager").GetComponent<PersonagensManager>();
        materialOutroPlayer = botao.GetComponent<scriptBotao>().GetVariantePlayer(otherPlayerID);

        materialAtual += (int)direcao;

        StartCoroutine(Verificacao());
        if (materialAtual >= VarianteDaSkin.Count || materialAtual < 0)
        {
            goto pular;
        }
        if (personagensManager.GetPersonagemID(playerID) == personagensManager.GetPersonagemID(otherPlayerID) && personagensManager.GetConfirmouPersonagem(otherPlayerID))
        {
            while (materialAtual == materialOutroPlayer)
            {
                materialAtual += (int)direcao;
                StartCoroutine(Verificacao());
            }
        }
        pular:
        //if (personagensManager.GetPersonagemID(playerID) == personagensManager.GetPersonagemID(otherPlayerID) && personagensManager.GetConfirmouPersonagem(otherPlayerID))
        //{
        //    while (materialAtual == materialOutroPlayer)
        //    {
        //        materialAtual += (int)direcao;
        //    }
        //}
        botao.GetComponent<scriptBotao>().SetVariantePlayer(materialAtual, playerID);
        SetMaterialPersonagem(materialAtual);
        GameObject.FindGameObjectWithTag("PersonagemManager").GetComponent<PersonagensManager>().TrocarVarianteTxt(playerID, nome, frase);
        GameObject.FindGameObjectWithTag("PersonagemManager").GetComponent<PersonagensManager>().TrocarLocalBordaCor(playerID, materialAtual);
    }
    IEnumerator Verificacao()
    {
        if (materialAtual >= VarianteDaSkin.Count)
        {
            materialAtual = 0;
            goto pular;
        }
        if (materialAtual < 0)
        {
            materialAtual = VarianteDaSkin.Count - 1;
            goto pular;
        }
        pular:
        yield break;
    }
    public void SetMaterialPersonagem(int skin)
    {
        for (int i = 0; i < PartesDoCorpo.Length; i++)
        {
            if (PartesDoCorpo[i] != null && skin < VarianteDaSkin.Count && skin >= 0)
            {
                PartesDoCorpo[i].GetComponent<Renderer>().material = VarianteDaSkin[skin].material[i]; 
            }
        }

        if (hasTartico)
        {
            for (int i = 0; i < TarticoIntro.Length; i++)
            {
                TarticoIntro[i].GetComponent<Renderer>().materials = VarianteTartico[skin].material;
            }
            for (int i = 0; i < ListaTarticos.Count; i++)
            {
                for (int x = 0; x < ListaTarticos[i].material.Length; x++)
                {
                    ListaTarticos[i].material[x].GetComponent<Renderer>().material = VarianteTartico[skin].material[x];
                }
            }
        }
    }
    string nome;
    string frase;
    #region Void's Get/Set
    public void SetFrase(string value)
    { 
        frase = value;
    }
    public string GetFrase()
    { 
        return frase;
    }
    public void SetNome(string value)
    {
        nome = value;
    }
    public string GetNome()
    {
        return nome;
    }
    public void SetMaterialAtual(int novoMaterialAtual)
    {
        materialAtual = novoMaterialAtual;
    }
    public void SetMaterialOutroPlayer(int novoMaterialOutroPlayer)
    {
        materialOutroPlayer = novoMaterialOutroPlayer;
    }
    public void SetBotao(GameObject newBotao)
    {
        botao = newBotao;
    }
    public void SetPlayerID(int newPlayerID)
    { 
        playerID = newPlayerID;
        for (int i = 0; i < 2; i++)
        {
            if (playerID != i)
            {
                otherPlayerID = i;
            }
        }
    }
    public int GetMaterialAtual()
    { 
        return materialAtual;
    }
    public string GetVarianteAtual()
    {
        return VarianteDaSkin[materialAtual].nome;
    }
    #endregion
}
