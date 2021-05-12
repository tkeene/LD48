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
    public TMPro.TextMeshProUGUI labelText;
    public SpriteRenderer itemDisplay;
    [UnityEngine.Serialization.FormerlySerializedAs("victorySceneIndex")]
    public int targetSceneIndex = 0;

    [HideInInspector]
    public bool interactedWithThisFrame = false;

    [HideInInspector]
    public float interactionCoolDown = 0f;

    public static Dictionary<Collider, PlayerInteraction> activeInteractions = new Dictionary<Collider, PlayerInteraction>();

    private float timeSinceLastPlayerTouch = float.PositiveInfinity;


    private void OnEnable()
    {
        if (verbLabelRoot.gameObject != null)
        {
            verbLabelRoot.gameObject.SetActive(false);
        }
        if (labelText != null && weaponDefinition != null)
        {
            labelText.text = string.Format(weaponDefinition.pickupMessage, weaponDefinition.pickupName);
        }
        if (itemDisplay != null && weaponDefinition?.pickupAndHudSprite != null)
        {
            itemDisplay.sprite = weaponDefinition?.pickupAndHudSprite;
        }

        timeSinceLastPlayerTouch = float.PositiveInfinity;
        interactionCoolDown = 0f;

        activeInteractions.Add(myPlayerCollider, this);
    }

    private void OnDisable()
    {
        activeInteractions.Remove(myPlayerCollider);
    }

    private void Update()
    {
        if (interactionCoolDown > 0f)
        {
            interactionCoolDown -= Time.deltaTime;
        }

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

    public void ClearFlags()
    {
        interactedWithThisFrame = false;
    }
}
