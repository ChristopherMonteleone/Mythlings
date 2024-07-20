using UnityEngine;

[CreateAssetMenu(fileName = "NewMonsterCard", menuName = "Card Game/Monster Card")]
public class MonsterCardSO : ScriptableObject 
{
    public string monsterName;
    public Sprite icon;
    public string description;
    public AbilityCardSO[] possibleAttackCards;
}
