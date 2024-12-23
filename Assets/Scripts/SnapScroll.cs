using System.Collections;  // �� �κ��� �߰��ؾ� �մϴ�.
using UnityEngine;
using UnityEngine.UI;

public class SnapScroll : MonoBehaviour
{
    public ScrollRect scrollRect;               // ScrollRect ������Ʈ
    public RectTransform content;               // Content RectTransform
    public float snapSpeed = 10f;               // ���� �ӵ�
    public float itemWidth = 200f;              // �� �������� �ʺ� (UI ��ҵ��� �ʺ�)
    public float itemSpacing = 10f;             // ������ �� ����

    private bool isSnapping = false;            // ���� ������ Ȯ��
    private bool isScrolling = false;           // ��ũ�� ������ Ȯ��

    void Update()
    {
        // ��ũ���� ���� ������ ���� ���� ����
        if (!isScrolling && !isSnapping)
        {
            StartCoroutine(SnapToClosestItem());
        }
    }

    // ���� ����� ���������� �����ϴ� �ڷ�ƾ
    private IEnumerator SnapToClosestItem()
    {
        isSnapping = true;

        // ���� ��ũ�� ��ġ (0-1 ����)
        float currentPos = scrollRect.horizontalNormalizedPosition;

        // �������� ����
        int totalItems = Mathf.FloorToInt(content.rect.width / (itemWidth + itemSpacing));

        // ���� ����� ������ �ε����� ���
        float targetPos = Mathf.Round(currentPos * (totalItems - 1)) / (totalItems - 1);

        // ������ ������ �� �� �ֵ��� `targetPos`�� ���缭 ��ũ��
        float elapsedTime = 0f;
        float initialPos = scrollRect.horizontalNormalizedPosition;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * snapSpeed;
            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(initialPos, targetPos, elapsedTime);
            yield return null;
        }

        // ������ �Ϸ�Ǹ� isSnapping�� false�� ����
        isSnapping = false;
    }

    // ��ũ�� ���� �� ȣ��
    public void OnBeginDrag()
    {
        isScrolling = true;
    }

    // ��ũ�� ������ ȣ��
    public void OnEndDrag()
    {
        isScrolling = false;
    }
}
