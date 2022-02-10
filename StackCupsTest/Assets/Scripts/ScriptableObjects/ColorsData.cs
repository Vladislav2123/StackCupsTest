using UnityEngine;

[CreateAssetMenu]
public class ColorsData : ScriptableObject
{
    [SerializeField] private Color[] _colors;

    public Color[] Colors => _colors;
}
