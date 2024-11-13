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
    [SerializeField] int materialAtual = -1;
    [SerializeField] int materialOutroPlayer = -1;

    private void Start()
    {
        TrocarMaterial(1);
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
    public void TrocarMaterial(float direcao)
    {
        materialAtual += (int)direcao;
        VerificacaoVarianteOutroPlayer();

        for (int i = 0; i < 1; i++)
        {
            if (playerID != i)
            {
                otherPlayerID = i;
            }
        }

        PersonagensManager personagemManager = GameObject.FindGameObjectWithTag("PersonagemManager").GetComponent<PersonagensManager>();

        if (materialAtual == materialOutroPlayer && personagemManager.GetPersonagemID(playerID) == personagemManager.GetPersonagemID(otherPlayerID) && personagemManager.GetConfirmouPersonagem(otherPlayerID))
        {
            if (direcao > 0)
            {
                materialAtual++;
            }
            else if (direcao < 0)
            {
                materialAtual--;
            }
        }
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
        botao.GetComponent<scriptBotao>().SetVariantePlayer(materialAtual, playerID);
        SetMaterialPersonagem(materialAtual);
        GameObject.FindGameObjectWithTag("PersonagemManager").GetComponent<PersonagensManager>().SetVariante(playerID, materialAtual);
        GameObject.FindGameObjectWithTag("PersonagemManager").GetComponent<PersonagensManager>().TrocarVarianteTxt(playerID, VarianteDaSkin[materialAtual].nome);
    }
    void VerificacaoVarianteOutroPlayer()
    {
        for (int i = 0; i < 1; i++)
        {
            if (playerID != i)
            {
                materialOutroPlayer = botao.GetComponent<scriptBotao>().GetVariantePlayer(i);
            }
        }
    }
    public void SetPlayerID(int newPlayerID)
    { 
        playerID = newPlayerID;
    }
    public string GetVarianteAtual()
    {
        return VarianteDaSkin[materialAtual].nome;
    }
    public void SetMaterialPersonagem(int skin)
    {
        for (int i = 0; i < PartesDoCorpo.Length; i++)
        {
            PartesDoCorpo[i].GetComponent<Renderer>().material = VarianteDaSkin[skin].material[i];
        }
    }
}
