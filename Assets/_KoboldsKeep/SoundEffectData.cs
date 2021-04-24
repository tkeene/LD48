using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/KoboldsKeep/Spawn SoundEffectData", order = 1)]
public class SoundEffectData : ScriptableObject
{
    public AudioClip soundEffect;
    public float volume = 1.0f;
    public float pitchVariation = 0.1f;
    public float timeBetweenMultipleSounds = 0.25f;
}
