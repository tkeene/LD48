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

    private void OnEnable()
    {
        foreach (Collider collider in myColliders)
        {
            CrashdownGameRoot.actorColliders[collider] = this;
        }
    }

    private void OnDisable()
    {
        foreach (Collider collider in myColliders)
        {
            CrashdownGameRoot.actorColliders.Remove(collider);
        }
    }

}
