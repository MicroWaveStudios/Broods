using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class volumeSettings : MonoBehaviour
{
    [Header("Sliders")]
    public Slider volumeGeral;
    public Slider BG;
    public Slider VFX;

    [Header("Textos")]
    public TMP_Text volumeGeralText;
    public TMP_Text BGText;
    public TMP_Text VFXText;

    [Header("Audios")]
    public AudioMixer audioMixer;
    public float defaultAudio = 60f;

    [Header("PopUps")]
    public GameObject PopUpNaoSalvou;
    public GameObject JanelaSom;
    public GameObject JanelaConfigs;

    private void Start()
    {
        volumeGeral.value = PlayerPrefs.GetFloat("volumeGeral");
        BG.value = PlayerPrefs.GetFloat("volumeBG");
        VFX.value = PlayerPrefs.GetFloat("volumeVFX");
    }
    public void mudarVolumeGeral()
    {
        audioMixer.SetFloat("VolumeGeral", volumeGeral.value);
        volumeGeralText.text = volumeGeral.normalizedValue.ToString("0.0");
    }

    public void mudarBG()
    {
        audioMixer.SetFloat("BG", BG.value);
        BGText.text = BG.normalizedValue.ToString("0.0");
    }

    public void mudarVFX()
    {
        audioMixer.SetFloat("SFX", VFX.value);
        VFXText.text = VFX.normalizedValue.ToString("0.0");
    }

    public void AplicarVolume()
    {
        PlayerPrefs.SetFloat("volumeGeral", volumeGeral.value);
        PlayerPrefs.SetFloat("volumeBG", BG.value);
        PlayerPrefs.SetFloat("volumeVFX", VFX.value);
    }

    public void RestaurarVolume()
    {
        audioMixer.SetFloat("volumeGeral", defaultAudio);
        volumeGeral.value = defaultAudio;
        volumeGeralText.text = "1,0";

        audioMixer.SetFloat("BG", defaultAudio);
        BG.value = defaultAudio;
        BGText.text = "1,0";

        audioMixer.SetFloat("VFX", defaultAudio);
        VFX.value = defaultAudio;
        VFXText.text = "1,0";

        AplicarVolume();
    }

    public void VoltarSemSalvar()
    {
        if (volumeGeral.value != PlayerPrefs.GetFloat("volumeGeral") ||
            BG.value != PlayerPrefs.GetFloat("volumeBG") ||
            VFX.value != PlayerPrefs.GetFloat("volumeVFX"))
        {
            PopUpNaoSalvou.SetActive(true);
            JanelaSom.SetActive(false);
        }
        else
        {
            JanelaConfigs.SetActive(true);
            JanelaSom.SetActive(false);
        }
    }

    public void NaoSalvou()
    {
        volumeGeral.value = PlayerPrefs.GetFloat("volumeGeral");
        BG.value = PlayerPrefs.GetFloat("volumeBG");
        VFX.value = PlayerPrefs.GetFloat("volumeVFX");
    }

    private void Update()
    {
        Debug.Log("Geral " + PlayerPrefs.GetFloat("volumeGeral"));
        Debug.Log("BG " + PlayerPrefs.GetFloat("volumeBG"));
        Debug.Log("VFX " + PlayerPrefs.GetFloat("volumeVFX"));
    }
}