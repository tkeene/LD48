using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashdownPlayerController : MonoBehaviour
{
    public float height = 1.0f;
    public float defaultMaxSpeed = 6.0f;

    public static List<CrashdownPlayerController> activePlayerInstances = new List<CrashdownPlayerController>();

    // Nope, use CrashdownGameRoot.UpdatePlayers()
    //private void Update()
    //{
    //    
    //}

    private void OnEnable()
    {
        activePlayerInstances.Add(this);
    }

    private void OnDisable()
    {
        activePlayerInstances.Remove(this);
    }

    public bool IsDead()
    {
        return false;
    }

    public float GetMaxSpeed()
    {
        return defaultMaxSpeed;
    }
}

