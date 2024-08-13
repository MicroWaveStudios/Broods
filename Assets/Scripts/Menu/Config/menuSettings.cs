using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class menuSettings : MonoBehaviour
{
    public TMP_Dropdown graphicsDropdown;
    public Slider volumeGeral;
    public Slider BG;
    public AudioMixer audioMixer;

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
