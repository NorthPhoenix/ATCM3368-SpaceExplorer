using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioHelper
{
    public static GameObject BGM = null;
    public static AudioSource PlayClip2D(AudioClip clip, float volume = 0.5f)
    {
        GameObject audioObject = new GameObject("2D Audio");
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();

        audioSource.clip = clip;
        audioSource.volume = volume;

        audioSource.Play();
        Object.Destroy(audioObject, clip.length);

        return audioSource;
    }

    public static GameObject PlayBGM(AudioClip clip, float volume = 0.2f)
    {
        GameObject audioObject = new GameObject("BG Music");
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();

        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.loop = true;

        audioSource.Play();

        return audioObject;
    }

    public static GameObject PlayBGMWithIntro(AudioClip introClip, AudioClip mainClip, float volume = 0.2f)
    {
        GameObject intoObject = new GameObject("BG Music Intro");
        GameObject mainObject = new GameObject("BG Music");
        AudioSource introSource = intoObject.AddComponent<AudioSource>();
        AudioSource mainSource = mainObject.AddComponent<AudioSource>();

        introSource.clip = introClip;
        introSource.volume = volume;

        mainSource.clip = mainClip;
        mainSource.volume = volume;
        mainSource.loop = true;

        BGM = mainObject;

        introSource.Play();
        mainSource.PlayDelayed(introClip.length);
        Object.Destroy(intoObject, introClip.length);
        return BGM;
    }
}
