using UnityEngine;

[CreateAssetMenu]
public class ScriptablePlaneData : ScriptableObject
{
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public float Health { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public GunData[] Guns { get; private set; }
}
