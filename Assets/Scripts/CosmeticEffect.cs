using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticEffect : MonoBehaviour
{
    public float defaultLifetime = 0.0f;

    private Transform transformToFollow = null;
    private float remainingLifetime = 0.0f;

    public static void Spawn(CosmeticEffect prefab, float lifetime, Vector3 position, Quaternion rotation, Transform transformToFollow = null)
    {
        CosmeticEffect effect = GameObject.Instantiate<CosmeticEffect>(prefab);
        prefab.transform.position = position;
        prefab.transform.rotation = rotation;
        prefab.remainingLifetime = lifetime;
        prefab.transformToFollow = transformToFollow;
    }

    private void OnEnable()
    {
        if (remainingLifetime == 0.0f && defaultLifetime > 0.0f)
        {
            remainingLifetime = defaultLifetime;
        }
    }

    private void Update()
    {
        if (remainingLifetime > 0.0f)
        {
            remainingLifetime -= Time.deltaTime;
            if (transformToFollow != null)
            {
                transform.position = transformToFollow.position;
            }
        }
        else
        {
            GameObject.Destroy(this);
        }
    }

}
