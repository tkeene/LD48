using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnInWebGlVersion : MonoBehaviour
{
    void Start()
    {
#if UNITY_WEBGL
        this.gameObject.SetActive(false);
#endif
    }
}
