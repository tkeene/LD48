using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioLowPassFilter lowPassFilter;
    public float minimumFilter = 200;
    public float maximumFilter = 22000;

    public static MusicManager instance = null;

    private void Awake()
    {
        instance = this;
    }

    internal void SetFilterAmount(float v)
    {
        v = v * v; // Poor man's control curve, make it only kick in heavily at low health.
        lowPassFilter.cutoffFrequency = Mathf.Lerp(maximumFilter, minimumFilter, v);
    }
}
