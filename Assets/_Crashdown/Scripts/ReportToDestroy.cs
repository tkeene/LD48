using UnityEngine;

public class ReportToDestroy : MonoBehaviour
{
    private void OnEnable()
    {
        CrashdownGameRoot.DisposeOnLevelChange.Add(gameObject);
    }
}
