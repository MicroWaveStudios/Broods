using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialPlayer : MonoBehaviour
{
    //[SerializeField] Material[] MaterialPlayer1;

    //[SerializeField] Material[] MaterialPlayer2;

    [System.Serializable]
    struct ListaMaterial
    {
        public string nome;
        public Material[] material;
    }

    [SerializeField] List<ListaMaterial> VarianteDaSkin = new List<ListaMaterial>();

    [SerializeField] GameObject[] PartesDoCorpo;
    GameObject botao;

    int playerID;
    int materialAtual;

    private void Start()
    {
        TrocarMaterial(0);
    }
    public void SetBotao(GameObject newBotao)
    {
        botao = newBotao;
    }
    public void TrocarMaterial(float direcao)
    {
        float material = materialAtual + direcao;
        //botao.GetComponent<scriptBotao>().GetVariantePlayer(0) != botao.GetComponent<scriptBotao>().GetVariantePlayer(1)
        if (material < VarianteDaSkin.Count && material >= 0)
        {
            materialAtual = (int)material;
            botao.GetComponent<scriptBotao>().SetVariantePlayer(materialAtual, playerID);
            SetMaterialPersonagem(materialAtual);
            GameObject.FindGameObjectWithTag("PersonagemManager").GetComponent<PersonagensManager>().TrocarVarianteTxt(playerID, VarianteDaSkin[materialAtual].nome);
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
