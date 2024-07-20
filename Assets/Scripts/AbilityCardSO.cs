using UnityEngine;

[CreateAssetMenu(fileName = "NewAbilityCard", menuName = "Card Game/Ability Card")]
public class AbilityCardSO : ScriptableObject
{
    public string cardName;
    public Sprite icon;
    public string description;
    public CardEffectType effectType;
}

public enum CardEffectType {
    Damage,
    Heal,
    Draw,
    // Add other effect types here
}