using UnityEngine;
using UnityEngine.UI;

public class AbilityCardUI : MonoBehaviour {
    public Text cardNameText;
    public Image iconImage;
    public Text descriptionText;

    public void Initialize(AbilityCardSO abilityCardSO) {
        cardNameText.text = abilityCardSO.cardName;
        iconImage.sprite = abilityCardSO.icon;
        descriptionText.text = abilityCardSO.description;
    }
}