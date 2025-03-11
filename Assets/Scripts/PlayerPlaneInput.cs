using UnityEngine;

public class PlayerPlaneInput : PlaneInput
{
    private void Update()
    {
        MoveDirection = GameplayManager.Instance.InputDirection;
    }
}
