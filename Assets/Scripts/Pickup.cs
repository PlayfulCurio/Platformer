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
        var plane = collision.GetComponent<Plane>();
        //if (plane.IsPlayerSide != _isPlayerSide)
        //{
        //    plane.TakeDamage(_damage);
        //    Destroy(gameObject);
        //}
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
