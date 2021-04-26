using UnityEngine;

public class FixedSpawn : MonoBehaviour
{
    public float x;
    public float z;

    // Start is called before the first frame update
    void OnEnable()
    {
        Vector3 t = transform.position;
        t.x = x;
        t.z = z;
        transform.position = t;
        transform.rotation = Quaternion.identity;
    }
    
}
