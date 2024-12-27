using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    private GameObject GameObjectManager;

    private float lastPressTime = 0f; // ������ ��ư ���� �ð�
    private float doublePressTimeLimit = 5f; // �� �� ���� �� �ִ� �ð� ���� (5��)
    private int pressCount = 0; // �ڷΰ��� ��ư ���� Ƚ��


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("���� �ΰ� �̻��� ���� �Ŵ����� �����մϴ�!");
            Destroy(gameObject);
        }
    }



    private void Update()
    {
        // �ڷΰ��� ��ư�� ������ ��
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Escape();
        }
    }


    private void Escape()
    {
        if (SceneManager.GetActiveScene().name == "SelectProblem")
        {
            LoadScene("Title");
            return;
        }
        if (SceneManager.GetActiveScene().name == "RecordProblem")
        {
            LoadScene("Title");
            return;
        }
        if (SceneManager.GetActiveScene().name == "Setting")
        {
            LoadScene("Title");
            return;
        }
        if (SceneManager.GetActiveScene().name == "Result")
        {
            LoadScene("Title");
            return;
        }
        if (SceneManager.GetActiveScene().name == "MakingCourse")
        {
            LoadScene("SelectProblem");
            return;
        }


        // ������ ��ư ���� �ð��� ���� �ð��� 5�� ���� �̳����
        if (Time.time - lastPressTime <= doublePressTimeLimit)
        {
            pressCount++; // ��ư ���� Ƚ�� ����
        }
        else
        {
            pressCount = 1; // �ð� �ʰ��ϸ� Ƚ�� �ʱ�ȭ�ϰ� ���� ��ư�� ù ��°�� ����
        }

        lastPressTime = Time.time; // ���� �ð��� ������ ��ư ���� �ð����� ����

        // �� ��° �ڷΰ��� ��ư ������ �� ���α׷� ����
        if (pressCount == 2)
        {
            if (SceneManager.GetActiveScene().name == "SolvingQuestions")
            {
                LoadScene("SelectProblem");
                return;
            }
            else
            {
                Quit();
            }
        }

    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //play��带 false��.
#elif UNITY_WEBPLAYER
        Application.OpenURL("http://google.com"); //���������� ��ȯ
#else
        Application.Quit(); //���ø����̼� ����
#endif
    }

    public void FindMe()
    {
        if (SceneManager.GetActiveScene().name == "Title")
        {
            GameObjectManager = GameObject.Find("GameObjectManager");
        }
    }

    public void LoadScene(string name = "Error")
    {
        SceneManager.LoadScene(name);
    }


    public void LogoShowCompleted()
    {
        GameObjectManager.GetComponent<GameObjectManager>().ReadQuestionManager.GetComponent<ReadQuestionManager>().RoadManagerStart();
    }

    public void ShowButton()
    {
        GameObjectManager.GetComponent<GameObjectManager>().ShowButton();
    }

    public string Normalization(string input)
    {
        // ����, �ѱ�, ���ĺ��� - ��ȣ�� ������ ��� ���ڸ� �����ϴ� ���Խ�
        string pattern = @"[^��-�Ra-zA-Z0-9\-]";  // �ѱ�, ���ĺ�, ����, - ���� ����
        string result = Regex.Replace(input, pattern, "");
        return Regex.Replace(result.ToLower(), @"\u200B", "");
    }






}
