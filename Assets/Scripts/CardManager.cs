using System.Collections.Generic;
using UnityEngine;

// Spawn Monsters and Their Ability Cards

// Todo: Add function to draw a card, use on start of each turn
// Todo: Change to function as a FriendlyMonsterCard and EnemyMonsterCard, where only FriendlyMonsterCard's have a visible hand

public class CardManager : MonoBehaviour {
    public GameObject monsterCardPrefab; // Assign the MonsterCardPrefab in the inspector
    public GameObject abilityCardPrefab; // Assign the AbilityCardPrefab in the inspector
    public Color hoverColor = Color.red; // Hover color for cards

    private List<GameObject> abilityCards = new List<GameObject>(); // List to keep track of ability cards

    // Method to create a monster card
    public void CreateMonsterCard(MonsterCardSO monsterCardSO) {
        // Instantiate the monster card prefab
        GameObject monsterCard = Instantiate(monsterCardPrefab, transform);

        // Initialize the monster card with data from the ScriptableObject
        MonsterCardUI monsterCardUI = monsterCard.GetComponent<MonsterCardUI>();
        if (monsterCardUI != null) {
            monsterCardUI.Initialize(monsterCardSO);
        }

        // Randomly select three ability cards from the possible attack cards
        AbilityCardSO[] selectedAbilityCards = SelectRandomAbilityCards(monsterCardSO.possibleAttackCards, 3);

        // Instantiate and initialize the selected ability cards
        for (int i = 0; i < selectedAbilityCards.Length; i++) {
            GameObject abilityCard = Instantiate(abilityCardPrefab); // instantiate without parent to set world position

            AbilityCardUI abilityCardUI = abilityCard.GetComponent<AbilityCardUI>();
            if (abilityCardUI != null) {
                abilityCardUI.Initialize(selectedAbilityCards[i]);
            }

            // Add hover handler to the ability card
            AddHoverHandler(abilityCard);

            // Add the ability card to the list
            abilityCards.Add(abilityCard);
        }

        // Reorganize the cards to be centered
        ReorganizeCards();
    }

    // Method to select a specified number of random ability cards from an array
    private AbilityCardSO[] SelectRandomAbilityCards(AbilityCardSO[] abilityCards, int numberOfCards) {
        AbilityCardSO[] selectedCards = new AbilityCardSO[numberOfCards];
        System.Random random = new System.Random();
        for (int i = 0; i < numberOfCards; i++) {
            int randomIndex = random.Next(abilityCards.Length);
            selectedCards[i] = abilityCards[randomIndex];
        }
        return selectedCards;
    }

    // Method to add hover handler to a card
    private void AddHoverHandler(GameObject card) {
        if (card.GetComponent<CardHoverHandler>() == null) {
            CardHoverHandler hoverHandler = card.AddComponent<CardHoverHandler>();
            hoverHandler.hoverColor = hoverColor;
        }
    }

    // Method to remove a card from the game
    public void RemoveCard(Transform cardTransform) {
        abilityCards.Remove(cardTransform.gameObject);
        Destroy(cardTransform.gameObject);
        ReorganizeCards();
    }

    // Method to reorganize cards to be centered around x = 0
    private void ReorganizeCards() {
        int cardCount = abilityCards.Count;
        if (cardCount == 0) return;

        float cardWidth = 3f; // Assume each card has a width of 3 units for calculation
        float totalWidth = (cardCount - 1) * cardWidth;
        Vector3 initialPosition = new Vector3(-totalWidth / 2, -3.5f, 0); // starting position

        for (int i = 0; i < cardCount; i++) {
            GameObject abilityCard = abilityCards[i];
            abilityCard.transform.position = initialPosition + new Vector3(i * cardWidth, 0, 0);
        }
    }
}