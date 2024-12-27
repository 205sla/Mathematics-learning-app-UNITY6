using DG.Tweening;
using Michsky.MUIP;
using TMPro;
using UnityEngine;

public class TopBar : MonoBehaviour
{
    [SerializeField]
    TMP_Text HeartCount;
    [SerializeField]
    GameObject progressBar, Heart;

    float value = 0f;

    private void Start()
    {
        progressBar.GetComponent<ProgressBar>().SetValue(0);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SetHeartCount(int heartCount)
    {
        if (heartCount > 0)
        {
            HeartCount.text = heartCount.ToString();
        }
        else
        {
            Heart.SetActive(false);
        }
    }

    public void SetProgress(float progress)
    {
        // progress ���� 100�� ���� �ʵ��� ����
        float targetProgress = Mathf.Min(progress, 100);

        // DOTween�� ����Ͽ� value ������ 2�ʿ� ���� ������Ŵ
        DOTween.To(() => value, x => value = x, targetProgress, 1f)
            .SetEase(Ease.OutBounce)
            .OnUpdate(() =>
            {
                progressBar.GetComponent<ProgressBar>().SetValue(value);
            })
            .OnComplete(() =>
            {
                progressBar.GetComponent<ProgressBar>().SetValue(value);
            });

    }
}
