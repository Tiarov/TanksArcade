using System;
using UnityEngine;

public class EventManager
{
    public static Action<int[,]> MapCreatedEvent;
    public static Action<Transform> PlayerCharacterCreatedEvent;
    public static Action<Transform> PlayerCharacterDeathEvent;
    public static Action<Transform> MonsterDeathEvent;
    public static Action<Transform, float> DamageEvent;
    public static Action<Transform, float> HealEvent;
    public static Action<Transform> DeathEvent;
    public static Action<Vector3, float> BoomEvent;
    public static Action<Vector3> HitEvent;
    public static Action MonstersTargetRequestEvent;
    public static Action<Transform> PublicityOfTargetEvent;

    public static void MapCreated(int[,] array)
    {
        if (MapCreatedEvent != null)
            MapCreatedEvent(array);
    }

    public static void Damage(Transform target, float damage)
    {
        if (DamageEvent != null)
            DamageEvent(target, damage);
    }

    public static void Heal(Transform target, float damage)
    {
        if (HealEvent != null)
            HealEvent(target, damage);
    }

    public static void Death(Transform sender)
    {
        if (DeathEvent != null)
            DeathEvent(sender);
    }
    
    public static void Boom(Vector3 position, float volume)
    {
        if (BoomEvent != null)
            BoomEvent(position, volume);
    }

    public static void Hit(Vector3 position)
    {
        if (HitEvent != null)
            HitEvent(position);
    }

    public static void MonstersTargetRequest()
    {
        if (MonstersTargetRequestEvent != null)
            MonstersTargetRequestEvent();
    }

    public static void PublicityOfTarget(Transform target)
    {
        if (PublicityOfTargetEvent != null)
            PublicityOfTargetEvent(target);
    }

    public static void PlayerCharacterCreated(Transform player)
    {
        if (PlayerCharacterCreatedEvent != null)
            PlayerCharacterCreatedEvent(player);
    }

    public static void PlayerCharacterDeath(Transform player)
    {
        if (PlayerCharacterDeathEvent != null)
            PlayerCharacterDeathEvent(player);
    }
}
