using System;
using UnityEngine;

public class InputAndCameraManager : MonoBehaviourSingleton<InputAndCameraManager>
{
    [field:SerializeField] public Camera Camera { get; private set; }
    
    public Vector2 MousePosition { get; private set; }
    public Vector2 MoveDirection { get; private set; }
    public Vector2 HalfWorldScreenSize { get; private set; }

    private void Update()
    {
        MousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);
        MoveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (MoveDirection.sqrMagnitude > 1f)
            MoveDirection = MoveDirection.normalized;
    }

    public Vector2 ClampToCamera(Vector2 position)
    {
        return new Vector2(Mathf.Clamp(position.x, -HalfWorldScreenSize.x, HalfWorldScreenSize.x), Mathf.Clamp(position.y, -HalfWorldScreenSize.y, HalfWorldScreenSize.y));
    }

    protected override void Initialize()
    {
        base.Initialize();
        HalfWorldScreenSize = new Vector2(Camera.orthographicSize * Camera.aspect, Camera.orthographicSize);
    }
}
