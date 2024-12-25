using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolvingQuestionsManager : MonoBehaviour
{
    public List<List<string>> Questions = new List<List<string>>();
    public List<List<string>> missedQuestions = new List<List<string>>();
    public int questionNum = 0;
    public string answer = "";
    public bool IsInputAnswer = false;

    public int comboCount = 0, lifeCount = 5;

    [SerializeField]
    GameObject TopBar, MainContent, ResultBoard, OXQuiz, SAQuiz, SOQuiz, SMQuiz;
    private void Awake()
    {
        questionNum = 0;
        if (ES3.KeyExists("courseData"))
        {
            Questions = ES3.Load<List<List<string>>>("courseData");
        }
        else
        {
            //에러
            GameManager.instance.LoadScene();
        }
    }

    private void Start()
    {
        StartCoroutine(CourseProgress());
    }

    IEnumerator CourseProgress()
    {

        for (int i = 0; i < Questions.Count; i++)
        {
            questionNum = i;
            SetQuestion(); //문제 설정
            TopBar.GetComponent<TopBar>().SetProgress((float)(questionNum+1) / (float)Questions.Count*100);
            MainContent.GetComponent<MainContent>().MoveMainContent(2000, 0, 0.5f);

            //정답 입력 할때까지 기다림
            answer = "";
            yield return new WaitUntil(() => answer != "");

            //제출 버튼 보이고 제출 버튼 누를때까지 기다림
            ResultBoard.GetComponent<ResultBoard>().SetBtn("제출", -100, 0.5f);
            yield return new WaitForSeconds(0.5f);

            IsInputAnswer = false;
            yield return new WaitUntil(() => IsInputAnswer);

            //결과 버튼 보이고 제출 버튼 누를때까지 기다림//SetCorrectAnswer
            SetResult();
            ResultBoard.GetComponent<ResultBoard>().SetBtn("확인", 500, 0.5f);
            yield return new WaitForSeconds(0.5f);

            IsInputAnswer = false;
            yield return new WaitUntil(() => IsInputAnswer);


            //결과버튼 숨기며 
            ResultBoard.GetComponent<ResultBoard>().SetBtn("오예!", -500, 0.5f);

            //다음으로
            MainContent.GetComponent<MainContent>().MoveMainContent(0, -2000, 0.5f);
            yield return new WaitForSeconds(0.5f);
            //콤보?


            QuizComplete();
        }


    }

    void SetQuestion()
    {

        switch ("SA") //  Questions[questionNum][0]
        {
            case "OX":
                // "OX"일 때 실행할 코드
                Debug.Log("OX 타입 질문 처리");
                OXQuiz.SetActive(true);
                OXQuiz.GetComponent<OXQuiSet>().SetQuestion(Questions[questionNum][4]);
                break;

            case "SA":
                // "SA"일 때 실행할 코드
                Debug.Log("SA 타입 질문 처리");
                
                SAQuiz.SetActive(true);
                SAQuiz.GetComponent<SAQuizSet>().SetQuestion(Questions[questionNum][4], "1,2");
                break;

            case "SO":
                // "SO"일 때 실행할 코드
                Debug.Log("SO 타입 질문 처리");
                // SO 처리 로직 추가
                break;

            case "SM":
                // "SM"일 때 실행할 코드
                Debug.Log("SM 타입 질문 처리");
                // SM 처리 로직 추가
                break;

            default:
                // 예외 처리: 예상치 못한 값일 때
                Debug.LogWarning("알 수 없는 질문 타입: " + Questions[questionNum][0]);
                GameManager.instance.LoadScene();
                break;
        }
    }




    void SetResult()
    {
        string showTxt = "";
        string correctAnswer = "";

        switch ("SA") //  Questions[questionNum][0]
        {
            case "OX":
                // "OX"일 때 실행할 코드
                correctAnswer = GameManager.instance.Normalization(Questions[questionNum][5]);
                if (answer == correctAnswer)
                {
                    showTxt = "정답입니다!";
                    ProcessCorrectAnswer();
                }
                else
                {
                    showTxt = "오답입니다!\n 정답은 " + correctAnswer + " 입니다.";
                    ProcessIncorrectAnswer();
                }
                Debug.Log("OX 타입 질문 처리");
                break;

            case "SA":
                // "SA"일 때 실행할 코드 correctAnswer = GameManager.instance.Normalization(Questions[questionNum][5]);
                correctAnswer = GameManager.instance.Normalization(Questions[questionNum][5]);
                Debug.Log(correctAnswer);
                if (answer == correctAnswer)
                {
                    showTxt = "정답입니다!";
                    ProcessCorrectAnswer();
                }
                else
                {
                    showTxt = "오답입니다!\n 정답은 " + correctAnswer + " 입니다.";
                    ProcessIncorrectAnswer();
                }
                Debug.Log("SA 타입 질문 처리");
                break;

            case "SO":
                // "SO"일 때 실행할 코드
                Debug.Log("SO 타입 질문 처리");
                // SO 처리 로직 추가
                break;

            case "SM":
                // "SM"일 때 실행할 코드
                Debug.Log("SM 타입 질문 처리");
                // SM 처리 로직 추가
                break;

            default:
                // 예외 처리: 예상치 못한 값일 때
                Debug.LogWarning("알 수 없는 질문 타입: " + Questions[questionNum][0]);
                GameManager.instance.LoadScene();
                break;
        }
        ResultBoard.GetComponent<ResultBoard>().SetCorrectAnswer(showTxt);
    }



    public void InputAnswer(string ans)
    {
        answer = GameManager.instance.Normalization(ans);
        Debug.Log($"답변 고마워: {answer}");
    }


    public void ResultButton()
    {
        IsInputAnswer = true;

    }

    void ProcessCorrectAnswer() //정답
    {
        //점수
        comboCount += 1;
        Debug.Log("콤보: " + comboCount.ToString());

        //푼 횟수에 1 더하기
        // 뒤에서 두 번째 원소 가져오기 (문제 푼 횟수)
        string secondLastElement = Questions[questionNum][Questions[questionNum].Count - 2];

        // 뒤에서 두 번째 원소를 int로 변환하여 1 더하기
        int secondLastNumber = int.Parse(secondLastElement);
        secondLastNumber += 1;

        // 1 더한 값을 다시 string으로 변환하여 덮어쓰기
        Questions[questionNum][Questions[questionNum].Count - 2] = secondLastNumber.ToString();


    }

    void ProcessIncorrectAnswer()  //오답
    {

        comboCount = 0;
        lifeCount -= 1;
        TopBar.GetComponent<TopBar>().SetHeartCount(lifeCount);

        //푼 횟수랑 틀린 횟수 모두 더하기
        // 뒤에서 두 번째 원소 가져오기
        string secondLastElement = Questions[questionNum][Questions[questionNum].Count - 2];

        // 마지막 원소 가져오기
        string lastElement = Questions[questionNum][Questions[questionNum].Count - 1];

        // 두 원소를 int로 변환하여 각각 1 더하기
        int secondLastNumber = int.Parse(secondLastElement);
        secondLastNumber += 1;

        int lastNumber = int.Parse(lastElement);
        lastNumber += 1;

        // 1 더한 값을 다시 string으로 변환하여 각각 덮어쓰기
        Questions[questionNum][Questions[questionNum].Count - 2] = secondLastNumber.ToString();
        Questions[questionNum][Questions[questionNum].Count - 1] = lastNumber.ToString();

        //오답 리스트에 넣기
        missedQuestions.Add(Questions[questionNum]);

    }


    void QuizComplete()
    {
        switch ("SA") //  Questions[questionNum][0]
        {
            case "OX":
                OXQuiz.SetActive(false);
                break;

            case "SA":
                SAQuiz.SetActive(false);
                break;

            case "SO":
                SOQuiz.SetActive(false);

                break;

            case "SM":
                SMQuiz.SetActive(false);
                break;

            default:
                // 예외 처리: 예상치 못한 값일 때
                Debug.LogWarning("알 수 없는 질문 타입: " + Questions[questionNum][0]);
                GameManager.instance.LoadScene();
                break;
        }
    }

}


/*///////////////////////////////////////////////////////////////////////////////////////////////
 힌트
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OXQuizManager : MonoBehaviour
{
    public Text questionText;  // 문제를 보여줄 텍스트 UI
    public Text resultText;    // 결과를 보여줄 텍스트 UI
    public Button buttonO;     // O 버튼
    public Button buttonX;     // X 버튼

    private int currentQuestionIndex = 0;
    private int score = 0;

    // 문제 데이터 (문제, 정답)
    private string[] questions = {
        "각뿔대는 각뿔을 밑면에 평행한 평면으로 잘라서 생기는 두 다면체 중 각뿔을 제외한 나머지이다.",
        "2는 홀수이다.",
        "지구는 태양을 중심으로 공전한다.",
        "물은 무색, 무취, 무미이다.",
        "파리는 미국의 수도이다."
    };

    private bool[] answers = { false, false, true, true, false }; // 각각의 문제의 정답 (O: true, X: false)

    void Start()
    {
        buttonO.onClick.AddListener(() => Answer(true));
        buttonX.onClick.AddListener(() => Answer(false));

        StartCoroutine(QuizCoroutine());
    }

    // 코루틴으로 문제를 하나씩 풀어가는 방식
    IEnumerator QuizCoroutine()
    {
        for (int i = 0; i < questions.Length; i++)
        {
            questionText.text = questions[i];
            resultText.text = "";  // 이전 문제 결과 초기화

            // 사용자의 입력을 기다림
            yield return new WaitUntil(() => resultText.text != "");

            // 문제 종료 후 잠시 기다림
            yield return new WaitForSeconds(1f);
        }

        // 퀴즈 종료 후 최종 점수 표시
        questionText.text = "퀴즈 완료!";
        resultText.text = "점수: " + score + "/" + questions.Length;
    }

    // 정답을 처리하는 함수
    void Answer(bool userAnswer)
    {
        if (userAnswer == answers[currentQuestionIndex])
        {
            score++;
            resultText.text = "맞았습니다!";
        }
        else
        {
            resultText.text = "틀렸습니다.";
        }

        // 문제 인덱스 증가
        currentQuestionIndex++;

        // 다음 문제로 넘어갈 수 있도록
        if (currentQuestionIndex < questions.Length)
        {
            questionText.text = questions[currentQuestionIndex];
        }
    }
}

*/