using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Check : MonoBehaviour
{
    public CanvasGroup highlightCanvasGroup;  // ���̶���Ʈ�� ������ CanvasGroup
    public bool highlightOn = false;          // ���̶���Ʈ Ȱ��ȭ ����

    private Coroutine fadeCoroutine;  // ���� ���� ���� �ڷ�ƾ�� �����ϱ� ���� ����
    private bool isFading = false;    // ���̵� ������ ���θ� ����

    private void Update()
    {
        // ���̵� ���� �ƴϸ� ���ο� �ڷ�ƾ�� ����
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

    // ���̶���Ʈ ���̵� �� (Ȱ��ȭ)
    private IEnumerator FadeInHighlight()
    {
        isFading = true;  // ���̵� ����
        while (highlightCanvasGroup.alpha < 1f)
        {
            highlightCanvasGroup.alpha += Time.deltaTime * 8f;  // �ӵ� ����
            yield return null;
        }
        highlightCanvasGroup.alpha = 1f;
        isFading = false;  // ���̵� �Ϸ�
    }

    // ���̶���Ʈ ���̵� �ƿ� (��Ȱ��ȭ)
    private IEnumerator FadeOutHighlight()
    {
        isFading = true;  // ���̵� ����
        while (highlightCanvasGroup.alpha > 0f)
        {
            highlightCanvasGroup.alpha -= Time.deltaTime * 8f;  // �ӵ� ����
            yield return null;
        }
        highlightCanvasGroup.alpha = 0f;
        isFading = false;  // ���̵� �Ϸ�
    }

    // �ܺο��� ���̶���Ʈ ���¸� ������ �� �ֵ��� �ϴ� �޼���
    public void SetHighlightON(bool isEnabled)
    {
        highlightOn = isEnabled;
    }
}
