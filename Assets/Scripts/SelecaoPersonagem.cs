using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class SelecaoPersonagem : MonoBehaviour
{
    [SerializeField] GameObject BordaSelecaoPersonagem;
    [SerializeField] TMP_Text textoBorda;
    [SerializeField] Color[] CorBorda;
    [SerializeField] Vector3[] PosicaoTexto;

    PersonagensManager personagensManager;

    GameObject botaoAtual;

    private void Start()
    {
        personagensManager = GameObject.FindGameObjectWithTag("PersonagemManager").GetComponent<PersonagensManager>();
    }

    public void TrocarCorDaBorda(int playerIndex)
    {
        BordaSelecaoPersonagem.GetComponent<SpriteRenderer>().color = CorBorda[playerIndex];
        int playerText = playerIndex + 1;
        textoBorda.GetComponent<Transform>().position = PosicaoTexto[playerIndex];
        textoBorda.color = CorBorda[playerIndex];
        textoBorda.text = "P" + playerText;

        //BordaSelecaoPersonagem.GetComponentInChildren<TextMeshPro>().text = "P" + playerText;
        //BordaSelecaoPersonagem.GetComponentInChildren<TextMeshPro>().color = CorBorda[playerIndex];
    }

    public void Navegacao(InputAction.CallbackContext context)
    {
        Vector2 orientacao = context.ReadValue<Vector2>();
        Debug.Log("naSelecaoDePersonagem = " + GameObject.FindGameObjectWithTag("PersonagemManager").GetComponent<PersonagensManager>().GetNaSelecaoDePersonagem());
        //Debug.Log(orientacao);
        if (orientacao != Vector2.zero && GameObject.FindGameObjectWithTag("PersonagemManager").GetComponent<PersonagensManager>())
        {
            switch (orientacao)
            {
                case Vector2 when orientacao.Equals(Vector2.up):
                    Debug.Log("Up");
                    break;
                case Vector2 when orientacao.Equals(Vector2.down):
                    Debug.Log("Down");
                    break;
                case Vector2 when orientacao.Equals(Vector2.right):
                    Debug.Log("Right");
                    break;
                case Vector2 when orientacao.Equals(Vector2.left):
                    Debug.Log("Left");
                    break;
            }
        }
    }

}
