using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashdownLevelParent : MonoBehaviour
{
    public AudioClip myMusic;
    public float myMusicSpeed = 1.0f;
    public float defaultLowpassLevel = 1.0f;

    public static SortedList<float, CrashdownLevelParent> activeCrashdownLevels = new SortedList<float, CrashdownLevelParent>();

    public static float kExpectedDistanceBetweenFloors = 10.0f;


    private void OnEnable()
    {
        float sortedListPosition = -this.transform.position.y;
        activeCrashdownLevels.Add(sortedListPosition, this);
    }

    private void OnDisable()
    {
        if (activeCrashdownLevels.ContainsValue(this))
        {
            activeCrashdownLevels.RemoveAt(activeCrashdownLevels.IndexOfValue(this));
        }
    }

    public void Dispose()
    {
        gameObject.SetActive(false);
    }
}
