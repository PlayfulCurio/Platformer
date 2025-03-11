using UnityEngine;

public class OnInvisibleDestroyer : MonoBehaviour
{
    [SerializeField] private GameObject _target;

    private void OnBecameInvisible()
    {
        Destroy(_target);
    }
}
