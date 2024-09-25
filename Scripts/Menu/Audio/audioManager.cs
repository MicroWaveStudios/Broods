using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class audioManager : MonoBehaviour
{
    public void Som()
    {
        SceneManager.LoadScene(10);
    }

    public void Controles()
    {
        SceneManager.LoadScene(11);
    }

    public void Video()
    {
        SceneManager.LoadScene(12);
    }
}
