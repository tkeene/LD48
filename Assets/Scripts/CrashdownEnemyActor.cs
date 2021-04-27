using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashdownEnemyActor : MonoBehaviour, IGameActor
{
    public Collider[] myColliders;
    public WeaponDefinition[] weaponsCycle;
    public List<CrashdownEnemyActor> friendsToNotify;
    public GameObject[] toSpawnWhenKoed;
    public float aggroRadius = 15.0f;
    public float maximumRandomAttackDelay = 1.0f;
    public float height = 1.0f;
    public float moveSpeed = 3.0f;
    public bool ignoresTerrain = false;
    public float maximumHealth = 40.0f;
    public uint tribeNumber = 0;
    public float enrageDuration = 1.0f;
    public float enrageSpeedBonus = 0.7f;
    public float enrageWeaponCooldownMultiplier = 2.0f;
    public float enrageSidewaysStaggerSpeed = 3.0f;

    public float deathTime = 1f;

    [HideInInspector]
    public float timeDying = 0f;

    [HideInInspector]
    public bool movedThisFrame = false;

    [HideInInspector]
    public bool firedThisFrame = false;

    public static List<CrashdownEnemyActor> activeEnemies = new List<CrashdownEnemyActor>();

    public enum EAiType
    {
        InanimateObject = 0,
        Stationary = 1,
        RunAtTheKnees = 2,
        OneTimeEnemySpawner = 3,
    }
    public EAiType aiType = EAiType.InanimateObject;

    public Vector3 CurrentFacing { get; set; }
    public Vector3 CurrentMoving { get; set; }

    public enum EAiState
    {
        JustSpawned,
        WalkingAndFighting,
        Dying,
        IsDead,
    }
    public EAiState CurrentAiState { get; set; }
    public IGameActor CurrentAggroTarget { get; set; }
    public float RemainingCooldownTime { get; set; }
    public float CurrentHealth { get; set; }
    public float RemainingEnrageDuration { get; set; }
    public float CurrentSidewaysStaggerAmount { get; set; }

    private int currentAttack = 0;

    private void OnEnable()
    {
        if (aiType != EAiType.InanimateObject)
        {
            // This is a hack to fix a bug where an enemy that spawns precisely inside a floor (like if you drag and drop a spawner into the scene view) will not be able to seek down and find the floor.
            transform.position += Vector3.up * 0.02f;
        }
        activeEnemies.Add(this);
        foreach (Collider collider in myColliders)
        {
            CrashdownGameRoot.actorColliders[collider] = this;
        }
        CurrentAiState = EAiState.JustSpawned;
        CurrentHealth = maximumHealth;
    }

    private void OnDisable()
    {
        activeEnemies.Remove(this);
        foreach (Collider collider in myColliders)
        {
            CrashdownGameRoot.actorColliders.Remove(collider);
        }
    }

    public void UpdateFacingAndRenderer()
    {
        transform.LookAt(transform.position + CurrentFacing, Vector3.up);
        // TODO Keep renderer facing camera? Do we need that if we use a sprite renderer?
    }

    public void MoveTo(Vector3 newPosition)
    {
        var dif = newPosition - transform.position;

        if (dif.magnitude >= .005f)
        {
            movedThisFrame = true;
        }

        transform.position = newPosition;
    }

    public bool CanAttack()
    {
        return RemainingCooldownTime <= 0.0f;
    }

    public bool IsEnraged()
    {
        return RemainingEnrageDuration > 0f;
    }

    public bool TryGetCurrentAttack(out WeaponDefinition attack)
    {
        attack = null;
        if (currentAttack >= 0 && currentAttack < weaponsCycle.Length)
        {
            attack = weaponsCycle[currentAttack];
        }
        return attack != null;
    }

    public void AdvanceToNextAttack()
    {
        currentAttack++;
        if (currentAttack >= weaponsCycle.Length)
        {
            currentAttack = 0;
        }
    }

    public void ClearFlags()
    {
        movedThisFrame = false;
        firedThisFrame = false;
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

    bool IGameActor.IsDodging()
    {
        return !(CurrentAiState == EAiState.JustSpawned || CurrentAiState == EAiState.WalkingAndFighting);
    }

    void IGameActor.TakeDamage(float damage, IGameActor attacker)
    {
        // TODO Loot.
        CurrentHealth -= damage;
        if (CurrentHealth <= 0.0f && CurrentAiState == EAiState.WalkingAndFighting)
        {
            CurrentAiState = EAiState.Dying;
        }
        if (CurrentAggroTarget == null)
        {
            CurrentAggroTarget = attacker;
        }
        RemainingEnrageDuration = enrageDuration;
        float staggerSign = Mathf.Sign(UnityEngine.Random.Range(-1.0f, 1.0f));
        CurrentSidewaysStaggerAmount = enrageSidewaysStaggerSpeed * staggerSign;
    }

    uint IGameActor.GetTribe()
    {
        return tribeNumber;
    }

    public float GetMoveSpeed()
    {
        float speed = moveSpeed;
        if (RemainingEnrageDuration > 0.0f)
        {
            speed += enrageSpeedBonus;
        }
        return speed;
    }
}
