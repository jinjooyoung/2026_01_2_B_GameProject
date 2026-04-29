using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card" , menuName = "Card/Card Data")]
public class CardData : ScriptableObject
{
    public enum CardType    // 카드 타입 열거형 추가
    {
        Attack,             // 공격 카드
        Heal,               // 회복 카드
        Buff,               // 버프 카드
        Utility             // 유틸리티 카드
    }

    public string cardName;         // 카드 이름
    public string description;      // 카드 설명
    public Sprite artwork;          // 카드 이미지
    public int manaCost;            // 마나 비용
    public int effectAmount;        // 효과 값 (공격력)
    public CardType cardType;       // 카드 타입

    // 추가 효과 리스트
    public List<AdditionalEffect> additionalEffects = new List<AdditionalEffect>();

    public enum AdditionalEffectType
    {
        None,
        DrawCard,
        DiscardCard,
        GainMana,
        ReduceEnemyMana,
        ReduceCardCost
    }

    public Color GetCardColor()
    {
        switch (cardType)
        {
            case CardType.Attack:
                return Color.red;

            case CardType.Heal:
                return Color.green;

            case CardType.Buff:
                return Color.blue;

            case CardType.Utility:
                return Color.yellow;

            default:
                return Color.white;
        }
    }

    // 추가 효과 정보를 문자열로 변환
    public string GetAdditionalEffectDescription()
    {
        if (additionalEffects.Count == 0)
            return "";

        string result = "\n";

        foreach (var effect in additionalEffects)
        {
            result += effect.GetDescription() + "\n";
        }

        return result;
    }
}
