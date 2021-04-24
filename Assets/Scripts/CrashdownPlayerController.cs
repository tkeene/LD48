using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashdownPlayerController : MonoBehaviour, IGameActor
{
    public Collider[] myColliders;
    [SerializeField]
    protected WeaponDefinition debugStarterWeapon = null;

    public float height = 1.0f;
    public float defaultMaxSpeed = 6.0f;

    public Vector2 InputMovementThisFrame { get; set; }
    public bool InputAttackDownThisFrame { get; set; }
    public bool InputDodgeDownThisFrame { get; set; }
    public bool InputCrashdownDownThisFrame { get; set; }
    public bool InputInteractDownThisFrame { get; set; }

    public Vector3 CurrentFacing { get; set; }

    public static List<CrashdownPlayerController> activePlayerInstances = new List<CrashdownPlayerController>();

    // Nope, use CrashdownGameRoot.UpdatePlayers()
    //private void Update()
    //{
    //    
    //}

    private void OnEnable()
    {
        activePlayerInstances.Add(this);
        CurrentFacing = Vector3.back;
        foreach (Collider collider in myColliders)
        {
            CrashdownGameRoot.actorColliders[collider] = this;
        }
    }

    private void OnDisable()
    {
        activePlayerInstances.Remove(this);
        foreach (Collider collider in myColliders)
        {
            CrashdownGameRoot.actorColliders.Remove(collider);
        }
    }

    public bool IsDead()
    {
        return false;
    }

    public bool TryGetCurrentWeapon(out WeaponDefinition weapon)
    {
        weapon = null;
        // TODO
        weapon = debugStarterWeapon;
        return weapon != null;
    }

    public void UpdateFacingAndRenderer()
    {
        transform.LookAt(transform.position + CurrentFacing, Vector3.up);
        // TODO Keep renderer facing camera? Do we need that if we use a sprite renderer?
    }

    public float GetMaxSpeed()
    {
        return defaultMaxSpeed;
    }

    Vector3 IGameActor.GetFacing()
    {
        return CurrentFacing;
    }

    Vector3 IGameActor.GetPosition()
    {
        return transform.position;
    }

    Quaternion IGameActor.GetRotation()
    {
        return transform.rotation;
    }

    void IGameActor.TakeDamage(float damage, IGameActor attacker)
    {
        Debug.LogError("TODO: Player took " + damage + " damage.");
    }
}

