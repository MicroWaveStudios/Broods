using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ComandosTutorial : MonoBehaviour
{
    [SerializeField] private List<GameObject> acao = new List<GameObject>();
    [SerializeField] private GameObject instrucoes;
    [SerializeField] private GameObject painelFimTutorial;
    PlayerMoveRigidbody playerMove;
    PlayerCombat playerCombat;
    PlayerInput playerInput;

    private void Start()
    {
        playerMove = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerMoveRigidbody>();
        playerCombat = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerCombat>();
        playerInput = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerInput>();
        playerInput.enabled = true;
    
    }
    void Jogando()
    {
        instrucoes.SetActive(true);

        if (playerMove.GetDirecaoX() == 1f)
        {
            //acao[0].fontStyle = FontStyles.Strikethrough;
            Destroy(acao[0]);
        }

        if (playerMove.GetDirecaoX() == -1f)
        {
            //acao[1].fontStyle = FontStyles.Strikethrough;
            Destroy(acao[1]);
        }

        if (!playerMove.GetNoChao())
        {
            //acao[2].fontStyle = FontStyles.Strikethrough;
            Destroy(acao[2]);
        }

        if (playerMove.GetEstaAgachado())
        {
            //acao[3].fontStyle = FontStyles.Strikethrough;
            Destroy(acao[3]);
        }

        if (playerCombat.GetAtacouLeve())
        {
            //acao[4].fontStyle = FontStyles.Strikethrough;
            Destroy(acao[4]);
        }

        if (playerCombat.GetAtacouMedio())
        {
            //acao[5].fontStyle = FontStyles.Strikethrough;
            Destroy(acao[5]);
        }

        if (playerCombat.GetAtacouPesado())
        {
            //acao[6].fontStyle = FontStyles.Strikethrough;
            Destroy(acao[6]);
        }

        if (playerCombat.GetAtacouBaixo())
        {
            //acao[7].fontStyle = FontStyles.Strikethrough;
            Destroy(acao[7]);
        }

        if (instrucoes.transform.childCount == 0)
        {
            StartCoroutine(Acabou());
        }
    }

    private IEnumerator Acabou()
    {
        instrucoes.SetActive(false);
        yield return new WaitForSeconds(1f);
        painelFimTutorial.SetActive(true);
    }
}