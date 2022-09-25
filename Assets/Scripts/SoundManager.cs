using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource[] destroyNoice;
    public Slider volControl;
    public GameObject[] audios;

    private void Start()
    {
        audios = GameObject.FindGameObjectsWithTag("audio");
        volControl.value = PlayerPrefs.GetFloat("volSave", 1f);
    }

    private void Update()
    {
        foreach (GameObject au in audios)
        {
            au.GetComponent<AudioSource>().volume = volControl.value;
        }
    }

    public void saveVol()
    {
        PlayerPrefs.SetFloat("volSave", volControl.value);
    }

    public void PlayRandomDestroyNoice()
    {
        if (PlayerPrefs.HasKey("Sound"))
        {
            if (PlayerPrefs.GetInt("Sound") == 1)
            {
                int ClipToPlay = Random.Range(0, destroyNoice.Length);
                destroyNoice[ClipToPlay].Play();
            }
        }
        else
        {
            int ClipToPlay = Random.Range(0, destroyNoice.Length);
            destroyNoice[ClipToPlay].Play();
        }
    }

}