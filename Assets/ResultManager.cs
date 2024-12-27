using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;


public class ResultManager : MonoBehaviour
{
    [SerializeField]
    GameObject ResultBoard;

    [SerializeField]
    RectTransform Text1;

    [SerializeField]
    TMP_Text comment, Continuous;

    [SerializeField]
    AttendanceManager AttendanceManager;

    bool IsInputAnswer;

    Dictionary<string, string> ProblemCourseResults;
    private void Awake()
    {
        Text1.anchoredPosition = new Vector2(Text1.anchoredPosition.x, 2000f);
        if (ES3.KeyExists("ProblemCourseResults"))
        {
            ProblemCourseResults = ES3.Load<Dictionary<string, string>>("ProblemCourseResults");
        }
        comment.alpha = 0f;
        comment.text = AddNewLineAfterPunctuation(ProblemCourseResults["comment"]);
        Continuous.text = AddNewLineAfterPunctuation(AttendanceManager.GetConsecutiveDays());
    }
    void Start()
    {
        ResultShow();
        Debug.Log("���â ����");
        StartCoroutine(BtnShow());
    }

    void ResultShow()
    {
        // Sequence ����
        Sequence sequence = DOTween.Sequence();

        // 0.2�� ���
        sequence.AppendInterval(0.2f);

        // RectTransform�� ���� Y ��ǥ�� 2000���� 1200���� �̵�
        sequence.Append(Text1.DOLocalMoveY(1200f, 0.3f).SetEase(Ease.InOutQuad));

        // comment �ؽ�Ʈ�� ������ 0���� 1�� ���� ���̰� ����
        sequence.Append(comment.DOFade(1f, 0.5f).SetEase(Ease.InOutQuad));

        // �ִϸ��̼� ����
        sequence.Play();
    }

    IEnumerator BtnShow()
    {
        ResultBoard.GetComponent<ResultBoard>().SetBtn("Ȯ��", -100, 0.5f);
        yield return new WaitForSeconds(0.5f);

        IsInputAnswer = false;
        yield return new WaitUntil(() => IsInputAnswer);

        ResultBoard.GetComponent<ResultBoard>().SetBtn("���׿�", 500, 0.5f);
        yield return new WaitForSeconds(0.5f);


        IsInputAnswer = false;
        yield return new WaitUntil(() => IsInputAnswer);

        ResultBoard.GetComponent<ResultBoard>().SetBtn("����", -500, 0.5f);
        yield return new WaitForSeconds(0.5f);

        GameManager.instance.LoadScene("Title");
    }

    string AddNewLineAfterPunctuation(string input)
    {
        // !, , �Ǵ� . �ڿ� \n�� �߰�
        string pattern = @"([!,\.])";  // !, , �Ǵ� .�� ã�Ƽ�
        string replacement = "$1\n";    // �ش� ���ڸ� ã�� �� �ڿ� \n�� �߰�

        // Replace ����
        return Regex.Replace(input, pattern, replacement);
    }

    public void ResultButton()
    {
        IsInputAnswer = true;

    }
}
