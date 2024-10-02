using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    public void Game()
    {
        SceneManager.LoadScene("Game");
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

    public void CampoDeTreinamento()
    {
        SceneManager.LoadScene("CampoDeTreinamento");
    }

    public void Personagens()
    {
        SceneManager.LoadScene("Personagens");
    }
    public void Sair()
    {
        Application.Quit();
    }
}
