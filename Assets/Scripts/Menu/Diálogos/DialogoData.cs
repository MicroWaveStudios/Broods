using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct Dialogue
{
    public string nome;
    public Sprite icon;

    [TextArea(5, 10)]
    public string text;
}

[CreateAssetMenu(fileName = "DialogueData", menuName = "ScriptableObject/TalkScript")]
public class DialogoData : ScriptableObject
{
    public List<Dialogue> scriptFalas;
}


