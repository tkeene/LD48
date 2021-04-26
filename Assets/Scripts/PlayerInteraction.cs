using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Collider myPlayerCollider;
    public enum EInteractionType
    {
        HealthPowerUp = 0,
        WeaponPickup = 1,
        DodgePowerUp = 2,
        ToggleSomething = 3,
        CrashdownKey = 4,
        Nothing = 5,
        WinTheGame = 101,
    }
    public EInteractionType interactionType;
    public WeaponDefinition weaponDefinition;
    public GameObject[] objectsToToggle;
    public bool removeAfterActivation = false;
    public GameObject verbLabelRoot;
    public int victorySceneIndex = 4;

    public static Dictionary<Collider, PlayerInteraction> activeInteractions = new Dictionary<Collider, PlayerInteraction>();

    private float timeSinceLastPlayerTouch = float.PositiveInfinity;


    private void OnEnable()
    {
        verbLabelRoot.gameObject.SetActive(false);
        timeSinceLastPlayerTouch = float.PositiveInfinity;
        activeInteractions.Add(myPlayerCollider, this);
    }

    private void OnDisable()
    {
        activeInteractions.Remove(myPlayerCollider);
    }

    private void Update()
    {
        if (timeSinceLastPlayerTouch < 0.25f)
        {
            timeSinceLastPlayerTouch += Time.deltaTime;
            verbLabelRoot.SetActive(true);
        }
        else
        {
            verbLabelRoot.SetActive(false);
        }
    }

    public void OnPlayerStaysThisFrame()
    {
        timeSinceLastPlayerTouch = 0.0f;
    }
}
