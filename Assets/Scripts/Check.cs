using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Check : MonoBehaviour
{
    public CanvasGroup highlightCanvasGroup;  // 하이라이트를 제어할 CanvasGroup
    public bool highlightOn = false;          // 하이라이트 활성화 여부

    private Coroutine fadeCoroutine;  // 현재 실행 중인 코루틴을 추적하기 위한 변수
    private bool isFading = false;    // 페이드 중인지 여부를 추적

    private void Update()
    {
        // 페이드 중이 아니면 새로운 코루틴을 시작
        if (!isFading)
        {
            if (highlightOn)
            {
                fadeCoroutine = StartCoroutine(FadeInHighlight());
            }
            else
            {
                fadeCoroutine = StartCoroutine(FadeOutHighlight());
            }
        }
    }

    // 하이라이트 페이드 인 (활성화)
    private IEnumerator FadeInHighlight()
    {
        isFading = true;  // 페이드 시작
        while (highlightCanvasGroup.alpha < 1f)
        {
            highlightCanvasGroup.alpha += Time.deltaTime * 8f;  // 속도 조정
            yield return null;
        }
        highlightCanvasGroup.alpha = 1f;
        isFading = false;  // 페이드 완료
    }

    // 하이라이트 페이드 아웃 (비활성화)
    private IEnumerator FadeOutHighlight()
    {
        isFading = true;  // 페이드 시작
        while (highlightCanvasGroup.alpha > 0f)
        {
            highlightCanvasGroup.alpha -= Time.deltaTime * 8f;  // 속도 조정
            yield return null;
        }
        highlightCanvasGroup.alpha = 0f;
        isFading = false;  // 페이드 완료
    }

    // 외부에서 하이라이트 상태를 변경할 수 있도록 하는 메서드
    public void SetHighlightON(bool isEnabled)
    {
        highlightOn = isEnabled;
    }
}
