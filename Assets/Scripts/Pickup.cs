using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody2D _rigidbody;

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + Vector2.down * Time.fixedDeltaTime * _speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
