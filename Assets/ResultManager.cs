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
        Debug.Log("결과창 시작");
        StartCoroutine(BtnShow());
    }

    void ResultShow()
    {
        // Sequence 생성
        Sequence sequence = DOTween.Sequence();

        // 0.2초 대기
        sequence.AppendInterval(0.2f);

        // RectTransform의 로컬 Y 좌표를 2000에서 1200으로 이동
        sequence.Append(Text1.DOLocalMoveY(1200f, 0.3f).SetEase(Ease.InOutQuad));

        // comment 텍스트의 투명도를 0에서 1로 점점 보이게 설정
        sequence.Append(comment.DOFade(1f, 0.5f).SetEase(Ease.InOutQuad));

        // 애니메이션 실행
        sequence.Play();
    }

    IEnumerator BtnShow()
    {
        ResultBoard.GetComponent<ResultBoard>().SetBtn("확인", -100, 0.5f);
        yield return new WaitForSeconds(0.5f);

        IsInputAnswer = false;
        yield return new WaitUntil(() => IsInputAnswer);

        ResultBoard.GetComponent<ResultBoard>().SetBtn("좋네요", 500, 0.5f);
        yield return new WaitForSeconds(0.5f);


        IsInputAnswer = false;
        yield return new WaitUntil(() => IsInputAnswer);

        ResultBoard.GetComponent<ResultBoard>().SetBtn("오예", -500, 0.5f);
        yield return new WaitForSeconds(0.5f);

        GameManager.instance.LoadScene("Title");
    }

    string AddNewLineAfterPunctuation(string input)
    {
        // !, , 또는 . 뒤에 \n을 추가
        string pattern = @"([!,\.])";  // !, , 또는 .을 찾아서
        string replacement = "$1\n";    // 해당 문자를 찾고 그 뒤에 \n을 추가

        // Replace 실행
        return Regex.Replace(input, pattern, replacement);
    }

    public void ResultButton()
    {
        IsInputAnswer = true;

    }
}
