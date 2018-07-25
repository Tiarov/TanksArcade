using UnityEngine;

[CreateAssetMenu(fileName = "Characters Config settings", menuName = "Configs/Characters Config")]
public class CharactersConfig : ScriptableObject
{
    public float Health;
    [Range(0f, 1f)]
    public float Armor;
    public float Speed;
}
