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

    public static List<Projectile> activeProjectiles = new List<Projectile>();

    private void OnEnable()
    {
        activeProjectiles.Add(this);
    }

    private void OnDisable()
    {
        activeProjectiles.Remove(this);
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
}
