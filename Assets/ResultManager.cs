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
    {// Sequence 생성
        Sequence sequence = DOTween.Sequence();

        // 0.2초 대기
        sequence.AppendInterval(0.2f);

        // RectTransform의 로컬 Y 좌표를 2000에서 1200으로 이동
        sequence.Append(Text1.DOLocalMoveY(1200f, 0.3f).SetEase(Ease.InOutQuad));

        // comment 텍스트의 투명도를 0에서 1로 점점 보이게 설정
        sequence.Append(comment.DOFade(1f, 0.5f).SetEase(Ease.InOutQuad));

        // 0.2초 대기
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



        // 애니메이션 실행
        sequence.Play();
    }

    IEnumerator BtnShow()
    {
        yield return new WaitForSeconds(4f);
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

    public string FormatTime(string s)
    {
        // 문자열이 숫자로 변환 가능한지 확인
        if (int.TryParse(s, out int totalSeconds))
        {
            // 분과 초로 나누기
            int minutes = totalSeconds / 60;
            int seconds = totalSeconds % 60;

            // 2자리 숫자 형식으로 반환하기 위해 string.Format 사용
            return string.Format("{0:D2}:{1:D2}", minutes, seconds);
        }
        else
        {
            Debug.Log("이상한 시간이 들어왔어!!!:" + s);
            // 변환할 수 없는 경우 예외 처리 (혹은 디폴트 값 반환)
            return "00:00";  // 예시: 기본값을 "00:00"으로 설정
        }
    }


}
