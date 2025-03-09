using UnityEngine;

public class OnVisibleEnabler : MonoBehaviour
{
    [SerializeField] private Behaviour[] _behavioursToEnable;

    private void OnBecameVisible()
    {
        foreach (var behaviour in _behavioursToEnable)
        {
            behaviour.enabled = true;
        }
    }
}
