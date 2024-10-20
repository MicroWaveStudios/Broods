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

    public bool isPlayer2 = false;


    private void Start()
    {
        //if (transform.parent.transform.parent.gameObject.CompareTag("Player2"))
        //{
        //    isPlayer2 = true;
        //}
        //else
        //{
        //    isPlayer2 = false;
        //}

        //if (isPlayer2)
        //{
        //    for(int i = 0; i < PartesDoCorpo.Length; i++)
        //    {
        //        PartesDoCorpo[i].GetComponent<Renderer>().material = MaterialPlayer2[i];
        //    }
        //}
        //else
        //{
        //    for (int i = 0; i < PartesDoCorpo.Length; i++)
        //    {
        //        PartesDoCorpo[i].GetComponent<Renderer>().material = MaterialPlayer1[i];
        //    }
        //}
    }

    public void SetMaterialPersonagem(int skin)
    {
        for (int i = 0; i < PartesDoCorpo.Length; i++)
        {
            PartesDoCorpo[i].GetComponent<Renderer>().material = VarianteDaSkin[skin].material[i];
        }
    }
}
