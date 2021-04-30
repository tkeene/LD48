using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnEnable : MonoBehaviour
{
    public SoundEffectData effectToPlay;

    private void OnEnable()
    {
        AudioManager.instance.PlaySound(effectToPlay, transform.position);
    }
}
