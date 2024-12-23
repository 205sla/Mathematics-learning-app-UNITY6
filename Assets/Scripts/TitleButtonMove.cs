using UnityEngine;
using DG.Tweening;

public class TitleButtonMove : MonoBehaviour
{
    [SerializeField]
    float setX, delayTime;
    Vector3 targetScale = Vector3.one * 5;  // 목표 크기 (기본 크기: (5, 5, 5))

    void Start()
    {
        // 처음 위치를 setX로 설정 (로컬 좌표 기준)
        transform.localPosition = new Vector3(setX, transform.localPosition.y, transform.localPosition.z);

        // X축 로컬 위치를 0으로 1초 동안 이동
        Sequence mySequence = DOTween.Sequence();

        mySequence.AppendInterval(delayTime);

        // 로컬 X축 애니메이션: setX에서 0으로 1초 동안 이동
        mySequence.Append(transform.DOLocalMoveX(0f, 1f));

        // 크기 애니메이션: 1초 동안 오브젝트 크기를 목표 크기(targetScale)로 변경
        mySequence.Join(transform.DOScale(targetScale, 1f));
    }
}
