using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Weapon Definition", order = 1)]
public class WeaponDefinition : ScriptableObject
{
    public CosmeticEffect actorEffectOnFiring;
    public CosmeticEffect projectileLockedEffect;
    public CosmeticEffect onFinalHitEffect;

    public float startDistance = 0.5f;
    public float radius = 1.0f;
    public float moveSpeed = 0.1f;
    public float lifetime = 1.0f;
    public float cooldown = 0.2f;
    public float damage = 10.0f;
    public int maximumEnemiesHit = 128;
    public int numberToSpawn = 1;
    public bool treatProjectilesAsOneWave = false;
    public float spreadBetweenShotsDegrees = 5.0f;
    public bool hiddenAttack = false;
}
