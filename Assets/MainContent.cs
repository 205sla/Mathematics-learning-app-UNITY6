using DG.Tweening;
using Michsky.MUIP;
using UnityEngine;

public class MainContent : MonoBehaviour
{

    [SerializeField]
    RectTransform rectTransform;


    public void MoveMainContent(int x1, int x2, float time)
    {
        Vector2 currentPosition = rectTransform.anchoredPosition;
        rectTransform.anchoredPosition = new Vector2(x1, currentPosition.y);

        rectTransform.DOLocalMoveX(x2, time).SetEase(Ease.InOutQuad);
        Debug.Log(x1 + "->" + x2);

    }
}
