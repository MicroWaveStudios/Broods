using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem.LowLevel;
using System.Diagnostics.Tracing;

public class Dialogo_UI : MonoBehaviour
{
     Image background;
     TMP_Text texto;
     TMP_Text nome;
     Sprite icon;

    [SerializeField] GameObject resto;

    bool aberto = false;
    float speed = 20f;

    private void Awake()
    {
        background = transform.GetChild(0).GetComponent<Image>();
        texto = transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>();
        nome = transform.GetChild(1).GetChild(1).GetComponent<TMP_Text>();
        icon = transform.GetChild(1).GetChild(2).GetComponent<Sprite>();
    }

    void Update()
    {
        if (aberto)
        {
            background.fillAmount = Mathf.Lerp(background.fillAmount, 1, speed * Time.deltaTime);
            HabilitarResto();
        }
        else
        {
            background.fillAmount = Mathf.Lerp(background.fillAmount, 0, speed * Time.deltaTime);
            DesabilitarResto();
        }
    }

    public void SetName(string value)
    {
        nome.text = value;
    }

    public void SetIcon(Sprite value)
    {
        icon = value;
    }

    public void Enable()
    {
        aberto = true;
        background.fillAmount = 0;
    }

    public void Disable()
    {
        aberto = false;
        nome.text = "";
        texto.text = "";
    }

    void DesabilitarResto()
    {
        resto.SetActive(false);
    }

    void HabilitarResto()
    {
        resto.SetActive(true);
    }
}
