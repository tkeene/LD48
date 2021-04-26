using UnityEngine;

public class EffectBilboard : MonoBehaviour
{
    public GameObject parent;

    private void LateUpdate()
    {
        transform.rotation = Camera.main.transform.rotation;

        Vector3 desiredDirection = parent.transform.forward;
        Vector2 direction2 = new Vector2(desiredDirection.x, desiredDirection.z);
        float angle = Vector2.SignedAngle(Vector2.up, direction2);
        Quaternion rotation = Quaternion.AngleAxis(angle + 90, transform.up);

        transform.rotation *= rotation;
    }
}
