using System;
using System.Collections;
using UnityEngine;

public class Plane : DestructibleEntity
{
    [SerializeField] private ScriptablePlaneData _data;
    [SerializeField] private PlaneInput _planeInput;
    [SerializeField] private bool _clampPositionToViewport;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private PlaneGun[] _guns;

    private float _maxTilt = 20f;
    private float _tilt;

    protected override void Awake()
    {
        base.Awake();
        SetData(_data);
    }

    private void FixedUpdate()
    {
        if (!_isDead)
            Move();
    }

    private void Update()
    {
        if (!_isDead)
            TiltPlane();
    }

    public void SetData(ScriptablePlaneData data)
    {
        if (!_isDead)
        {
            _data = data;
            _currentHealth = _data.Health;
            _spriteRenderer.sprite = _data.Sprite;
            _flickerSpriteRenderer.sprite = _data.FlickerSprite;
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
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        if (_isDead)
        {
            _spriteRenderer.transform.rotation = Quaternion.identity;
            foreach (var gun in _guns)
                gun.gameObject.SetActive(false);
        }
    }

    private void Move()
    {
        var position = _rigidbody.position + _planeInput.MoveDirection * Time.fixedDeltaTime * _data.Speed;
        if (_clampPositionToViewport)
            position = InputAndCameraManager.Instance.ClampToCamera(position);
        _rigidbody.MovePosition(position);
    }

    private void TiltPlane()
    {
        _tilt = Mathf.LerpUnclamped(0f, _maxTilt, -_planeInput.MoveDirection.x);
        _spriteRenderer.transform.localRotation = Quaternion.Euler(Vector3.up * _tilt);
    }
}
