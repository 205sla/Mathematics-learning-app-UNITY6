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
            TopBar.GetComponent<TopBar>().SetProgress((float)(questionNum + 1) / (float)Questions.Count * 100);
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

        switch (Questions[questionNum][0]) //  Questions[questionNum][0]
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
                SOQuiz.SetActive(true);
                SOQuiz.GetComponent<SOQuizSet>().SetQuestion(Questions[questionNum][4]);
                SOQuiz.GetComponent<SOQuizSet>().SetOption(new string[] { Questions[questionNum][5], Questions[questionNum][6], Questions[questionNum][7], Questions[questionNum][8], Questions[questionNum][9] });

                break;

            case "SM":
                // "SM"일 때 실행할 코드
                SMQuiz.SetActive(true);
                SMQuiz.GetComponent<SMQuizSet>().SetQuestion(Questions[questionNum][4]);
                SMQuiz.GetComponent<SMQuizSet>().SetOption(new string[] { Questions[questionNum][6], Questions[questionNum][7], Questions[questionNum][8], Questions[questionNum][9], Questions[questionNum][10] });
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

        switch (Questions[questionNum][0]) //  Questions[questionNum][0]
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
                correctAnswer = Questions[questionNum][5];
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
                Debug.Log("SO 타입 질문 처리");
                break;

            case "SM":
                correctAnswer = GameManager.instance.Normalization(Questions[questionNum][5]); Debug.Log(correctAnswer);
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
                Debug.Log("SM 타입 질문 처리");
                break;

            default:
                // 예외 처리: 예상치 못한 값일 때
                Debug.LogWarning("알 수 없는 질문 타입: " + Questions[questionNum][0]);
                GameManager.instance.LoadScene();
                break;
        }
        ResultBoard.GetComponent<ResultBoard>().SetCorrectAnswer(showTxt);
    }



    public void InputAnswer(string ans, bool Normalization=true )
    {
        if (Normalization)
        {
            answer = GameManager.instance.Normalization(ans);
        }
        else
        {
            answer = ans;
        }
        
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
        switch (Questions[questionNum][0]) //  Questions[questionNum][0]
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