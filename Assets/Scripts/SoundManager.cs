using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioSource[] destroyNoice;

    public void PlayRandomDestroyNoice()
    {
        int ClipToPlay = Random.Range(0, destroyNoice.Length);
        destroyNoice[ClipToPlay].Play();
    }

    [SerializeField] private AudioMixer audioMix;

    public void ControlVolume(float volume)
    {
        audioMix.SetFloat("Volume", volume);
    }

}