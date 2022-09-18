using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource[] destroyNoice;

    public void PlayRandomDestroyNoice()
    {
        int ClipToPlay = Random.Range(0, destroyNoice.Length);
        destroyNoice[ClipToPlay].Play();
    }

}
