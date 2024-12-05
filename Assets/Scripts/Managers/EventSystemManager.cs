using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class EventSystemManager : MonoBehaviour
{
    [SerializeField] EventSystem eventSystem;
    [SerializeField] InputSystemUIInputModule inputSystemUIInputModule;

    public void SetCurrentSelectedButton(GameObject newSelectedButton)
    {
        StartCoroutine(SelectedButton(newSelectedButton));
        //eventSystem.SetSelectedGameObject(newSelectedButton);
        //eventSystem.firstSelectedGameObject = newSelectedButton;
        //Button newSelectable = newSelectedButton.GetComponent<Button>();
        //newSelectable.Select();
        //newSelectable.OnSelect(null);
    }
    IEnumerator SelectedButton(GameObject newSelectedButton)
    {
        yield return new WaitForSeconds(0.001f);
        eventSystem.SetSelectedGameObject(newSelectedButton);
        eventSystem.firstSelectedGameObject = newSelectedButton;
        if (newSelectedButton != null)
        {
            Button newSelectable = newSelectedButton.GetComponent<Button>();
            newSelectable.Select();
        }
    }
}
