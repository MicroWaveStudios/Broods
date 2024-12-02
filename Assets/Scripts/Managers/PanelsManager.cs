using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelsManager : MonoBehaviour
{
    [SerializeField] GameObject eventSystemManager;
    [SerializeField] GameObject[] panel;
    [SerializeField] GameObject[] defaultPanelButton;
    int currentPanel;
    public void ChangePanel(int numberPanel)
    {
        panel[currentPanel].SetActive(false);
        currentPanel = numberPanel;
        panel[currentPanel].SetActive(true);

        if (defaultPanelButton[currentPanel] != null)
        {
            eventSystemManager.GetComponent<EventSystemManager>().SetCurrentSelectedButton(defaultPanelButton[currentPanel]);
        }
    }

    public void ClosePanel()
    {
        panel[currentPanel].SetActive(false);
        currentPanel = 0;
        eventSystemManager.GetComponent<EventSystemManager>().SetCurrentSelectedButton(defaultPanelButton[currentPanel]);
    }

    public int GetPanel()
    {
        return currentPanel;
    }
}
