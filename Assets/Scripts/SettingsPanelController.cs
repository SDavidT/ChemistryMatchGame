using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanelController : MonoBehaviour
{
    public Animator panelSettings;
    public Animator panelHowPlay;
    public Image soundButton;
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;

    void Start()
    {
        if (PlayerPrefs.HasKey("Sound"))
        {
            if (PlayerPrefs.GetInt("Sound") == 0)
            {
                soundButton.sprite = musicOffSprite;
            }
            else
            {
                soundButton.sprite = musicOnSprite;
            }
        }
        else
        {
            soundButton.sprite = musicOnSprite;
        }
    }

    public void ExitSettings()
    {
        if (panelSettings != null)
        {
            panelSettings.SetBool("Out2", true);
            panelSettings.SetBool("In", false);
        }
    }

    public void InSettings()
    {
        if (panelSettings != null)
        {
            panelSettings.SetBool("In", true);
            panelSettings.SetBool("Out2", false);
        }
    }

    public void ExitHowPlay()
    {
        if (panelHowPlay != null)
        {
            panelHowPlay.SetBool("HowOut", true);
            panelHowPlay.SetBool("HowIn", false);
        }
    }

    public void InHowPlay()
    {
        if (panelHowPlay != null)
        {
            ExitSettings();
            panelHowPlay.SetBool("HowIn", true);
            panelHowPlay.SetBool("HowOut", false);
        }
    }

    public void SoundButton()
    {
        if (PlayerPrefs.HasKey("Sound"))
        {
            if (PlayerPrefs.GetInt("Sound") == 0)
            {
                soundButton.sprite = musicOnSprite;
                PlayerPrefs.SetInt("Sound", 1);
            }
            else
            {
                soundButton.sprite = musicOffSprite;
                PlayerPrefs.SetInt("Sound", 0);
            }
        }
        else
        {
            soundButton.sprite = musicOffSprite;
            PlayerPrefs.SetInt("Sound", 1);
        }
    }
}
