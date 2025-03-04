using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using Debug = UnityEngine.Debug;
using System;
using TMPro;
using System.Linq;

public class SolvingQuestionsManager : MonoBehaviour
{
    public List<List<string>> Questions = new List<List<string>>();
    public List<List<string>> missedQuestions = new List<List<string>>();
    public int questionNum = 0;
    public string answer = "";
    public bool IsInputAnswer = false;

    public int comboCount = 0, lifeCount = 5, incorrectQuestionCount = 0, maxCombo = 0;

    [SerializeField]
    GameObject TopBar, MainContent, ResultBoard, OXQuiz, SAQuiz, SOQuiz, SMQuiz, Combo;

    [SerializeField]
    ComplimentGenerator ComplimentGenerator;

    [SerializeField]
    TMP_Text testTXT;


    string showTxt = "";
    string correctAnswer = "";
    bool firstWrong = true;
    float startTime = 0;
    private void Awake()
    {
        firstWrong = true;
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
        startTime = Time.time;
        StartCoroutine(CourseProgress());
    }

    IEnumerator CourseProgress()
    {
        questionNum = -1;
        while (true)
        {
            questionNum += 1;
            
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
            ResultBoard.GetComponent<ResultBoard>().SetBtn("오예", -500, 0.5f);

            //다음으로
            MainContent.GetComponent<MainContent>().MoveMainContent(0, -2000, 0.5f);
            yield return new WaitForSeconds(0.5f);

            QuizComplete();


            //마지막 문제 였나요?
            if (questionNum +1 == Questions.Count)
            {
                if (missedQuestions.Count != 0)
                {
                    foreach (var q in missedQuestions)
                    {
                        Questions.Add(q);
                    }

                    if (firstWrong)
                    {
                        SaveWrongProblem();
                    }

                    
                    missedQuestions.Clear();
                }
                else
                {
                    if (firstWrong)
                    {
                        SaveWrongProblem();
                    }
                    EndProblemCourse();
                    yield return new WaitUntil(() => false);
                }
            }
            else
            {

                if (comboCount == 5 || comboCount == 10 || comboCount == 15)
                {
                    //콤보인데요
                    Debug.Log("콤보 접근");
                    SetCombo();
                    MainContent.GetComponent<MainContent>().MoveMainContent(2000, 0, 0.5f);
                    yield return new WaitForSeconds(0.5f);


                    ResultBoard.GetComponent<ResultBoard>().SetBtn("콤보", -100, 0.5f);
                    yield return new WaitForSeconds(0.5f);

                    IsInputAnswer = false;
                    yield return new WaitUntil(() => IsInputAnswer);

                    ResultBoard.GetComponent<ResultBoard>().SetBtn("오예", -500, 0.5f);
                    yield return new WaitForSeconds(0.5f);

                    MainContent.GetComponent<MainContent>().MoveMainContent(0, -2000, 0.5f);
                    yield return new WaitForSeconds(0.5f);

                    Combo.SetActive(false);


                }
            }
        }
    }
    void EndProblemCourse()
    {
        Dictionary<string, string> ProblemCourseResults = new Dictionary<string, string>();
        ProblemCourseResults.Add("time", ((int)(Time.time - startTime)).ToString());
        ProblemCourseResults.Add("comment", ComplimentGenerator.GetAccuracyMessage(Questions.Count, incorrectQuestionCount));
        ProblemCourseResults.Add("percentage", ((int)((float)(Questions.Count-incorrectQuestionCount) / (float)Questions.Count * 100f)).ToString());
        ProblemCourseResults.Add("maxCombo", maxCombo.ToString());

        

        //GetRandomCombo(comboCount)
        ES3.Save("ProblemCourseResults", ProblemCourseResults);
        GameManager.instance.LoadScene("Result");
    }


    void SaveWrongProblem()
    {//missedQuestions
        firstWrong = false;
        List<List<List<string>>> existingData = new List<List<List<string>>>();
        missedQuestions.Insert(0, new List<string> { DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ES3.Load<string>("course") });

        if (ES3.KeyExists("WrongProblemData"))
        {
            // 기존 데이터 로드
            existingData = ES3.Load<List<List<List<string>>>>("WrongProblemData");
            Debug.Log("기존 데이터 로드 완료.");
        }
        existingData.Add(missedQuestions);

        if (existingData.Count > 10)
        {
            existingData.RemoveAt(0);
            Debug.Log("데이터가 10개를 초과하여 가장 오래된 데이터가 삭제되었습니다.");
        }
        


        ES3.Save("WrongProblemData", existingData);
    }

    void SetCombo()
    {
        Debug.Log("콤보 접근는: "+ comboCount);
        Combo.SetActive(true);
        Combo.GetComponent<Combo>().SetCombo(comboCount.ToString(), ComplimentGenerator.GetRandomCombo(comboCount));
    }

    void SetQuestion()
    {
        PrintList(Questions[questionNum]);


        Debug.Log("진짜 미리 정답 내가 알려준다." + Questions[questionNum][5]);
        switch (Questions[questionNum][0]) //  Questions[questionNum][0]
        {
            case "OX":
                // "OX"일 때 실행할 코드
                Debug.Log("OX 타입 질문 처리: ");
                OXQuiz.SetActive(true);
                OXQuiz.GetComponent<OXQuiSet>().SetQuestion(Questions[questionNum][4]);
                break;

            case "SA":
                // "SA"일 때 실행할 코드
                Debug.Log("SA 타입 질문 처리");

                SAQuiz.SetActive(true);
                SAQuiz.GetComponent<SAQuizSet>().SetQuestion(Questions[questionNum][4], Questions[questionNum][6]);
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
        Debug.Log("마스터" + Questions[questionNum][0]);


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

                Debug.Log("너의이름은");
                correctAnswer = GameManager.instance.Normalization(Questions[questionNum][5]);
                Debug.Log("정답 스포는: " + correctAnswer);
                Debug.Log("입력된 스포는: " + answer);
                testTXT.text = "정답은:'" + correctAnswer + "'(" + correctAnswer.Count() + ")'\n입력된 문자는:" + answer + "'(" + answer.Count() + ")'\n단순 비교:"+ (correctAnswer== answer)+"\n보정 비교:"+ IsAnswerCorrect(correctAnswer, answer);
                if (IsAnswerCorrect(correctAnswer, answer))
                {
                    showTxt = "정답입니다!";
                    ProcessCorrectAnswer();
                }
                else
                {
                    showTxt = "오답입니다!\n 정답은 " + Questions[questionNum][5] + " 입니다.";
                    ProcessIncorrectAnswer();
                }
                break;

            case "SO":
                correctAnswer = Questions[questionNum][5];
                Debug.Log("정답 스포: " + correctAnswer);
                Debug.Log("답변 스포: " + answer);
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
                Debug.Log("정답 스포: " + correctAnswer);
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




    public void InputAnswer(string ans, bool Normalization = true)
    {
        if (Normalization)
        {
            answer = GameManager.instance.Normalization(ans);
        }
        else
        {
            answer = ans;
        }
    }



    public void ResultButton()
    {
        IsInputAnswer = true;

    }

    void ProcessCorrectAnswer() //정답
    {
        //점수

        comboCount += 1;
        maxCombo = maxCombo > comboCount ? maxCombo : comboCount;

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
        incorrectQuestionCount += 1;
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

    void PrintList(List<string> list)
    {
        Debug.Log("문제 출력 해줘잉!");
        foreach (string item in list)
        {
            Debug.Log(item);
        }
    }

    // 정답과 답변을 비교하는 함수
    bool IsAnswerCorrect(string answer, string correctAnswer) 
    {
        if(answer== correctAnswer)
        {
            return true;
        }
        // 1. 답변과 정답에서 한글, 알파벳, 숫자, -만 남기기
        string processedAnswer = CleanString(answer);
        string processedCorrectAnswer = CleanString(correctAnswer);

        // 2. 대소문자 구분 없음 (소문자로 변환)
        processedAnswer = processedAnswer.ToLower();
        processedCorrectAnswer = processedCorrectAnswer.ToLower();

        // 3. 답변의 끝이 한글이면 마지막 글자 제거 후 비교
        if (IsLastCharacterKorean(processedAnswer))
        {
            processedAnswer = processedAnswer.Substring(0, processedAnswer.Length - 1);
        }

        // 비교 후 결과 반환
        return processedAnswer == processedCorrectAnswer;
    }

    // 한글, 알파벳, 숫자, -만 남기는 함수
    private static string CleanString(string input)
    {
        return Regex.Replace(input, "[^가-힣a-zA-Z0-9-]", "");
    }

    // 문자열 마지막 문자가 한글인지 확인하는 함수
    private static bool IsLastCharacterKorean(string input)
    {
        return input.Length > 0 && Regex.IsMatch(input[input.Length - 1].ToString(), @"[\uac00-\ud7af]");
    }
}