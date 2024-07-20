using UnityEngine;

// Manage Monster Creation

public class GameManager : MonoBehaviour {
    public CardManager cardManager;
    public MonsterCardSO[] monsterCards;

    void Start() {
        foreach (MonsterCardSO monsterCard in monsterCards) {
            cardManager.CreateMonsterCard(monsterCard);
        }
    }
}