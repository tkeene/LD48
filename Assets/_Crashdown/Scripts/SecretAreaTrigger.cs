using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretAreaTrigger : MonoBehaviour
{
    public Collider myCollider;

    public static Dictionary<Collider, SecretAreaTrigger> activeSecretAreas = new Dictionary<Collider, SecretAreaTrigger>();

    private void OnEnable()
    {
        activeSecretAreas.Add(myCollider, this);
    }

    private void OnDisable()
    {
        activeSecretAreas.Remove(myCollider);
    }
}
