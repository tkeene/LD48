using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashdownEnemyActor : MonoBehaviour, IGameActor
{
    public Collider[] myColliders;
    public enum EAiType
    {
        InanimateObject = 0,
    }
    EAiType aiType = EAiType.InanimateObject;

    public Vector3 CurrentFacing { get; set; }

    public enum EAiState
    {
        JustSpawned,
        WalkingAndFighting,
        Dying,
        IsDead,
    }
    public EAiState CurrentAiState { get; set; }

    private void OnEnable()
    {
        foreach (Collider collider in myColliders)
        {
            CrashdownGameRoot.actorColliders[collider] = this;
        }
        CurrentAiState = EAiState.JustSpawned;
    }

    private void OnDisable()
    {
        foreach (Collider collider in myColliders)
        {
            CrashdownGameRoot.actorColliders.Remove(collider);
        }
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
        return false;
    }

    void IGameActor.TakeDamage(float damage, IGameActor attacker)
    {
        // TODO Dying animation.
        // TODO Loot.
        GameObject.Destroy(gameObject);
    }
}
