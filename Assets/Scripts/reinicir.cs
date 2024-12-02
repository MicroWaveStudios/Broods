using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class reinicir : MonoBehaviour
{
    
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Z))
        {
            SceneManager.LoadScene("Jogar");
        }
    }
}
