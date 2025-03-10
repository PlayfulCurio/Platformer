using System;
using UnityEngine;

public class GameplayManager : MonoBehaviourSingleton<GameplayManager>
{
    [SerializeField] private GameObject[] _pickups;

    private float _spawnPickupChance = .05f;
    private float _pickupChanceIncreaseEverySecond;

    public int Kills { get; private set; }
    public int Hits { get; private set; }
    public int Upgrades { get; private set; }
    public int Score => Kills + Upgrades - Hits;

    public event Action<bool> OnGameOver;
    public event Action<float> OnPlayerHealthChanged;

    private void FixedUpdate()
    {
        _spawnPickupChance += _pickupChanceIncreaseEverySecond / Time.fixedDeltaTime;
    }

    public void EndGame(bool didPlayerWin)
    {
        OnGameOver?.Invoke(didPlayerWin);
    }

    public bool TryGetPickupOnDeath(out GameObject pickup)
    {
        Kills++;
        if (UnityEngine.Random.value <= _spawnPickupChance)
        {
            pickup = _pickups[UnityEngine.Random.Range(0, _pickups.Length)];
            return true;
        }
        pickup = null;
        return false;
    }

    public int ChangeHits(int amount) => Hits += amount;
    public int ChangeUpgrades(int amount) => Upgrades += amount;
    public void ChangePlayerHealth(float normalizedHealth) => OnPlayerHealthChanged(normalizedHealth);
}
