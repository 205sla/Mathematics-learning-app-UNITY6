using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using Debug = UnityEngine.Debug;
using System;

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
            //����
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
            
            SetQuestion(); //���� ����
            TopBar.GetComponent<TopBar>().SetProgress((float)(questionNum + 1) / (float)Questions.Count * 100);
            MainContent.GetComponent<MainContent>().MoveMainContent(2000, 0, 0.5f);

            //���� �Է� �Ҷ����� ��ٸ�
            answer = "";
            yield return new WaitUntil(() => answer != "");

            //���� ��ư ���̰� ���� ��ư ���������� ��ٸ�
            ResultBoard.GetComponent<ResultBoard>().SetBtn("����", -100, 0.5f);
            yield return new WaitForSeconds(0.5f);

            IsInputAnswer = false;
            yield return new WaitUntil(() => IsInputAnswer);

            //��� ��ư ���̰� ���� ��ư ���������� ��ٸ�//SetCorrectAnswer
            SetResult();
            ResultBoard.GetComponent<ResultBoard>().SetBtn("Ȯ��", 500, 0.5f);
            yield return new WaitForSeconds(0.5f);

            IsInputAnswer = false;
            yield return new WaitUntil(() => IsInputAnswer);


            //�����ư ����� 
            ResultBoard.GetComponent<ResultBoard>().SetBtn("����", -500, 0.5f);

            //��������
            MainContent.GetComponent<MainContent>().MoveMainContent(0, -2000, 0.5f);
            yield return new WaitForSeconds(0.5f);

            QuizComplete();


            //������ ���� ������?
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
                    //�޺��ε���
                    Debug.Log("�޺� ����");
                    SetCombo();
                    MainContent.GetComponent<MainContent>().MoveMainContent(2000, 0, 0.5f);
                    yield return new WaitForSeconds(0.5f);


                    ResultBoard.GetComponent<ResultBoard>().SetBtn("�޺�", -100, 0.5f);
                    yield return new WaitForSeconds(0.5f);

                    IsInputAnswer = false;
                    yield return new WaitUntil(() => IsInputAnswer);

                    ResultBoard.GetComponent<ResultBoard>().SetBtn("����", -500, 0.5f);
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
        missedQuestions.Insert(0, new List<string> { DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") });

        if (ES3.KeyExists("WrongProblemData"))
        {
            // ���� ������ �ε�
            existingData = ES3.Load<List<List<List<string>>>>("WrongProblemData");
            Debug.Log("���� ������ �ε� �Ϸ�.");
        }
        existingData.Add(missedQuestions);

        if (existingData.Count > 10)
        {
            existingData.RemoveAt(0);
            Debug.Log("�����Ͱ� 10���� �ʰ��Ͽ� ���� ������ �����Ͱ� �����Ǿ����ϴ�.");
        }
        


        ES3.Save("WrongProblemData", existingData);
    }

    void SetCombo()
    {
        Debug.Log("�޺� ���ٴ�: "+ comboCount);
        Combo.SetActive(true);
        Combo.GetComponent<Combo>().SetCombo(comboCount.ToString(), ComplimentGenerator.GetRandomCombo(comboCount));
    }

    void SetQuestion()
    {
        PrintList(Questions[questionNum]);


        Debug.Log("��¥ �̸� ���� ���� �˷��ش�." + Questions[questionNum][5]);
        switch (Questions[questionNum][0]) //  Questions[questionNum][0]
        {
            case "OX":
                // "OX"�� �� ������ �ڵ�
                Debug.Log("OX Ÿ�� ���� ó��: ");
                OXQuiz.SetActive(true);
                OXQuiz.GetComponent<OXQuiSet>().SetQuestion(Questions[questionNum][4]);
                break;

            case "SA":
                // "SA"�� �� ������ �ڵ�
                Debug.Log("SA Ÿ�� ���� ó��");

                SAQuiz.SetActive(true);
                SAQuiz.GetComponent<SAQuizSet>().SetQuestion(Questions[questionNum][4], Questions[questionNum][6]);
                break;

            case "SO":
                // "SO"�� �� ������ �ڵ�
                Debug.Log("SO Ÿ�� ���� ó��");
                SOQuiz.SetActive(true);
                SOQuiz.GetComponent<SOQuizSet>().SetQuestion(Questions[questionNum][4]);
                SOQuiz.GetComponent<SOQuizSet>().SetOption(new string[] { Questions[questionNum][5], Questions[questionNum][6], Questions[questionNum][7], Questions[questionNum][8], Questions[questionNum][9] });

                break;

            case "SM":
                // "SM"�� �� ������ �ڵ�
                SMQuiz.SetActive(true);
                SMQuiz.GetComponent<SMQuizSet>().SetQuestion(Questions[questionNum][4]);
                SMQuiz.GetComponent<SMQuizSet>().SetOption(new string[] { Questions[questionNum][6], Questions[questionNum][7], Questions[questionNum][8], Questions[questionNum][9], Questions[questionNum][10] });
                // SM ó�� ���� �߰�
                break;

            default:
                // ���� ó��: ����ġ ���� ���� ��
                Debug.LogWarning("�� �� ���� ���� Ÿ��: " + Questions[questionNum][0]);
                GameManager.instance.LoadScene();
                break;
        }
    }




    void SetResult()
    {
        Debug.Log("������" + Questions[questionNum][0]);


        switch (Questions[questionNum][0]) //  Questions[questionNum][0]
        {

            case "OX":
                // "OX"�� �� ������ �ڵ�
                correctAnswer = GameManager.instance.Normalization(Questions[questionNum][5]);
                if (answer == correctAnswer)
                {
                    showTxt = "�����Դϴ�!";
                    ProcessCorrectAnswer();
                }
                else
                {
                    showTxt = "�����Դϴ�!\n ������ " + correctAnswer + " �Դϴ�.";
                    ProcessIncorrectAnswer();
                }
                Debug.Log("OX Ÿ�� ���� ó��");
                
                break;

            case "SA":
                // "SA"�� �� ������ �ڵ� correctAnswer = GameManager.instance.Normalization(Questions[questionNum][5]);

                Debug.Log("�����̸���");
                correctAnswer = GameManager.instance.Normalization(Questions[questionNum][5]);
                Debug.Log("���� ������: " + correctAnswer);
                Debug.Log("�Էµ� ������: " + answer);
                if (IsAnswerCorrect(correctAnswer, answer))
                {
                    showTxt = "�����Դϴ�!";
                    ProcessCorrectAnswer();
                }
                else
                {
                    showTxt = "�����Դϴ�!\n ������ " + Questions[questionNum][5] + " �Դϴ�.";
                    ProcessIncorrectAnswer();
                }
                break;

            case "SO":
                correctAnswer = Questions[questionNum][5];
                Debug.Log("���� ����: " + correctAnswer);
                Debug.Log("�亯 ����: " + answer);
                if (answer == correctAnswer)
                {
                    showTxt = "�����Դϴ�!";
                    ProcessCorrectAnswer();
                }
                else
                {
                    showTxt = "�����Դϴ�!\n ������ " + correctAnswer + " �Դϴ�.";
                    ProcessIncorrectAnswer();
                }
                Debug.Log("SO Ÿ�� ���� ó��");
                break;

            case "SM":
                correctAnswer = GameManager.instance.Normalization(Questions[questionNum][5]); Debug.Log(correctAnswer);
                Debug.Log("���� ����: " + correctAnswer);
                if (answer == correctAnswer)
                {
                    showTxt = "�����Դϴ�!";
                    ProcessCorrectAnswer();
                }
                else
                {
                    showTxt = "�����Դϴ�!\n ������ " + correctAnswer + " �Դϴ�.";
                    ProcessIncorrectAnswer();
                }
                Debug.Log("SM Ÿ�� ���� ó��");
                break;

            default:
                // ���� ó��: ����ġ ���� ���� ��
                Debug.LogWarning("�� �� ���� ���� Ÿ��: " + Questions[questionNum][0]);
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

    void ProcessCorrectAnswer() //����
    {
        //����

        comboCount += 1;
        maxCombo = maxCombo > comboCount ? maxCombo : comboCount;

        Debug.Log("�޺�: " + comboCount.ToString());

        //Ǭ Ƚ���� 1 ���ϱ�
        // �ڿ��� �� ��° ���� �������� (���� Ǭ Ƚ��)
        string secondLastElement = Questions[questionNum][Questions[questionNum].Count - 2];

        // �ڿ��� �� ��° ���Ҹ� int�� ��ȯ�Ͽ� 1 ���ϱ�
        int secondLastNumber = int.Parse(secondLastElement);
        secondLastNumber += 1;

        // 1 ���� ���� �ٽ� string���� ��ȯ�Ͽ� �����
        Questions[questionNum][Questions[questionNum].Count - 2] = secondLastNumber.ToString();


    }

    void ProcessIncorrectAnswer()  //����
    {
        incorrectQuestionCount += 1;
        comboCount = 0;
        lifeCount -= 1;
        TopBar.GetComponent<TopBar>().SetHeartCount(lifeCount);

        //Ǭ Ƚ���� Ʋ�� Ƚ�� ��� ���ϱ�
        // �ڿ��� �� ��° ���� ��������
        string secondLastElement = Questions[questionNum][Questions[questionNum].Count - 2];

        // ������ ���� ��������
        string lastElement = Questions[questionNum][Questions[questionNum].Count - 1];

        // �� ���Ҹ� int�� ��ȯ�Ͽ� ���� 1 ���ϱ�
        int secondLastNumber = int.Parse(secondLastElement);
        secondLastNumber += 1;

        int lastNumber = int.Parse(lastElement);
        lastNumber += 1;

        // 1 ���� ���� �ٽ� string���� ��ȯ�Ͽ� ���� �����
        Questions[questionNum][Questions[questionNum].Count - 2] = secondLastNumber.ToString();
        Questions[questionNum][Questions[questionNum].Count - 1] = lastNumber.ToString();

        //���� ����Ʈ�� �ֱ�
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
                // ���� ó��: ����ġ ���� ���� ��
                Debug.LogWarning("�� �� ���� ���� Ÿ��: " + Questions[questionNum][0]);
                GameManager.instance.LoadScene();
                break;
        }
    }

    void PrintList(List<string> list)
    {
        Debug.Log("���� ��� ������!");
        foreach (string item in list)
        {
            Debug.Log(item);
        }
    }

    // ����� �亯�� ���ϴ� �Լ�
    bool IsAnswerCorrect(string answer, string correctAnswer) 
    {
        // 1. �亯�� ���信�� �ѱ�, ���ĺ�, ����, -�� �����
        string processedAnswer = CleanString(answer);
        string processedCorrectAnswer = CleanString(correctAnswer);

        // 2. ��ҹ��� ���� ���� (�ҹ��ڷ� ��ȯ)
        processedAnswer = processedAnswer.ToLower();
        processedCorrectAnswer = processedCorrectAnswer.ToLower();

        // 3. �亯�� ���� �ѱ��̸� ������ ���� ���� �� ��
        if (IsLastCharacterKorean(processedAnswer))
        {
            processedAnswer = processedAnswer.Substring(0, processedAnswer.Length - 1);
        }

        // �� �� ��� ��ȯ
        return processedAnswer == processedCorrectAnswer;
    }

    // �ѱ�, ���ĺ�, ����, -�� ����� �Լ�
    private static string CleanString(string input)
    {
        return Regex.Replace(input, "[^��-�Ra-zA-Z0-9-]", "");
    }

    // ���ڿ� ������ ���ڰ� �ѱ����� Ȯ���ϴ� �Լ�
    private static bool IsLastCharacterKorean(string input)
    {
        return input.Length > 0 && Regex.IsMatch(input[input.Length - 1].ToString(), @"[\uac00-\ud7af]");
    }
}