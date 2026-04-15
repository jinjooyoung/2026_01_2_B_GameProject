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

    [Header("마나")]
    public int maxMana = 10;
    public int currentMana;
    public Slider manaBar;
    public TextMeshProUGUI manaText;

    [Header("UI 요소")]
    public Slider hpBar;
    public TextMeshProUGUI hpText;

    void Start()
    {
        currentHp = maxHp;
        currentMana = maxMana;
        UpdateUI();
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
    }

    public void Heal(int amount)
    {
        currentHp += amount;
    }

    public void UseMana(int amount)
    {
        currentMana -= amount;
        if (currentMana < 0)
        {
            currentMana = 0;
        }
        UpdateUI();
    }

    public void GainMana(int amount)
    {
        currentMana += amount;

        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (hpBar != null)
        {
            hpBar.value = (float)currentHp / maxHp;
        }

        if (hpText != null)
        {
            hpText.text = $"{currentHp} / {maxHp}";
        }

        if (manaBar != null)
        {
            manaBar.value = (float)currentMana / maxMana;
        }

        if (manaText != null)
        {
            manaText.text = $"{currentMana} / {maxMana}";
        }
    }
}
