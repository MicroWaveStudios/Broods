using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager_Menu : MonoBehaviour
{
    public void Game()
    {
        SceneManager.LoadScene(8);
    }
    public void Sair()
    {
        Application.Quit();
    }
    public void VoltarMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Jogar()
    {
        SceneManager.LoadScene(1);
    }

    public void Tutorial()
    {
        SceneManager.LoadScene(2);
    }

    public void CampoDeTreinamento()
    {
        SceneManager.LoadScene(3);
    }

    public void Personagens()
    {
        SceneManager.LoadScene(4);
    }

    public void Gameplay()
    {
        SceneManager.LoadScene(5);
    }
}
