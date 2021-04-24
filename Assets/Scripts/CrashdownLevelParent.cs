using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashdownLevelParent : MonoBehaviour
{
    public static SortedList<float, CrashdownLevelParent> activeCrashdownLevels = new SortedList<float, CrashdownLevelParent>();

    public static float kExpectedDistanceBetweenFloors = 10.0f;


    private void OnEnable()
    {
        activeCrashdownLevels.Add(this.transform.position.y, this);
    }

    private void OnDisable()
    {
        if (activeCrashdownLevels.ContainsValue(this))
        {
            Debug.LogError("Error: CrashdownLevelParent was destroyed before it could play an awesome animation.");
            activeCrashdownLevels.RemoveAt(activeCrashdownLevels.IndexOfValue(this));
        }
    }
}
