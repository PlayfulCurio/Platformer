using System;
using UnityEngine;

public class Plane : MonoBehaviour
{
    [SerializeField] private ScriptablePlaneData _data;
    [SerializeField] private PlaneInput _planeInput;
    [SerializeField] private bool _clampPositionToViewport;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private PlaneGun[] _guns;

    private float _maxTilt = 20f;
    private float _tilt;
    private float _currentHealth;

    private void Awake()
    {
        SetData(_data);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        TiltPlane();
    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
    }

    public void SetData(ScriptablePlaneData data)
    {
        _data = data;
        _currentHealth = _data.Health;
        _spriteRenderer.sprite = _data.Sprite;
        for (int i = 0; i < _guns.Length; i++)
        {
            if (_data.Guns.Length > i)
            {
                _guns[i].gameObject.SetActive(true);
                _guns[i].SetData(_data.Guns[i]);
            }
            else
                _guns[i].gameObject.SetActive(false);
        }
    }

    private void Move()
    {
        var position = _rigidbody.position + _planeInput.NormalizedMoveDirection * Time.fixedDeltaTime * _data.Speed;
        if (_clampPositionToViewport)
            position = InputAndCameraManager.Instance.ClampToCamera(position);
        _rigidbody.MovePosition(position);
    }

    private void TiltPlane()
    {
        _tilt = Mathf.LerpUnclamped(0f, _maxTilt, -_planeInput.NormalizedMoveDirection.x);
        _spriteRenderer.transform.localRotation = Quaternion.Euler(Vector3.up * _tilt);
    }
}
