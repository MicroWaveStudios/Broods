using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Audio;

public class menuSettings : MonoBehaviour
{
    [Header("Brilho")]
    [SerializeField] private Slider sliderBrilho;
    [SerializeField] private TMP_Text textBrilho;
    
    private float valorBrilho;
    private bool isFullScreen;
    private int qualidadeAtual;

    [Header("Resolução")]
    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;
    private float currentRefreshRate;
    [SerializeField] private int resolucaoAtual_index = 0;
    
    [Header("PopUps")]
    [SerializeField] private GameObject PopUpGraficos;
    [SerializeField] private GameObject PopUpConfigs;
    [SerializeField] private GameObject PopUpNaoSalvouGraficos;

    [Header("Botões")]
    [SerializeField] private TMP_Dropdown QualityDropdown;
    [SerializeField] private Toggle FullscreenToggle;
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    private void Start()
    {
        QualityDropdown.value = PlayerPrefs.GetInt("quality");
    
        isFullScreen = (PlayerPrefs.GetInt("fullscreen") != 0);
        FullscreenToggle.isOn = isFullScreen;

        sliderBrilho.value = PlayerPrefs.GetFloat("brightness");
        textBrilho.text = sliderBrilho.value.ToString();

        resolutionDropdown.value = PlayerPrefs.GetInt("resolution");
        resolucaoAtual_index = resolutionDropdown.value;

        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();
        resolutionDropdown.ClearOptions();
        currentRefreshRate = Screen.currentResolution.refreshRate;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].refreshRate == currentRefreshRate)
            {
                filteredResolutions.Add(resolutions[i]);
            }
        }

        List<string> options = new List<string>();

        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            string opcoes = filteredResolutions[i].width + "x" + filteredResolutions[i].height;
            options.Add(opcoes);    
            
            if (filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height)
            {
                resolucaoAtual_index = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = resolucaoAtual_index;
        resolutionDropdown.RefreshShownValue();
    }

    public void mudarResolucao(int ResolutionIndex)
    {
        Resolution resolucao = filteredResolutions[ResolutionIndex];
        Screen.SetResolution(resolucao.width, resolucao.height, true);
        resolucaoAtual_index = ResolutionIndex;
        resolucaoAtual_index = resolutionDropdown.value;
    }

    public void mudarGraficos()
    {
        qualidadeAtual = QualityDropdown.value;
    }

    public void mudarBrilho()
    {
        valorBrilho = sliderBrilho.value;
        textBrilho.text = sliderBrilho.value.ToString();
    }

    public void mudarFullScreen(bool isFullscreen)
    {
        this.isFullScreen = isFullscreen;
    }
    
    public void AplicarGraficos()
    {
        PlayerPrefs.SetInt("quality", qualidadeAtual);
        QualitySettings.SetQualityLevel(qualidadeAtual);

        PlayerPrefs.SetFloat("brightness", valorBrilho);

        PlayerPrefs.SetInt("fullscreen", isFullScreen ? 1:0);

        PlayerPrefs.SetInt("resolution", resolucaoAtual_index);
    }

    public void BotaoSairSemSalvarGraficos()
    {
        sliderBrilho.value = PlayerPrefs.GetFloat("brightness");

        qualidadeAtual = PlayerPrefs.GetInt("quality");
        QualityDropdown.value = qualidadeAtual;

        isFullScreen = (PlayerPrefs.GetInt("fullscreen") != 0);
        FullscreenToggle.isOn = isFullScreen;

        resolutionDropdown.value = PlayerPrefs.GetInt("resolution");
    }

    public void BotaoVoltar()
    {
        if (sliderBrilho.value != PlayerPrefs.GetFloat("brightness") ||
            qualidadeAtual != PlayerPrefs.GetInt("quality") ||
            isFullScreen != (PlayerPrefs.GetInt("fullscreen") != 0) ||
            resolucaoAtual_index != PlayerPrefs.GetInt("resolution"))
        {
            PopUpNaoSalvouGraficos.SetActive(true);
            PopUpGraficos.SetActive(false);
        }
        else
        {
            PopUpConfigs.SetActive(true);
            PopUpGraficos.SetActive(false);
        }
    }

    public void BotaoRestaurarGraficos()
    {
        valorBrilho = 50f;
        sliderBrilho.value = 50f;
        textBrilho.text = valorBrilho.ToString();

        qualidadeAtual = 1;
        QualityDropdown.value = 1;
        QualitySettings.SetQualityLevel(1); //nao sei se funciona

        isFullScreen = true;
        FullscreenToggle.isOn = true;
        Screen.fullScreen = true; // idem

        Resolution resolucaoAtual = Screen.currentResolution;
        Screen.SetResolution(resolucaoAtual.width, resolucaoAtual.height, Screen.fullScreen);
        resolutionDropdown.value = resolutions.Length;
        resolucaoAtual_index = resolutionDropdown.value;

        AplicarGraficos();
    }
}
