using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameActor
{
    Vector3 GetFacing();
    Vector3 GetPosition();
    Quaternion GetRotation();
}
