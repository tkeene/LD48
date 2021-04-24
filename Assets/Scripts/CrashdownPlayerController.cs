using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashdownPlayerController : MonoBehaviour
{
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
        return 6.0f;
    }
}

