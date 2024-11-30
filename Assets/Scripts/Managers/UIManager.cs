using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject PausePanel;
    [SerializeField] GameObject[] ButtonPanel;
    [SerializeField] GameObject DefaultButtonSelected;
    [SerializeField] GameObject EventSystemManager;
    [SerializeField] GameObject background;

    public void UIStatePause(bool value)
    {
        SetActiveMenuButton(value);
    }

    void SetActiveMenuButton(bool value)
    { 
        PausePanel.SetActive(value);
        EventSystemManager.GetComponent<EventSystemManager>().SetCurrentSelectedButton(DefaultButtonSelected);

        if (value)
        {
            background.GetComponent<AudioSource>().Pause();
        }
        else
        {
            background.GetComponent<AudioSource>().UnPause();
        }
    }

    public void ButtonPressed(int numberButton)
    { 
        for (int i = 0; i < ButtonPanel.Length; i++) 
        {
            if (i == numberButton)
                ButtonPanel[i].SetActive(true);
            else
                ButtonPanel[i].SetActive(false);
        }
    }
}
