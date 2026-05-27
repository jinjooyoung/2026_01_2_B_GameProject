using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TweenSample : MonoBehaviour
{
    public RectTransform UITarget;
    public Image UIimage;
    public GameObject objectTarget;

    public TMP_Text countText;
    public int currntValue = 0;    
    public int addValue = 100;

    private int targetValue;

    public Color flashColor = Color.red;

    private Color originalColor;

    public CanvasGroup fadeTarget;

    public GameObject coinPrefab;

    void Start()
    {
        originalColor = UIimage.color;

        fadeTarget.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayPunchUIScale();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayPunchOBJScale();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayUIShake();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            PlayCountUp();
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            PlayColorFlash();
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            PlayFade();
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Vector3 dropPos = transform.position + Vector3.up;
            Instantiate(coinPrefab, dropPos, Quaternion.identity);
        }
    }

    public void PlayPunchUIScale()
    {
        if (UITarget == null) return;
        UITarget.DOKill();                      // 이전 효과 삭제
        UITarget.localScale = Vector3.one;      // 기본 크기로 초기화
        UITarget.DOPunchScale(Vector3.one * 0.3f, 0.25f, 8, 1.0f);  // 방향 * 크기, 시간, 진동횟수, 탄성
    }

    public void PlayPunchOBJScale()
    {
        if (objectTarget == null) return;
        objectTarget.transform.DOKill();                      // 이전 효과 삭제
        objectTarget.transform.localScale = Vector3.one;      // 기본 크기로 초기화
        objectTarget.transform.DOPunchScale(Vector3.one * 0.3f, 0.25f, 8, 1.0f);  // 방향 * 크기, 시간, 진동횟수, 탄성
    }

    public void PlayUIShake()
    {
        if (objectTarget ==null) return;
        objectTarget.transform.DOKill();
        objectTarget.transform.DOShakePosition(0.3f, 1f, 20, 90f);  // 시간, 강도, 진동횟수, 랜덤성
    }

    public void PlayCountUp()
    {
        if (countText == null) return;

        targetValue += addValue;            // 목표 숫자
        DOTween.Kill("CountTween", true);   // 기존 "CountTween" 연출을 완료한 후 종료 처리

        DOTween.To(
            () => currntValue,              // 현재 값
            value =>                        // 중간 값이 바뀔때마다 실행되는 부분
            {
                currntValue = value;
                countText.text = currntValue.ToString();
            },
            targetValue,                    // 목표값
            0.5f                            // 걸리는 시간
        )
        .SetEase(Ease.OutQuad)
        .SetId("CountTween");
    }

    public void PlayColorFlash()
    {
        if (UIimage == null) return;

        UIimage.DOKill();
        UIimage.color = originalColor;
        UIimage.DOColor(flashColor, 0.1f)
        .OnComplete(() =>
        {
            UIimage.DOColor(originalColor, 0.2f);
        });
    }

    public void PlayFade()
    {
        if (fadeTarget == null) return;
        fadeTarget.DOKill();
        fadeTarget.alpha = 0;

        Sequence seq = DOTween.Sequence();          // 여러 트윈을 순서대로 실행할 때 사용

        seq.Append(fadeTarget.DOFade(1, 0.2f));     // 0.2초 동안 나타난다.
        seq.AppendInterval(0.5f);                   // 0.5초 유지
        seq.Append(fadeTarget.DOFade(0f, 0.3f));    // 0.3초 동안 사라진다.
    }
}
