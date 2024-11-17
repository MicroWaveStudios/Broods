using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialPlayer : MonoBehaviour
{
    [System.Serializable]
    struct ListaMaterial
    {
        public string nome;
        public Material[] material;
    }

    [SerializeField] List<ListaMaterial> VarianteDaSkin = new List<ListaMaterial>();

    [SerializeField] GameObject[] PartesDoCorpo;
    GameObject botao;

    [SerializeField] int playerID;
    [SerializeField] int otherPlayerID;

    [SerializeField] int materialAtual = 0;
    [SerializeField] int materialOutroPlayer = -1;

    [SerializeField] bool selecaoPersonagem;

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

        if (materialAtual >= VarianteDaSkin.Count)
        {
            materialAtual = 0;
        }
        else
        {
            if (materialAtual < 0)
            {
                materialAtual = VarianteDaSkin.Count - 1;
            }
        }
        if (personagensManager.GetPersonagemID(playerID) == personagensManager.GetPersonagemID(otherPlayerID) && personagensManager.GetConfirmouPersonagem(otherPlayerID))
        {
            while (materialAtual == materialOutroPlayer)
            {
                materialAtual += (int)direcao;
            }
        }
        botao.GetComponent<scriptBotao>().SetVariantePlayer(materialAtual, playerID);
        SetMaterialPersonagem(materialAtual);
        Debug.Log(materialAtual);
        GameObject.FindGameObjectWithTag("PersonagemManager").GetComponent<PersonagensManager>().TrocarVarianteTxt(playerID, VarianteDaSkin[materialAtual].nome);
    }
    public void SetMaterialPersonagem(int skin)
    {
        for (int i = 0; i < PartesDoCorpo.Length; i++)
        {
            if (PartesDoCorpo[i] != null && VarianteDaSkin[skin].material[i] != null)
            { 
                PartesDoCorpo[i].GetComponent<Renderer>().material = VarianteDaSkin[skin].material[i];
            }
        }
    }

    #region Void's Get/Set
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
