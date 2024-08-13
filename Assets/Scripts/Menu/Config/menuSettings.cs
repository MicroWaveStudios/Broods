using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class menuSettings : MonoBehaviour
{
    public TMP_Dropdown graphicsDropdown;
    public TMP_Dropdown resolutionDropdown;

    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;
    private float currentRefreshRate;
    private int resolucaoAtual_index = 0;

    public Slider volumeGeral;
    public Slider BG;
    public AudioMixer audioMixer;

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

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
    }

    public void mudarGraficos()
    {
        QualitySettings.SetQualityLevel(graphicsDropdown.value);
    }

    public void mudarVolumeGeral()
    {
        audioMixer.SetFloat("VolumeGeral", volumeGeral.value);
    }

    public void mudarBG()
    {
        audioMixer.SetFloat("BG", BG.value);
    }
}
