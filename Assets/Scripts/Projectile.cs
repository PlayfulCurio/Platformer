﻿using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] private Rigidbody2D _rigidbody;

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + (Vector2)transform.up * Time.fixedDeltaTime * _speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && TryGetComponent<Plane>(out var plane))
        {
            plane.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}