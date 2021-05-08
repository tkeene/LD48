using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource defaultAudioPrefab;

    public static AudioManager instance = null;

    private List<SoundEffectData> remainingRepeatPreventionData = new List<SoundEffectData>();
    private List<float> remainingRepeatPreventionTimes = new List<float>();

    private void Awake()
    {
        instance = this;
    }

    private void LateUpdate()
    {
        for (int i = 0; i < remainingRepeatPreventionData.Count; i++)
        {
            if (remainingRepeatPreventionTimes[i] >= 0)
            {
                remainingRepeatPreventionTimes[i] -= Time.unscaledDeltaTime;
            }
            else
            {
                remainingRepeatPreventionTimes.RemoveAt(i);
                remainingRepeatPreventionData.RemoveAt(i);
                i--;
            }
        }
    }

    public void PlaySound(SoundEffectData soundToPlay, Vector3 position)
    {
        if (!remainingRepeatPreventionData.Contains(soundToPlay))
        {
            float pitch = 1.0f + UnityEngine.Random.Range(-soundToPlay.pitchVariation, soundToPlay.pitchVariation);
            PlaySound(soundToPlay.soundEffect, position, soundToPlay.volume, pitch);
            remainingRepeatPreventionData.Add(soundToPlay);
            remainingRepeatPreventionTimes.Add(soundToPlay.timeBetweenMultipleSounds);
        }
    }

    public void PlaySound(AudioClip clip, Vector3 position, float volume = 1.0f, float pitchVariation = 1.0f)
    {
        AudioSource spawnedAudioSource = GameObject.Instantiate<AudioSource>(defaultAudioPrefab);
        DontDestroyOnLoad(spawnedAudioSource);
        spawnedAudioSource.transform.position = position;
        spawnedAudioSource.clip = clip;
        spawnedAudioSource.volume = volume;
        spawnedAudioSource.pitch = pitchVariation;
        spawnedAudioSource.PlayOneShot(clip);
    }

}
