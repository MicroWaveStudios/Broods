using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ComandosTutorial : MonoBehaviour
{
    [SerializeField] public List<TMP_Text> acao = new List<TMP_Text>();
    [SerializeField] private GameObject instrucoes;
    [SerializeField] private GameObject painelFimTutorial;
    PlayerMoveRigidbody playerMove;
    PlayerCombat playerCombat;
    public bool podeAtacar = true;

    [SerializeField] sceneManager sceneManager;
    
    private void Start()
    {
        playerMove = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerMoveRigidbody>();
        playerCombat = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerCombat>();
    }
    private void Update()
    {
        Jogando();

        if (playerCombat.GetInAttack())
        {
            podeAtacar = false;
        }
        else
        {
            podeAtacar = true;
        }
    }

    public void Jogando()
    {
        instrucoes.SetActive(true);

        if (podeAtacar)
        {

        if (playerMove.GetDirecaoX() == 1f)
        {
            acao[0].color = Color.green;
        }

        if (playerMove.GetDirecaoX() == -1f)
        {
            acao[1].color = Color.green;
        }

        if (!playerMove.GetNoChao())
        {
            acao[2].color = Color.green;
        }

        if (playerMove.GetEstaAgachado())
        {
            acao[3].color = Color.green;
        }

        if (playerCombat.GetAtacouLeve())
        {
            acao[4].color = Color.green;
        }

        if (playerCombat.GetAtacouMedio())
        {
            acao[5].color = Color.green;
        }

        if (playerCombat.GetAtacouPesado())
        {
            acao[6].color = Color.green;
        }

        if (playerCombat.GetAtacouBaixo())
        {
            acao[7].color = Color.green;
        }
        }

        int x = 0;

        for (int i = 0; i < acao.Count; i++)
        {
            if (acao[i].color == Color.green)
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
        //painelFimTutorial.SetActive(true);
        sceneManager.GetComponent<sceneManager>().VoltarMenu();
    }
}