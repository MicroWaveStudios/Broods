using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ComandosTutorial : MonoBehaviour
{
    [SerializeField] private List<TMP_Text> acao = new List<TMP_Text>();
    [SerializeField] private List<bool> confirmacoes = new List<bool>();
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
    private void Update()
    {
        Jogando();    
    }

    void Jogando()
    {
        instrucoes.SetActive(true);

        if (playerMove.GetDirecaoX() == 1f)
        {
            acao[0].fontStyle = FontStyles.Strikethrough;
        }

        if (playerMove.GetDirecaoX() == -1f)
        {
            acao[1].fontStyle = FontStyles.Strikethrough;
        }

        if (!playerMove.GetNoChao())
        {
            acao[2].fontStyle = FontStyles.Strikethrough;
        }

        if (playerMove.GetEstaAgachado())
        {
            acao[3].fontStyle = FontStyles.Strikethrough;
        }

        if (playerCombat.GetAtacouLeve())
        {
            acao[4].fontStyle = FontStyles.Strikethrough;
        }

        if (playerCombat.GetAtacouMedio())
        {
            acao[5].fontStyle = FontStyles.Strikethrough;
        }

        if (playerCombat.GetAtacouPesado())
        {
            acao[6].fontStyle = FontStyles.Strikethrough;
        }

        if (playerCombat.GetAtacouBaixo())
        {
            acao[7].fontStyle = FontStyles.Strikethrough;
        }

        int x = 0;

        for (int i = 0; i < acao.Count; i++)
        {
            if (acao[i].fontStyle == FontStyles.Strikethrough)
            {
                x++;
            }
            else
            {
                x = 0;
            }
        }

        if (x == acao.Count)
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