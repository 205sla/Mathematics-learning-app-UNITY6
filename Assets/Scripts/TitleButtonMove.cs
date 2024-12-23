using UnityEngine;
using DG.Tweening;

public class TitleButtonMove : MonoBehaviour
{
    [SerializeField]
    float setX, delayTime;
    Vector3 targetScale = Vector3.one * 5;  // ��ǥ ũ�� (�⺻ ũ��: (5, 5, 5))

    void Start()
    {
        // ó�� ��ġ�� setX�� ���� (���� ��ǥ ����)
        transform.localPosition = new Vector3(setX, transform.localPosition.y, transform.localPosition.z);

        // X�� ���� ��ġ�� 0���� 1�� ���� �̵�
        Sequence mySequence = DOTween.Sequence();

        mySequence.AppendInterval(delayTime);

        // ���� X�� �ִϸ��̼�: setX���� 0���� 1�� ���� �̵�
        mySequence.Append(transform.DOLocalMoveX(0f, 1f));

        // ũ�� �ִϸ��̼�: 1�� ���� ������Ʈ ũ�⸦ ��ǥ ũ��(targetScale)�� ����
        mySequence.Join(transform.DOScale(targetScale, 1f));
    }
}
