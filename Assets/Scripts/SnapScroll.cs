using System.Collections;  // 이 부분을 추가해야 합니다.
using UnityEngine;
using UnityEngine.UI;

public class SnapScroll : MonoBehaviour
{
    public ScrollRect scrollRect;               // ScrollRect 컴포넌트
    public RectTransform content;               // Content RectTransform
    public float snapSpeed = 10f;               // 스냅 속도
    public float itemWidth = 200f;              // 각 아이템의 너비 (UI 요소들의 너비)
    public float itemSpacing = 10f;             // 아이템 간 간격

    private bool isSnapping = false;            // 스냅 중인지 확인
    private bool isScrolling = false;           // 스크롤 중인지 확인

    void Update()
    {
        // 스크롤이 진행 중이지 않을 때만 스냅
        if (!isScrolling && !isSnapping)
        {
            StartCoroutine(SnapToClosestItem());
        }
    }

    // 가장 가까운 아이템으로 스냅하는 코루틴
    private IEnumerator SnapToClosestItem()
    {
        isSnapping = true;

        // 현재 스크롤 위치 (0-1 사이)
        float currentPos = scrollRect.horizontalNormalizedPosition;

        // 아이템의 갯수
        int totalItems = Mathf.FloorToInt(content.rect.width / (itemWidth + itemSpacing));

        // 가장 가까운 아이템 인덱스를 계산
        float targetPos = Mathf.Round(currentPos * (totalItems - 1)) / (totalItems - 1);

        // 스냅을 빠르게 할 수 있도록 `targetPos`에 맞춰서 스크롤
        float elapsedTime = 0f;
        float initialPos = scrollRect.horizontalNormalizedPosition;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * snapSpeed;
            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(initialPos, targetPos, elapsedTime);
            yield return null;
        }

        // 스냅이 완료되면 isSnapping을 false로 설정
        isSnapping = false;
    }

    // 스크롤 시작 시 호출
    public void OnBeginDrag()
    {
        isScrolling = true;
    }

    // 스크롤 끝나면 호출
    public void OnEndDrag()
    {
        isScrolling = false;
    }
}
