using UnityEngine;

public class GroundFoe : DestructibleEntity
{
    [SerializeField] private float _shootInterval;
    [SerializeField] private Projectile _projectile;
    [SerializeField] private SpriteRenderer _baseSpriteRenderer;
    [SerializeField] private Transform _barrelTransform;
    [SerializeField] private Transform _targetTransform;

    private float _rotationSpeed = 2f;
    private float _maxAngleToShoot = 5f;
    private float _shootCooldown;

    private void FixedUpdate()
    {
        if (_targetTransform != null)
        {
            var targetRotation = _targetTransform.position - transform.position;
            _barrelTransform.up = RotateTowards(_barrelTransform.up, targetRotation, _rotationSpeed * Time.fixedDeltaTime, 0f);
            if ((_shootCooldown -= Time.fixedDeltaTime) <= 0 && Mathf.Abs(Vector2.SignedAngle(_barrelTransform.up, targetRotation)) <= _maxAngleToShoot)
            {
                _shootCooldown = _shootInterval;
                Instantiate(_projectile, (Vector2)_barrelTransform.position, _barrelTransform.rotation);
            }
        }
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        if (_isDead)
            _baseSpriteRenderer.enabled = false;
    }

    private Vector2 RotateTowards(Vector2 current, Vector2 target, float maxRadiansDelta, float maxMagnitudeDelta)
    {
        if (current.x + current.y == 0)
            return target.normalized * maxMagnitudeDelta;

        float signedAngle = Vector2.SignedAngle(current, target);
        float stepAngle = Mathf.MoveTowardsAngle(0, signedAngle, maxRadiansDelta * Mathf.Rad2Deg) * Mathf.Deg2Rad;
        Vector2 rotated = new Vector2(
            current.x * Mathf.Cos(stepAngle) - current.y * Mathf.Sin(stepAngle),
            current.x * Mathf.Sin(stepAngle) + current.y * Mathf.Cos(stepAngle)
        );
        if (maxMagnitudeDelta == 0)
            return rotated;

        float magnitude = current.magnitude;
        float targetMagnitude = target.magnitude;
        targetMagnitude = Mathf.MoveTowards(magnitude, targetMagnitude, maxMagnitudeDelta);
        return rotated.normalized * targetMagnitude;
    }
}
