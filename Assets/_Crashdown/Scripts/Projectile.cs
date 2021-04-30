using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public WeaponDefinition MyWeaponData { get; set; }
    public IGameActor MyOwner { get; set; }
    public float RemainingLifetime { get; set; }
    public int RemainingHits { get; set; }
    public uint MyId { get; set; }
    public GameObject Renderer;

    public static List<Projectile> activeProjectiles = new List<Projectile>();

    private static Dictionary<uint, List<IGameActor>> actorsHitbyProjectile = new Dictionary<uint, List<IGameActor>>();

    private void OnEnable()
    {
        activeProjectiles.Add(this);
    }

    private void OnDisable()
    {
        activeProjectiles.Remove(this);
        actorsHitbyProjectile.Remove(MyId);
    }

    public static void Spawn(Projectile prefab, WeaponDefinition weapon, IGameActor owner, Vector3 position, Quaternion rotation, uint projectileId)
    {
        Projectile spawnedProjectile = GameObject.Instantiate<Projectile>(prefab, position, rotation);
        spawnedProjectile.Initialize(weapon, owner, projectileId);
    }

    private void Initialize(WeaponDefinition weapon, IGameActor owner, uint projectileId)
    {
        MyWeaponData = weapon;
        MyOwner = owner;
        RemainingLifetime = weapon.lifetime;
        RemainingHits = weapon.maximumEnemiesHit;
        MyId = projectileId;
        Renderer.SetActive(!weapon.hiddenAttack);
        // Don't spawn the actorsHitByProjectile array until it actually hits something.
    }

    public bool IsLifetimeOver()
    {
        return RemainingLifetime <= 0.0f || RemainingHits <= 0;
    }

    public float GetSpeed()
    {
        float speed = 1.0f;
        if (MyWeaponData != null)
        {
            speed = MyWeaponData.moveSpeed;
        }
        return speed;
    }

    public bool CanHitActor(IGameActor touchedActor)
    {
        bool canHitActor = true;
        if (touchedActor == null)
        {
            canHitActor = false;
        }
        else if (touchedActor.IsDodging())
        {
            canHitActor = false;
        }
        else if (MyOwner != null && touchedActor.GetTribe() == MyOwner.GetTribe())
        {
            canHitActor = false;
        }
        else if (actorsHitbyProjectile.TryGetValue(MyId, out List<IGameActor> actors)
            && actors.Contains(touchedActor))
        {
            // Don't let a bullet hit the same character multiple times.
            canHitActor = false;
        }
        return canHitActor;
    }

    public void AddHitToActor(IGameActor touchedActor)
    {
        if (touchedActor != null)
        {
            if (!actorsHitbyProjectile.ContainsKey(MyId))
            {
                actorsHitbyProjectile[MyId] = new List<IGameActor>();
            }
            if (!actorsHitbyProjectile[MyId].Contains(touchedActor))
            {
                actorsHitbyProjectile[MyId].Add(touchedActor);
            }
            RemainingHits--;
        }
    }
}
