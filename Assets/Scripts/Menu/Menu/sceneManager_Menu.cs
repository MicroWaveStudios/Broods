using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager_Menu : MonoBehaviour
{
    public void Jogar()
    {
        SceneManager.LoadScene(2);
    }

    public void Tutorial()
    {
        SceneManager.LoadScene(3);
    }

    public void CampoDeTreinamento()
    {
        SceneManager.LoadScene(4);
    }

    public void Personagens()
    {
        SceneManager.LoadScene(5);
    }

    public void Ajustes()
    {
        SceneManager.LoadScene(6);
    }

    public void Sair()
    {
        Application.Quit();
    }
}
