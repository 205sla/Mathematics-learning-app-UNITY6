using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    private GameObject GameObjectManager;

    private float lastPressTime = 0f; // 마지막 버튼 누른 시간
    private float doublePressTimeLimit = 5f; // 두 번 누를 수 있는 시간 제한 (5초)
    private int pressCount = 0; // 뒤로가기 버튼 누른 횟수


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("씬에 두개 이상의 게임 매니저가 존재합니다!");
            Destroy(gameObject);
        }
    }



    private void Update()
    {
        // 뒤로가기 버튼이 눌렸을 때
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
        if (SceneManager.GetActiveScene().name == "LearningAnalytics")
        {
            LoadScene("RecordProblem");
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
            LoadScene("Title");
            return;
        }


        // 마지막 버튼 눌린 시간이 현재 시간과 5초 차이 이내라면
        if (Time.time - lastPressTime <= doublePressTimeLimit)
        {
            pressCount++; // 버튼 누른 횟수 증가
        }
        else
        {
            pressCount = 1; // 시간 초과하면 횟수 초기화하고 현재 버튼을 첫 번째로 간주
        }

        lastPressTime = Time.time; // 현재 시간을 마지막 버튼 눌린 시간으로 설정

        // 두 번째 뒤로가기 버튼 눌렀을 때 프로그램 종료
        if (pressCount == 2)
        {
            if (SceneManager.GetActiveScene().name == "SolvingQuestions")
            {
                LoadScene("Title");
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
        UnityEditor.EditorApplication.isPlaying = false; //play모드를 false로.
#elif UNITY_WEBPLAYER
        Application.OpenURL("http://google.com"); //구글웹으로 전환
#else
        Application.Quit(); //어플리케이션 종료
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
        // 숫자, 한글, 알파벳과 - 기호를 제외한 모든 문자를 제거하는 정규식
        string pattern = @"[^가-힣a-zA-Z0-9\-]";  // 한글, 알파벳, 숫자, - 외의 문자
        string result = Regex.Replace(input, pattern, "");
        return Regex.Replace(result.ToLower(), @"\u200B", "");
    }






}
