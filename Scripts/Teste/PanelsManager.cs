using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelsManager : MonoBehaviour
{
    [SerializeField] GameObject eventSystemManager;
    [SerializeField] GameObject[] panel;
    [SerializeField] GameObject[] defaultPanelButton;
    int currentPanel;

    private void Start()
    {
        //ChangePanel(0);
    }

    public void ChangePanel(int numberPanel)
    {
        panel[currentPanel].SetActive(false);
        currentPanel = numberPanel;
        panel[currentPanel].SetActive(true);
        eventSystemManager.GetComponent<EventSystemManager>().SetCurrentSelectedButton(defaultPanelButton[currentPanel]);
    }

    public void ClosePanel()
    {
        panel[currentPanel].SetActive(false);
        currentPanel = 0;
        eventSystemManager.GetComponent<EventSystemManager>().SetCurrentSelectedButton(defaultPanelButton[currentPanel]);
    }

}
