using System;
using UnityEngine;

public class Foe : MonoBehaviour
{
    [SerializeField] private DestructibleEntity _destructibleEntity;

    private void Awake()
    {
        _destructibleEntity.OnDeath += GiveReward;
    }

    private void GiveReward()
    {
        if (GameplayManager.Instance.TryGetPickupOnDeath(out var pickup))
            Instantiate(pickup, transform.position, Quaternion.identity);
    }
}
