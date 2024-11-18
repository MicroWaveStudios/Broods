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

    [Header("PanelManager")]
    PanelsManager panelsManager;

    private void Start()
    {
        panelsManager = FindObjectOfType<PanelsManager>();

        volumeGeral.value = PlayerPrefs.GetFloat("VolumeGeral");
        AtualizarTextoVolumeGeral();

        BG.value = PlayerPrefs.GetFloat("VolumeBG");
        AtualizarTextoBG();

        VFX.value = PlayerPrefs.GetFloat("VolumeVFX");
        AtualizarTextoVFX();
    }

    public void mudarVolumeGeral()
    {
        audioMixer.SetFloat("volumeGeral", volumeGeral.value);
        AtualizarTextoVolumeGeral();
    }

    public void mudarBG()
    {
        audioMixer.SetFloat("BG", BG.value);
        AtualizarTextoBG();
    }

    public void mudarVFX()
    {
        audioMixer.SetFloat("VFX", VFX.value);
        AtualizarTextoVFX();
    }

    public void AplicarVolume()
    {
        PlayerPrefs.SetFloat("VolumeGeral", volumeGeral.value);
        PlayerPrefs.SetString("txtVolumeGeral", volumeGeralText.text);

        PlayerPrefs.SetFloat("VolumeBG", BG.value);
        PlayerPrefs.SetString("txtVolumeBG", BGText.text);

        PlayerPrefs.SetFloat("VolumeVFX", VFX.value);
        PlayerPrefs.SetString("txtVolumeVFX", VFXText.text);
    }

    public void RestaurarVolume()
    {
        audioMixer.SetFloat("volumeGeral", 0f);
        volumeGeral.value = 0f;
        AtualizarTextoVolumeGeral();

        audioMixer.SetFloat("BG", 0f);
        BG.value = 0f;
        AtualizarTextoBG();

        audioMixer.SetFloat("VFX", 0f);
        VFX.value = 0f;
        AtualizarTextoVFX();

        AplicarVolume();
    }

    public void BotaoVoltar()
    {
        if (volumeGeral.value != PlayerPrefs.GetFloat("VolumeGeral") ||
            BG.value != PlayerPrefs.GetFloat("VolumeBG") ||
            VFX.value != PlayerPrefs.GetFloat("VolumeVFX"))
        {
            //PopUpNaoSalvou.SetActive(true);
            panelsManager.ChangePanel(6);
        }
        else
        {
            //JanelaConfigs.SetActive(true);
            panelsManager.ChangePanel(1);
        }
    }

    public void NaoSalvou()
    {
        volumeGeral.value = PlayerPrefs.GetFloat("VolumeGeral");
        BG.value = PlayerPrefs.GetFloat("VolumeBG");
        VFX.value = PlayerPrefs.GetFloat("VolumeVFX");
    }

    private void AtualizarTextoVolumeGeral()
    {
        volumeGeralText.text = ((volumeGeral.normalizedValue * 100f).ToString());

        if (volumeGeral.value == 0f)
        {
            volumeGeralText.text = "100";
        }

        if (volumeGeral.value == -80f)
        {
            volumeGeralText.text = "0";
        }
    }
    private void AtualizarTextoBG()
    {
        BGText.text = (BG.normalizedValue * 100f).ToString();

        if (BG.value == 0f)
        {
            BGText.text = "100";
        }

        if (BG.value == -80f)
        {
            BGText.text = "0";
        }
    }
    private void AtualizarTextoVFX()
    {
        VFXText.text = (VFX.normalizedValue * 100f).ToString();

        if (VFX.value == 0f)
        {
            VFXText.text = "100";
        }

        if (VFX.value == -80)
        {
            VFXText.text = "0";
        }
    }
}