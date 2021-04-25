using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashdownPlayerController : MonoBehaviour, IGameActor
{
    public Collider[] myColliders;

    public WeaponDefinition crashdownSmashWeapon;

    public float height = 1.0f;
    public float defaultMaxSpeed = 6.0f;
    public float defaultDodgeDuration = 0.5f;

    public float playerStartingHealth = 100.0f;
    public float playerHealthBoostMultiplier = 1.25f;
    public float playerDelayBeforeRegen = 4.0f;
    public float playerFullRegenWait = 6.0f;
    public float crashdownHealthDrainPerSecond = 10.0f;

    public Vector2 InputMovementThisFrame { get; set; }
    public bool InputAttackDownThisFrame { get; set; }
    public bool InputDodgeDownThisFrame { get; set; }
    public bool InputCrashdownDownThisFrame { get; set; }
    public bool InputInteractDownThisFrame { get; set; }
    public bool WasDamagedThisFrame { get; set; }

    public Vector3 CurrentFacing { get; set; }
    public float CurrentHealth { get; set; }
    public float CurrentHealthRegenDelay { get; set; }
    public float MaxHealth { get; set; }
    public float RemainingDodgeTime { get; set; }
    public float RemainingWeaponCooldown { get; set; }
    public bool HasCrashdownAttack { get; set; }

    public Vector3 CurrentAiming { get; set; }

    public static List<CrashdownPlayerController> activePlayerInstances = new List<CrashdownPlayerController>();

    private WeaponDefinition equippedWeapon = null;

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
        MaxHealth = playerStartingHealth;
        CurrentHealth = MaxHealth;
        CurrentHealthRegenDelay = 0.0f;
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
        return CurrentHealth <= 0.0f;
    }

    public bool TryGetCurrentWeapon(out WeaponDefinition weapon)
    {
        weapon = equippedWeapon;
        return weapon != null;
    }

    public void SetCurrentWeapon(WeaponDefinition weaponDefinition)
    {
        equippedWeapon = weaponDefinition;
    }

    public void UpdateFacingAndRenderer()
    {
        transform.LookAt(transform.position + CurrentFacing, Vector3.up);
        // TODO Keep renderer facing camera? Do we need that if we use a sprite renderer? 
        // No, since we can just use the billboard code to always face the sprite renderer to camera regardless of PlayerController facing.
    }

    public float GetMaxSpeed()
    {
        return defaultMaxSpeed;
    }

    public float GetDodgeSpeed()
    {
        return defaultMaxSpeed * 3.0f;
    }

    public float GetDodgeDuration()
    {
        return defaultDodgeDuration;
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

    public bool IsDodging()
    {
        return RemainingDodgeTime > 0.0f;
    }

    void IGameActor.TakeDamage(float damage, IGameActor attacker)
    {
        bool canSurviveOneMore = false;
        if (CurrentHealth > 1.0f)
        {
            canSurviveOneMore = true;
        }
        CurrentHealth -= damage;
        if (CurrentHealth < 0.0f && canSurviveOneMore)
        {
            CurrentHealth = 1.0f;
        }
        CurrentHealthRegenDelay = playerDelayBeforeRegen;
        WasDamagedThisFrame = true;
    }

    uint IGameActor.GetTribe()
    {
        return 255;
    }

}

