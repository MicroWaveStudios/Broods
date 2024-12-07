using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    public void CarregarGame(string nomeCena)
    {
        SceneManager.LoadScene(nomeCena);
    }
    public void Game()
    {
        SceneManager.LoadScene("GameAcademia");
    }
    public void GameBarDoBanana()
    {
        SceneManager.LoadScene("GameBarDoBanana");
    }
    
    public void Sair()
    {
        Application.Quit();
    }
    public void VoltarMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Jogar()
    {
        SceneManager.LoadScene("Jogar");
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void TutorialTeste()
    {
        SceneManager.LoadScene("Tutorial Teste");
    }

    public void CampoDeTreinamento()
    {
        SceneManager.LoadScene("CampoDeTreinamento");
    }
}