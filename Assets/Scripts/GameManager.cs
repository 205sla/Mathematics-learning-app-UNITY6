using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    private GameObject GameObjectManager;

    private float lastPressTime = 0f; // ¸¶Áö¸· ¹öÆ° ´©¸¥ ½Ã°£
    private float doublePressTimeLimit = 5f; // µÎ ¹ø ´©¸¦ ¼ö ÀÖ´Â ½Ã°£ Á¦ÇÑ (5ÃÊ)
    private int pressCount = 0; // µÚ·Î°¡±â ¹öÆ° ´©¸¥ È½¼ö


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("¾À¿¡ µÎ°³ ÀÌ»óÀÇ °ÔÀÓ ¸Å´ÏÀú°¡ Á¸ÀçÇÕ´Ï´Ù!");
            Destroy(gameObject);
        }
    }



    private void Update()
    {
        // µÚ·Î°¡±â ¹öÆ°ÀÌ ´­·ÈÀ» ¶§
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


        // ¸¶Áö¸· ¹öÆ° ´­¸° ½Ã°£ÀÌ ÇöÀç ½Ã°£°ú 5ÃÊ Â÷ÀÌ ÀÌ³»¶ó¸é
        if (Time.time - lastPressTime <= doublePressTimeLimit)
        {
            pressCount++; // ¹öÆ° ´©¸¥ È½¼ö Áõ°¡
        }
        else
        {
            pressCount = 1; // ½Ã°£ ÃÊ°úÇÏ¸é È½¼ö ÃÊ±âÈ­ÇÏ°í ÇöÀç ¹öÆ°À» Ã¹ ¹øÂ°·Î °£ÁÖ
        }

        lastPressTime = Time.time; // ÇöÀç ½Ã°£À» ¸¶Áö¸· ¹öÆ° ´­¸° ½Ã°£À¸·Î ¼³Á¤

        // µÎ ¹øÂ° µÚ·Î°¡±â ¹öÆ° ´­·¶À» ¶§ ÇÁ·Î±×·¥ Á¾·á
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
        UnityEditor.EditorApplication.isPlaying = false; //play¸ðµå¸¦ false·Î.
#elif UNITY_WEBPLAYER
        Application.OpenURL("http://google.com"); //±¸±ÛÀ¥À¸·Î ÀüÈ¯
#else
        Application.Quit(); //¾îÇÃ¸®ÄÉÀÌ¼Ç Á¾·á
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
        // ¼ýÀÚ, ÇÑ±Û, ¾ËÆÄºª°ú - ±âÈ£¸¦ Á¦¿ÜÇÑ ¸ðµç ¹®ÀÚ¸¦ Á¦°ÅÇÏ´Â Á¤±Ô½Ä
        string pattern = @"[^°¡-ÆRa-zA-Z0-9\-]";  // ÇÑ±Û, ¾ËÆÄºª, ¼ýÀÚ, - ¿ÜÀÇ ¹®ÀÚ
        string result = Regex.Replace(input, pattern, "");
        return Regex.Replace(result.ToLower(), @"\u200B", "");
    }






}
