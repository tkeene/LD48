using UnityEngine;

public class RingBilboard : MonoBehaviour
{
    public CrashdownPlayerController playerController;

    private void LateUpdate()
    {
        transform.rotation = Camera.main.transform.rotation;

        Vector3 desiredDirection = playerController.CurrentAiming;
        Vector2 direction2 = new Vector2(desiredDirection.x, desiredDirection.z);
        float angle = Vector2.SignedAngle(Vector2.up, direction2);
        Quaternion rotation  = Quaternion.AngleAxis(angle, transform.up);
        
        transform.rotation *= rotation;
    }
}
