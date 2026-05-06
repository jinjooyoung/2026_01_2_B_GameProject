using System.Drawing;
using TMPro;
using UnityEngine;
using Color = UnityEngine.Color;

public class DamageEffectManager : MonoBehaviour
{
    [SerializeField] private GameObject textPrfabs;         // 텍스트 프리팹
    [SerializeField] private Canvas uiCanvas;               // UI 캔버스 참조

    public static DamageEffectManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (uiCanvas == null)
        {
            uiCanvas = FindAnyObjectByType<Canvas>();
            if (uiCanvas == null)
            {
                Debug.LogError("UI 캔버스를 찾을 수 없습니다.");
            }
        }
    }

    public void ShowDamageText(Vector3 position, string text, Color color, bool isCritical = false,
        bool isStatusEffect = false)
    {
        if (textPrfabs == null || uiCanvas == null) return;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(position);   // 월드 좌표를 스크린 좌표로 변환

        if (screenPos.z < 0) return;    // UI가 카메라 뒤에 있는 경우 표시 X

        GameObject damageText = Instantiate(textPrfabs, uiCanvas.transform);    // 데미지 텍스트 UI 생성

        RectTransform rectTransform = damageText.GetComponent<RectTransform>(); // 스크린 위치 설정

        if (rectTransform != null)
        {
            rectTransform.position = screenPos;
        }

        TextMeshProUGUI tmp = damageText.GetComponent<TextMeshProUGUI>();       // 텍스트 컴포넌트 설정

        if (tmp != null)
        {
            tmp.text = text;
            tmp.color = color;
            tmp.outlineColor = new Color(
                Mathf.Clamp01(color.r - 0.3f),
                Mathf.Clamp01(color.g - 0.3f),
                Mathf.Clamp01(color.b - 0.3f),
                color.a
            );
        }

        float scale = 1.0f;

        int numbericValue;

        if (int.TryParse(text.Replace("+", "").Replace("CRIT!", "").Replace("HEAL CRIT", ""), out numbericValue))
        {
            scale = Mathf.Clamp(numbericValue / 15f, 0.8f, 2.5f);
        }

        if (isCritical) scale = 1.4f;
        if (isStatusEffect) scale *= 0.8f;

        damageText.transform.localScale = new Vector3(scale, scale, scale);

        DamageTextEffect effect = damageText.GetComponent <DamageTextEffect>();

        if (effect != null)
        {
            effect.Initialized(isCritical, isStatusEffect);
            if (isStatusEffect)
            {
                effect.SetVerticalMovement();
            }
        }
    }

    public void ShowDamage(Vector3 position, int amount, bool isCritical = false)
    {
        string text = amount.ToString();
        Color color = isCritical ? new Color(1.0f, 0.8f, 0.0f) : new Color(1.0f, 0.3f, 0.3f);

        if (isCritical)
        {
            text = "CRIT\n" + text;
        }

        ShowDamageText(position, text, color, isCritical);
    }

    public void ShowHeal(Vector3 position, int amount, bool isCritical = false)
    {
        string text = amount.ToString();
        Color color = isCritical ? new Color(0.4f, 1.0f, 0.4f) : new Color(0.3f, 0.9f, 0.3f);

        if (isCritical)
        {
            text = "HEAL CRIT!\n" + text;
        }

        ShowDamageText(position, text, color, isCritical);
    }

    public void ShowMiss(Vector3 position)
    {
        ShowDamageText(position, "MISS", Color.gray, false);
    }

    public void ShowStatusEffect(Vector3 position, string effectName)
    {
        Color color;

        switch (effectName.ToLower())
        {
            case "Poison":
                color = new Color(0.5f, 0.1f, 0.5f);        // 보라색
                break;
            case "Burn":
                color = new Color(1.0f, 0.4f, 0.0f);        // 주황색
                break;
            case "Freeze":
                color = new Color(0.5f, 0.0f, 1.0f);        // 하늘색
                break;
            case "Stun":
                color = new Color(1.0f, 1.0f, 0.0f);        // 노란색
                break;
            default:
                color = new Color(1.0f, 1.0f, 1.0f);        // 기본 흰색
                break;
        }

        ShowDamageText(position, effectName.ToLower(), Color.gray, false);
    }
}
