using UnityEngine;

public class PlayerPlaneInput : PlaneInput
{
    private void Update()
    {
        MoveDirection = InputAndCameraManager.Instance.MoveDirection;
    }
}
