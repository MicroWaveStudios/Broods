using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager_Menu : MonoBehaviour
{
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

    public void Ajustes()
    {
        SceneManager.LoadScene(5);
    }

    public void Jogar_Configuracoes()
    {
        SceneManager.LoadScene(6);
    }

    public void Jogar_Mapa()
    {
        SceneManager.LoadScene(7);
    }

    public void Jogar_Personagens()
    {
        SceneManager.LoadScene(8);
    }

    public void Ajustes_Som()
    {
        SceneManager.LoadScene(9);
    }

    public void Ajustes_Controles()
    {
        SceneManager.LoadScene(10);
    }

    public void Ajustes_Video()
    {
        SceneManager.LoadScene(11);
    }
}
