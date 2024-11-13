using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sons : MonoBehaviour
{
    [System.Serializable]

    struct Audios
    {
        public string nome;
        public AudioSource som;
    }

    [SerializeField] List<Audios> sons = new List<Audios>();

    public void TocarSom(string NomeMusica)
    {
        for (int i = 0; i < sons.Count; i++)
        {
            if (sons[i].nome == NomeMusica)
            {
                sons[i].som.Play();
            }
        }
    }
}
