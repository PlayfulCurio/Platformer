using System;
using UnityEngine;

public class PlaneGun : MonoBehaviour
{
    private GunData _gunData;
    private float _fireCooldown;

    private void Update()
    {
        if ((_fireCooldown += Time.deltaTime) >= _gunData.FireInterval)
        {
            _fireCooldown -= _gunData.FireInterval;
            Instantiate(_gunData.Projectile, transform.position, transform.rotation);
        }
    }

    public void SetData(GunData data)
    {
        _gunData = data;
        transform.localPosition = data.Position;
    }
}

[Serializable]
public struct GunData
{
    [field: SerializeField] public Vector2 Position { get; private set; }
    [field: SerializeField] public Quaternion Rotation { get; private set; }
    [field:SerializeField] public float FireInterval { get; private set; }
    [field: SerializeField] public Projectile Projectile { get; private set; }
}