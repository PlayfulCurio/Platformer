using System;
using UnityEngine;

public class GameplayManager : MonoBehaviourSingleton<GameplayManager>
{
    [SerializeField] private GameObject[] _pickups;

    private float _basePickupChance = .05f;
    private float _pickupChanceIncreaseEverySecond = .01f;

    private float _spawnPickupChance;

    public Vector2 InputDirection { get; private set; }
    public int Kills { get; private set; }
    public int Hits { get; private set; }
    public int Upgrades { get; private set; }
    public int Score => Kills + Upgrades - Hits;

    public event Action<bool> OnGameOver;
    public event Action<float> OnPlayerHealthChanged;

    protected override void Initialize()
    {
        base.Initialize();
        _spawnPickupChance = _basePickupChance;
    }

    private void FixedUpdate()
    {
        _spawnPickupChance += _pickupChanceIncreaseEverySecond * Time.fixedDeltaTime;
    }

    private void Update()
    {
        InputDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (InputDirection.sqrMagnitude > 1f)
            InputDirection = InputDirection.normalized;
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
            _spawnPickupChance = _basePickupChance;
            pickup = _pickups[UnityEngine.Random.Range(0, _pickups.Length)];
            return true;
        }
        pickup = null;
        return false;
    }

    public void ChangePlayerHealth(float changeDelta, float normalizedHealth)
    {
        if (changeDelta < 0)
            Hits++;
        OnPlayerHealthChanged(normalizedHealth);
    }

    public int ChangeUpgrades(int amount) => Upgrades += amount;
}
