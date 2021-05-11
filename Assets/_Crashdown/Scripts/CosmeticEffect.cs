using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticEffect : MonoBehaviour
{
    public float defaultLifetime = 0.0f;

    private Transform transformToFollow = null;
    private float remainingLifetime = 0.0f;

    public static CosmeticEffect Spawn(CosmeticEffect prefab, float lifetime, Vector3 position, Quaternion rotation, Transform transformToFollow = null)
    {
        CosmeticEffect spawnedEffect = GameObject.Instantiate<CosmeticEffect>(prefab, position, rotation);
        spawnedEffect.remainingLifetime = lifetime;
        spawnedEffect.transformToFollow = transformToFollow;
        return spawnedEffect;
    }

    public void Despawn()
    {
        remainingLifetime = float.NegativeInfinity;
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
        }
        else
        {
            GameObject.Destroy(gameObject);
        }
    }

    private void LateUpdate()
    {
        if (transformToFollow != null)
        {
            transform.position = transformToFollow.position;
        }
    }
}
