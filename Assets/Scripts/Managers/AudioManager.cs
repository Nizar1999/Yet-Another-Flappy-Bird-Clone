using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource sound;

    private void Awake()
    {
        sound = GetComponent<AudioSource>();
        instance = this;
    }

    public void playSound(AudioClip soundClip, float time = 0f)
    {
        sound.clip = soundClip;
        sound.time = time;
        sound.Play();
    }
}
