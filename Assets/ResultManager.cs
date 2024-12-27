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
    TMP_Text comment, Continuous, ComboT, TimeT, PercentT, ComboN, TimeN, PercentN;

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
        ComboN.text = ProblemCourseResults["maxCombo"];
        TimeN.text = FormatTime(ProblemCourseResults["time"]);
        PercentN.text = ProblemCourseResults["percentage"];
    }
    void Start()
    {
        ResultShow();
        StartCoroutine(BtnShow());
    }

    void ResultShow()
    {// Sequence ����
        Sequence sequence = DOTween.Sequence();

        // 0.2�� ���
        sequence.AppendInterval(0.2f);

        // RectTransform�� ���� Y ��ǥ�� 2000���� 1200���� �̵�
        sequence.Append(Text1.DOLocalMoveY(1200f, 0.3f).SetEase(Ease.InOutQuad));

        // comment �ؽ�Ʈ�� ������ 0���� 1�� ���� ���̰� ����
        sequence.Append(comment.DOFade(1f, 0.5f).SetEase(Ease.InOutQuad));

        // 0.2�� ���
        sequence.AppendInterval(0.2f);

        sequence.Join(ComboT.DOFade(1f, 0.5f).SetEase(Ease.InOutQuad));
        sequence.AppendInterval(0.3f);

        sequence.Join(ComboN.DOFade(1f, 0.5f).SetEase(Ease.InOutQuad));
        sequence.AppendInterval(0.5f);


        sequence.Join(TimeT.DOFade(1f, 0.5f).SetEase(Ease.InOutQuad));
        sequence.AppendInterval(0.3f);

        sequence.Join(TimeN.DOFade(1f, 0.5f).SetEase(Ease.InOutQuad));
        sequence.AppendInterval(0.5f);


        sequence.Join(PercentT.DOFade(1f, 0.5f).SetEase(Ease.InOutQuad));
        sequence.AppendInterval(0.3f);

        sequence.Join(PercentN.DOFade(1f, 0.5f).SetEase(Ease.InOutQuad));
        sequence.AppendInterval(0.5f);



        // �ִϸ��̼� ����
        sequence.Play();
    }

    IEnumerator BtnShow()
    {
        yield return new WaitForSeconds(4f);
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

    public string FormatTime(string s)
    {
        // ���ڿ��� ���ڷ� ��ȯ �������� Ȯ��
        if (int.TryParse(s, out int totalSeconds))
        {
            // �а� �ʷ� ������
            int minutes = totalSeconds / 60;
            int seconds = totalSeconds % 60;

            // 2�ڸ� ���� �������� ��ȯ�ϱ� ���� string.Format ���
            return string.Format("{0:D2}:{1:D2}", minutes, seconds);
        }
        else
        {
            Debug.Log("�̻��� �ð��� ���Ծ�!!!:" + s);
            // ��ȯ�� �� ���� ��� ���� ó�� (Ȥ�� ����Ʈ �� ��ȯ)
            return "00:00";  // ����: �⺻���� "00:00"���� ����
        }
    }


}
