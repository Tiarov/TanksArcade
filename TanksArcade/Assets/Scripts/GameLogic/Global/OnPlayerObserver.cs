using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlayerObserver : MonoBehaviour
{
    private Transform _target;

    private void Awake()
    {
        EventManager.DeathEvent += OnDeath;
        EventManager.PlayerCharacterCreatedEvent += OnPlayerCharacterCreated;
        EventManager.MonstersTargetRequestEvent += OnRequest;
    }

    private void OnDestroy()
    {
        EventManager.PlayerCharacterCreatedEvent -= OnPlayerCharacterCreated;
        EventManager.MonstersTargetRequestEvent -= OnRequest;
    }

    private void OnPlayerCharacterCreated(Transform target)
    {
        _target = target;
        EventManager.PublicityOfTargetEvent(_target);
    }

    private void OnRequest()
    {
        if (_target)
            EventManager.PublicityOfTargetEvent(_target);
    }

    private void OnDeath(Transform target)
    {
        if (_target != null && target == _target)
            EventManager.PlayerCharacterDeath(target);
    }
}
