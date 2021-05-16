using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateRandomChildOnSceneLoad : MonoBehaviour
{
    private void Start()
    {
        List<Transform> childTransforms = new List<Transform>();
        this.transform.GetComponentsInChildren<Transform>(includeInactive: true, childTransforms);
        childTransforms.Remove(this.transform);
        int childrenCount = childTransforms.Count;

        if (childrenCount > 0)
        {
            int indexToActivate = UnityEngine.Random.Range(0, childrenCount);
            for (int i = 0; i < childrenCount; i++)
            {
                bool shouldActivate = i == indexToActivate;
                childTransforms[i].gameObject.SetActive(shouldActivate);
            }
        }
    }
}
