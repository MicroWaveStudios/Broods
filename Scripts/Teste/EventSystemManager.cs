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
        eventSystem.SetSelectedGameObject(newSelectedButton);
        Button newSelectable = newSelectedButton.GetComponent<Button>();
        newSelectable.Select();
        //newSelectable.OnSelect(null);
    }
}
