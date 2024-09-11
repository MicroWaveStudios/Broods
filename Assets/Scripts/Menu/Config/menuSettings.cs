using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class menuSettings : MonoBehaviour
{    
    private int qualidadeAtual;
    private bool isFullScreen;
    private float TelaCheia;
    [SerializeField] private bool onVsync;
    [SerializeField] private int vSync;

    [Header("Resolução")]
    int width;
    int height;
    private int resolucaoAtual_index = 0;
    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;
    private float currentRefreshRate;
    
    [Header("PopUps")]
    [SerializeField] private GameObject PopUpGraficos;
    [SerializeField] private GameObject PopUpConfigs;
    [SerializeField] private GameObject PopUpNaoSalvouGraficos;

    [Header("Botões")]
    [SerializeField] private TMP_Dropdown QualityDropdown;
    [SerializeField] private Toggle FullscreenToggle;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle vSyncToggle;

    private void Start()
    {
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

        QualityDropdown.value = PlayerPrefs.GetInt("quality");

        resolutionDropdown.value = PlayerPrefs.GetInt("resolution");
        resolucaoAtual_index = resolutionDropdown.value;

        vSync = PlayerPrefs.GetInt("vSync");
        CheckvSyncByFloat();
        vSyncToggle.isOn = onVsync;
        QualitySettings.vSyncCount = vSync;

        TelaCheia = PlayerPrefs.GetFloat("fullscreen");
        CheckFullcreenByFloat();
        FullscreenToggle.isOn = isFullScreen;
        Screen.fullScreen = isFullScreen;
    }
    private void Update()
    {
        CheckFullscreenByBool();
        CheckvSyncByBool();
    }

    public void mudarResolucao()
    {
        width = filteredResolutions[resolutionDropdown.value].width;
        height = filteredResolutions[resolutionDropdown.value].height;
        resolucaoAtual_index = resolutionDropdown.value;
        Screen.SetResolution(width, height, isFullScreen);
    }

    public void mudarGraficos()
    {
        qualidadeAtual = QualityDropdown.value;
        QualitySettings.SetQualityLevel(qualidadeAtual);
    }
    public void mudarvSync()
    {
        onVsync = vSyncToggle.isOn;

        if (vSyncToggle.isOn)
        {

            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }

        isFullScreen = true;
    }
    public void mudarFullScreen()
    {
        isFullScreen = FullscreenToggle.isOn;
        Screen.fullScreen = isFullScreen;
    }
    
    public void AplicarGraficos()
    {
        PlayerPrefs.SetInt("quality", qualidadeAtual);
        QualitySettings.SetQualityLevel(qualidadeAtual);

        PlayerPrefs.SetFloat("vSync", vSync);

        PlayerPrefs.SetFloat("fullscreen", TelaCheia);

        PlayerPrefs.SetInt("resolution", resolucaoAtual_index);
    }

    public void BotaoSairSemSalvarGraficos()
    {
        vSync = PlayerPrefs.GetInt("vSync");
        CheckvSyncByFloat();
        vSyncToggle.isOn = onVsync;

        qualidadeAtual = PlayerPrefs.GetInt("quality");
        QualityDropdown.value = qualidadeAtual;

        TelaCheia = PlayerPrefs.GetFloat("fullscreen");
        CheckFullcreenByFloat();
        FullscreenToggle.isOn = isFullScreen;

        resolutionDropdown.value = PlayerPrefs.GetInt("resolution");
    }

    public void BotaoVoltar()
    {
        if (vSync != PlayerPrefs.GetFloat("vSync") ||
            qualidadeAtual != PlayerPrefs.GetInt("quality") ||
            TelaCheia != PlayerPrefs.GetFloat("fullscreen") ||
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
        vSync = 1;
        vSyncToggle.isOn = true;
        onVsync = true;
        QualitySettings.vSyncCount = 1;

        qualidadeAtual = 1;
        QualityDropdown.value = 1;
        QualitySettings.SetQualityLevel(1);

        TelaCheia = 1;
        FullscreenToggle.isOn = true;
        isFullScreen = true;
        Screen.fullScreen = true;

        Resolution resolucaoAtual = Screen.currentResolution;
        Screen.SetResolution(1920, 1080, Screen.fullScreen);
        resolutionDropdown.value = resolutions.Length;
        resolucaoAtual_index = resolutionDropdown.value;

        AplicarGraficos();
    }

    public void CheckFullscreenByBool()
    {
        if (isFullScreen)
        {
            TelaCheia = 1;
        }
        else
        {
            TelaCheia = 0;
        }
    }

    public void CheckFullcreenByFloat()
    {
        if (TelaCheia == 1)
        {
            isFullScreen = true;
        }
        else
        {
            isFullScreen = false;
        }
    }

    public void CheckvSyncByBool()
    {
        if (onVsync)
        {
            vSync = 1;
        }
        else
        {
            vSync = 0;
        }
    }

    public void CheckvSyncByFloat()
    {
        if (vSync == 1)
        {
            onVsync = true;
        }
        else
        {
            onVsync = false;
        }
    }
}