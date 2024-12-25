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
            //����
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
            SetQuestion(); //���� ����
            TopBar.GetComponent<TopBar>().SetProgress((float)(questionNum+1) / (float)Questions.Count*100);
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
            ResultBoard.GetComponent<ResultBoard>().SetBtn("����!", -500, 0.5f);

            //��������
            MainContent.GetComponent<MainContent>().MoveMainContent(0, -2000, 0.5f);
            yield return new WaitForSeconds(0.5f);
            //�޺�?


            QuizComplete();
        }


    }

    void SetQuestion()
    {

        switch ("SA") //  Questions[questionNum][0]
        {
            case "OX":
                // "OX"�� �� ������ �ڵ�
                Debug.Log("OX Ÿ�� ���� ó��");
                OXQuiz.SetActive(true);
                OXQuiz.GetComponent<OXQuiSet>().SetQuestion(Questions[questionNum][4]);
                break;

            case "SA":
                // "SA"�� �� ������ �ڵ�
                Debug.Log("SA Ÿ�� ���� ó��");
                
                SAQuiz.SetActive(true);
                SAQuiz.GetComponent<SAQuizSet>().SetQuestion(Questions[questionNum][4], "1,2");
                break;

            case "SO":
                // "SO"�� �� ������ �ڵ�
                Debug.Log("SO Ÿ�� ���� ó��");
                // SO ó�� ���� �߰�
                break;

            case "SM":
                // "SM"�� �� ������ �ڵ�
                Debug.Log("SM Ÿ�� ���� ó��");
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
        string showTxt = "";
        string correctAnswer = "";

        switch ("SA") //  Questions[questionNum][0]
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
                correctAnswer = GameManager.instance.Normalization(Questions[questionNum][5]);
                Debug.Log(correctAnswer);
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
                Debug.Log("SA Ÿ�� ���� ó��");
                break;

            case "SO":
                // "SO"�� �� ������ �ڵ�
                Debug.Log("SO Ÿ�� ���� ó��");
                // SO ó�� ���� �߰�
                break;

            case "SM":
                // "SM"�� �� ������ �ڵ�
                Debug.Log("SM Ÿ�� ���� ó��");
                // SM ó�� ���� �߰�
                break;

            default:
                // ���� ó��: ����ġ ���� ���� ��
                Debug.LogWarning("�� �� ���� ���� Ÿ��: " + Questions[questionNum][0]);
                GameManager.instance.LoadScene();
                break;
        }
        ResultBoard.GetComponent<ResultBoard>().SetCorrectAnswer(showTxt);
    }



    public void InputAnswer(string ans)
    {
        answer = GameManager.instance.Normalization(ans);
        Debug.Log($"�亯 ����: {answer}");
    }


    public void ResultButton()
    {
        IsInputAnswer = true;

    }

    void ProcessCorrectAnswer() //����
    {
        //����
        comboCount += 1;
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
                // ���� ó��: ����ġ ���� ���� ��
                Debug.LogWarning("�� �� ���� ���� Ÿ��: " + Questions[questionNum][0]);
                GameManager.instance.LoadScene();
                break;
        }
    }

}


/*///////////////////////////////////////////////////////////////////////////////////////////////
 ��Ʈ
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OXQuizManager : MonoBehaviour
{
    public Text questionText;  // ������ ������ �ؽ�Ʈ UI
    public Text resultText;    // ����� ������ �ؽ�Ʈ UI
    public Button buttonO;     // O ��ư
    public Button buttonX;     // X ��ư

    private int currentQuestionIndex = 0;
    private int score = 0;

    // ���� ������ (����, ����)
    private string[] questions = {
        "���Դ�� ������ �ظ鿡 ������ ������� �߶� ����� �� �ٸ�ü �� ������ ������ �������̴�.",
        "2�� Ȧ���̴�.",
        "������ �¾��� �߽����� �����Ѵ�.",
        "���� ����, ����, �����̴�.",
        "�ĸ��� �̱��� �����̴�."
    };

    private bool[] answers = { false, false, true, true, false }; // ������ ������ ���� (O: true, X: false)

    void Start()
    {
        buttonO.onClick.AddListener(() => Answer(true));
        buttonX.onClick.AddListener(() => Answer(false));

        StartCoroutine(QuizCoroutine());
    }

    // �ڷ�ƾ���� ������ �ϳ��� Ǯ��� ���
    IEnumerator QuizCoroutine()
    {
        for (int i = 0; i < questions.Length; i++)
        {
            questionText.text = questions[i];
            resultText.text = "";  // ���� ���� ��� �ʱ�ȭ

            // ������� �Է��� ��ٸ�
            yield return new WaitUntil(() => resultText.text != "");

            // ���� ���� �� ��� ��ٸ�
            yield return new WaitForSeconds(1f);
        }

        // ���� ���� �� ���� ���� ǥ��
        questionText.text = "���� �Ϸ�!";
        resultText.text = "����: " + score + "/" + questions.Length;
    }

    // ������ ó���ϴ� �Լ�
    void Answer(bool userAnswer)
    {
        if (userAnswer == answers[currentQuestionIndex])
        {
            score++;
            resultText.text = "�¾ҽ��ϴ�!";
        }
        else
        {
            resultText.text = "Ʋ�Ƚ��ϴ�.";
        }

        // ���� �ε��� ����
        currentQuestionIndex++;

        // ���� ������ �Ѿ �� �ֵ���
        if (currentQuestionIndex < questions.Length)
        {
            questionText.text = questions[currentQuestionIndex];
        }
    }
}

*/