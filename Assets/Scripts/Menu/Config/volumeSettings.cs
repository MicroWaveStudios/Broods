using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class volumeSettings : MonoBehaviour
{
    public Slider volumeGeral;
    public Slider BG;
    public Slider SFX;
    public AudioMixer audioMixer;

    public void mudarVolumeGeral()
    {
        audioMixer.SetFloat("VolumeGeral", volumeGeral.value);
    }

    public void mudarBG()
    {
        audioMixer.SetFloat("BG", BG.value);
    }

    public void mudarSFX()
    {
        audioMixer.SetFloat("SFX", SFX.value);
    }
}
