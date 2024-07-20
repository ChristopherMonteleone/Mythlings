using UnityEngine;
using UnityEngine.UI;

public class MonsterCardUI : MonoBehaviour {
    public Text monsterNameText;
    public Image iconImage;
    public Text descriptionText;

    public void Initialize(MonsterCardSO monsterCardSO) {
        monsterNameText.text = monsterCardSO.monsterName;
        iconImage.sprite = monsterCardSO.icon;
        descriptionText.text = monsterCardSO.description;
    }
}