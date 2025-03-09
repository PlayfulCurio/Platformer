using UnityEngine;

public class AutoScrollingBehaviour : MonoBehaviour
{
    [SerializeField] private float _scrollingSpeed;

    private void Awake()
    {
        Physics2D.autoSyncTransforms = true;
    }

    private void FixedUpdate()
    {
        transform.position += Vector3.down * Time.fixedDeltaTime * _scrollingSpeed;
    }
}
