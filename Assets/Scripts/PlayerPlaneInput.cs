using UnityEngine;

public class PlayerPlaneInput : PlaneInput
{
    private void Update()
    {
        NormalizedMoveDirection = InputAndCameraManager.Instance.MoveDirection;
    }
}
