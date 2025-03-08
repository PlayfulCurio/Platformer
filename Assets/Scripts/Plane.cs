using System;
using System.Collections;
using UnityEngine;

public class Plane : MonoBehaviour
{
    [SerializeField] private ScriptablePlaneData _data;
    [SerializeField] private PlaneInput _planeInput;
    [SerializeField] private bool _clampPositionToViewport;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private SpriteRenderer _flickerSpriteRenderer;
    [SerializeField] private Animator _animator;
    [SerializeField] private PlaneGun[] _guns;

    private float _flickerTime = .6f, _flickerInterval = .15f;
    private float _maxTilt = 20f;
    private float _tilt;
    private float _currentHealth;
    private bool _isDead;
    private Coroutine _flickerCoroutine;
    private WaitForSeconds _flickerWait;

    private void Awake()
    {
        _flickerWait = new WaitForSeconds(_flickerInterval);
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

    public void TakeDamage(float amount)
    {
        if (!_isDead)
        {
            if (_flickerCoroutine != null)
                StopCoroutine(_flickerCoroutine);
            if((_currentHealth -= amount) > 0)
            {
                _flickerCoroutine = StartCoroutine(Flicker());
            }
            else
            {
                _isDead = true;
                _spriteRenderer.transform.rotation = Quaternion.identity;
                _flickerSpriteRenderer.color = Color.clear;
                foreach (var gun in _guns)
                    gun.gameObject.SetActive(false);
                _animator.SetTrigger("Explode");
            }
        }
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

    private IEnumerator Flicker()
    {
        int ticks = (int)(_flickerTime / _flickerInterval);
        for (int i = 0; i < ticks; i++)
        {
            _flickerSpriteRenderer.color = (i % 2 == 1 ? Color.clear : Color.white);
            yield return _flickerWait;
        }
        _spriteRenderer.color = Color.clear;
    }

    private void FinishExploding() => Destroy(gameObject);
}
