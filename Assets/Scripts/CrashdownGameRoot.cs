using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CrashdownGameRoot : MonoBehaviour
{
    public Projectile projectilePrefab;

    public Vector3 defaultCameraOffset = new Vector3(0.0f, 5.0f, 0.0f);
    public float defaultCameraAcceleration = 5.0f;
    public LayerMask terrainLayer;
    public LayerMask actorsLayer;
    public bool debugInput = false;
    public bool debugPhysics = false;
    public bool debugCombat = false;

    private Controls _controls;
    private Vector3 _currentCameraVelocity = Vector3.zero;
    private static uint currentProjectileCounter = 0;
    private static RaycastHit[] cachedRaycastHitArray = new RaycastHit[32];

    public static Dictionary<Collider, IGameActor> actorColliders = new Dictionary<Collider, IGameActor>();

    private void OnEnable()
    {
        if (_controls == null)
        {
            _controls = new Controls();
        }

        _controls.Player.Move.performed += OnMovementChanged;
        _controls.Player.Move.canceled += OnMovementChanged;
        _controls.Player.Move.Enable();
        _controls.Player.Attack.performed += OnAttackDown;
        _controls.Player.Attack.Enable();
        _controls.Player.Dodge.performed += OnDodgeDown;
        _controls.Player.Dodge.Enable();
        _controls.Player.Crashdown.performed += OnCrashdownDown;
        _controls.Player.Crashdown.Enable();
        _controls.Player.Interact.performed += OnInteractDown;
        _controls.Player.Interact.Enable();
    }

    private void OnDisable()
    {
        _controls.Player.Move.performed -= OnMovementChanged;
        _controls.Player.Move.canceled -= OnMovementChanged;
        _controls.Player.Move.Disable();
        _controls.Player.Attack.performed -= OnAttackDown;
        _controls.Player.Attack.Disable();
        _controls.Player.Dodge.performed -= OnDodgeDown;
        _controls.Player.Dodge.Disable();
        _controls.Player.Crashdown.performed -= OnCrashdownDown;
        _controls.Player.Crashdown.Disable();
        _controls.Player.Interact.performed -= OnInteractDown;
        _controls.Player.Interact.Disable();
    }

    private void OnMovementChanged(InputAction.CallbackContext context)
    {
        if (debugInput)
        {
            Debug.Log("OnMovementChanged " + context.ToString());
        }
        if (context.performed)
        {
            CrashdownPlayerController.activePlayerInstances[0].InputMovementThisFrame = context.ReadValue<Vector2>();
        }
        if (context.canceled)
        {
            CrashdownPlayerController.activePlayerInstances[0].InputMovementThisFrame = Vector2.zero;
        }
    }

    private void OnAttackDown(InputAction.CallbackContext context)
    {
        if (debugInput)
        {
            Debug.Log("OnAttackDown " + context.ToString());
        }
        CrashdownPlayerController.activePlayerInstances[0].InputAttackDownThisFrame = true;
    }

    private void OnDodgeDown(InputAction.CallbackContext context)
    {
        if (debugInput)
        {
            Debug.Log("OnDodgeDown " + context.ToString());
        }
        CrashdownPlayerController.activePlayerInstances[0].InputDodgeDownThisFrame = true;
    }

    private void OnCrashdownDown(InputAction.CallbackContext context)
    {
        if (debugInput)
        {
            Debug.Log("OnCrashdownDown " + context.ToString());
        }
        CrashdownPlayerController.activePlayerInstances[0].InputCrashdownDownThisFrame = true;
    }

    private void OnInteractDown(InputAction.CallbackContext context)
    {
        if (debugInput)
        {
            Debug.Log("OnInteractDown " + context.ToString());
        }
        CrashdownPlayerController.activePlayerInstances[0].InputInteractDownThisFrame = true;
    }

    void Update()
    {
        UpdatePlayers();
        UpdateEnemies();
        UpdateGameLogic();
    }

    private void UpdatePlayers()
    {
        Vector3 cameraAveragedTargetPosition = Vector3.zero;
        int numberOfCameraTargets = 0;

        // Flatten out inputs relative to the camera.
        Vector3 inputRight = Camera.main.transform.right;
        inputRight.y = 0.0f;
        inputRight = inputRight.normalized;
        Vector3 inputUp = Camera.main.transform.up;
        inputUp.y = 0.0f;
        inputUp = inputUp.normalized;

        foreach (CrashdownPlayerController player in CrashdownPlayerController.activePlayerInstances)
        {
            if (!player.IsDead())
            {
                bool debugPlayerIsWalkingAround = true;
                if (debugPlayerIsWalkingAround)
                {
                    Vector2 input = player.InputMovementThisFrame;
                    // Don't make diagonal walking any faster.
                    if (input.sqrMagnitude > 1.0f)
                    {
                        input = input.normalized;
                    }

                    Vector3 worldspaceInput = inputRight * input.x + inputUp * input.y;
                    if (worldspaceInput != Vector3.zero)
                    {
                        player.CurrentFacing = worldspaceInput.normalized;
                    }

                    // Move on the X and Z axes separately so the player can slide along walls.
                    for (int i = 0; i < 2; i++)
                    {
                        Vector3 newPosition;
                        switch (i)
                        {
                            case 0:
                                newPosition = player.transform.position + player.GetMaxSpeed() * Time.deltaTime * new Vector3(worldspaceInput.x, 0.0f, 0.0f);
                                break;
                            default:
                                newPosition = player.transform.position + player.GetMaxSpeed() * Time.deltaTime * new Vector3(0.0f, 0.0f, worldspaceInput.z);
                                break;
                        }

                        if (Physics.Raycast(newPosition, Vector3.down, out RaycastHit floorHit, player.height, terrainLayer.value))
                        {
                            newPosition = floorHit.point + Vector3.up * (player.height / 2.0f);
                            player.transform.position = newPosition;
                            if (debugPhysics)
                            {
                                Debug.Log("Player " + player.name + " is walking on " + floorHit.collider.gameObject.name + " and moved to " + newPosition, floorHit.collider.gameObject);
                            }
                        }
                        else
                        {
                            // Player tried to walk off an edge, so they should stop and not move there.
                            if (debugPhysics)
                            {
                                Debug.Log("Player " + player.name + " tried to walk off an edge.", player.gameObject);
                            }
                        }
                    }

                    player.UpdateFacingAndRenderer();

                    // Player Attacks
                    if (player.InputAttackDownThisFrame && player.TryGetCurrentWeapon(out WeaponDefinition weapon))
                    {
                        // TODO Cooldowns and so on.
                        ActorUsesWeapon(player, weapon, projectilePrefab);
                    }

                    // Player Dodges

                    // Player Crashdown
                }

                cameraAveragedTargetPosition += player.transform.position;
                numberOfCameraTargets++;
            }
            player.InputAttackDownThisFrame = false;
            player.InputDodgeDownThisFrame = false;
            player.InputCrashdownDownThisFrame = false;
            player.InputInteractDownThisFrame = false;
        }

        // Update the camera after all the players have moved.
        if (numberOfCameraTargets > 0)
        {
            cameraAveragedTargetPosition /= numberOfCameraTargets;
            Vector3 cameraNewPosition = cameraAveragedTargetPosition + defaultCameraOffset;
            cameraNewPosition = Vector3.SmoothDamp(Camera.main.transform.position, cameraNewPosition, ref _currentCameraVelocity, 1.0f / defaultCameraAcceleration);
            Camera.main.transform.position = cameraNewPosition;
            Camera.main.transform.LookAt(cameraAveragedTargetPosition, Vector3.forward);
        }
    }

    private void UpdateEnemies()
    {
    }

    private void UpdateGameLogic()
    {
        for (int i = 0; i < Projectile.activeProjectiles.Count; i++)
        {
            Projectile currentProjectile = Projectile.activeProjectiles[i];
            if (debugCombat)
            {
                Debug.Log("Updating projectile " + currentProjectile.MyId);
            }
            bool shouldDespawn = false;
            if (currentProjectile.IsLifetimeOver())
            {
                shouldDespawn = true;
            }
            else
            {
                float distanceToMoveThisFrame = currentProjectile.GetSpeed() * Time.deltaTime;
                int numberOfHits = Physics.SphereCastNonAlloc(currentProjectile.transform.position, currentProjectile.MyWeaponData.radius, currentProjectile.transform.forward, cachedRaycastHitArray, distanceToMoveThisFrame, actorsLayer.value);
                for (int h = 0; h < numberOfHits; h++)
                {
                    RaycastHit currentHit = cachedRaycastHitArray[h];
                    if (debugCombat)
                    {
                        Debug.Log("Projectile hit on " + currentHit.collider.gameObject.name, currentHit.collider.gameObject);
                    }
                    Collider hitCollider = currentHit.collider;
                    if (actorColliders.TryGetValue(hitCollider, out IGameActor touchedActor))
                    {
                        bool canProjectileHitActor = currentProjectile.CanHitActor(touchedActor);
                        if (canProjectileHitActor)
                        {
                            touchedActor.TakeDamage(currentProjectile.MyWeaponData.damage, currentProjectile.MyOwner);
                            currentProjectile.AddHitToActor(touchedActor);
                        }
                    }
                    else
                    {
                        // Recently destroyed colliders might still take raycasts if we're not using DestroyImmediate, so just ignore them.
                    }
                }

                currentProjectile.transform.position += currentProjectile.transform.forward * distanceToMoveThisFrame;
                currentProjectile.RemainingLifetime -= Time.deltaTime;
            }
            if (shouldDespawn)
            {
                GameObject.Destroy(currentProjectile.gameObject);
                // This calls OnDisable and alters the projectile list.
                i--;
            }
        }
    }

    private void ActorUsesWeapon(IGameActor actor, WeaponDefinition weapon, Projectile projectilePrefab)
    {
        if (actor != null && weapon != null && weapon.numberToSpawn > 0)
        {
            Quaternion rotationPerShot = Quaternion.identity;
            Quaternion startRotation = actor.GetRotation();
            if (weapon.numberToSpawn > 1)
            {
                float totalAngle = (weapon.numberToSpawn - 1) * weapon.spreadBetweenShotsDegrees;
                float startAngle = totalAngle / 2.0f;
                startRotation = startRotation * Quaternion.Euler(0.0f, startAngle, 0.0f);
                rotationPerShot = Quaternion.Euler(0.0f, -weapon.spreadBetweenShotsDegrees, 0.0f);
            }
            Quaternion currentRotation = startRotation;
            Vector3 spawnOffset = Vector3.forward * weapon.startDistance;
            Vector3 actorCenter = actor.GetPosition();
            for (int i = 0; i < weapon.numberToSpawn; i++)
            {
                Vector3 spawnLocation = currentRotation * spawnOffset + actorCenter;
                Projectile.Spawn(projectilePrefab, weapon, actor, spawnLocation, currentRotation, currentProjectileCounter);
                if (debugCombat)
                {
                    Debug.Log("Actor fired a projectile at " + spawnLocation + " with angle " + currentRotation.eulerAngles.y);
                }
                currentRotation = currentRotation * rotationPerShot;
                if (!weapon.treatProjectilesAsOneWave)
                {
                    currentProjectileCounter++;
                }
            }
            currentProjectileCounter++;
        }
    }

}
