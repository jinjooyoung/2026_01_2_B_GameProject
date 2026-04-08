using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    [Header("캐릭터 이름")]
    public string charcterName;

    [Header("체력")]
    public int maxHp = 100;
    public int currentHp;

    [Header("UI 요소")]
    public Slider hpBar;
    public TextMeshProUGUI hpText;

    void Start()
    {
        currentHp = maxHp;
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
    }

    public void Heal(int amount)
    {
        currentHp += amount;
    }
}
