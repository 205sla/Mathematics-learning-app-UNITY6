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
            //¿¡·¯
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
            
            SetQuestion(); //¹®Á¦ ¼³Á¤
            TopBar.GetComponent<TopBar>().SetProgress((float)(questionNum + 1) / (float)Questions.Count * 100);
            MainContent.GetComponent<MainContent>().MoveMainContent(2000, 0, 0.5f);

            //Á¤´ä ÀÔ·Â ÇÒ¶§±îÁö ±â´Ù¸²
            answer = "";
            yield return new WaitUntil(() => answer != "");

            //Á¦Ãâ ¹öÆ° º¸ÀÌ°í Á¦Ãâ ¹öÆ° ´©¸¦¶§±îÁö ±â´Ù¸²
            ResultBoard.GetComponent<ResultBoard>().SetBtn("Á¦Ãâ", -100, 0.5f);
            yield return new WaitForSeconds(0.5f);

            IsInputAnswer = false;
            yield return new WaitUntil(() => IsInputAnswer);

            //°á°ú ¹öÆ° º¸ÀÌ°í Á¦Ãâ ¹öÆ° ´©¸¦¶§±îÁö ±â´Ù¸²//SetCorrectAnswer
            SetResult();
            ResultBoard.GetComponent<ResultBoard>().SetBtn("È®ÀÎ", 500, 0.5f);
            yield return new WaitForSeconds(0.5f);

            IsInputAnswer = false;
            yield return new WaitUntil(() => IsInputAnswer);


            //°á°ú¹öÆ° ¼û±â¸ç 
            ResultBoard.GetComponent<ResultBoard>().SetBtn("¿À¿¹", -500, 0.5f);

            //´ÙÀ½À¸·Î
            MainContent.GetComponent<MainContent>().MoveMainContent(0, -2000, 0.5f);
            yield return new WaitForSeconds(0.5f);

            QuizComplete();


            //¸¶Áö¸· ¹®Á¦ ¿´³ª¿ä?
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
                    //ÄÞº¸ÀÎµ¥¿ä
                    Debug.Log("ÄÞº¸ Á¢±Ù");
                    SetCombo();
                    MainContent.GetComponent<MainContent>().MoveMainContent(2000, 0, 0.5f);
                    yield return new WaitForSeconds(0.5f);


                    ResultBoard.GetComponent<ResultBoard>().SetBtn("ÄÞº¸", -100, 0.5f);
                    yield return new WaitForSeconds(0.5f);

                    IsInputAnswer = false;
                    yield return new WaitUntil(() => IsInputAnswer);

                    ResultBoard.GetComponent<ResultBoard>().SetBtn("¿À¿¹", -500, 0.5f);
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
            // ±âÁ¸ µ¥ÀÌÅÍ ·Îµå
            existingData = ES3.Load<List<List<List<string>>>>("WrongProblemData");
            Debug.Log("±âÁ¸ µ¥ÀÌÅÍ ·Îµå ¿Ï·á.");
        }
        existingData.Add(missedQuestions);

        if (existingData.Count > 10)
        {
            existingData.RemoveAt(0);
            Debug.Log("µ¥ÀÌÅÍ°¡ 10°³¸¦ ÃÊ°úÇÏ¿© °¡Àå ¿À·¡µÈ µ¥ÀÌÅÍ°¡ »èÁ¦µÇ¾ú½À´Ï´Ù.");
        }
        


        ES3.Save("WrongProblemData", existingData);
    }

    void SetCombo()
    {
        Debug.Log("ÄÞº¸ Á¢±Ù´Â: "+ comboCount);
        Combo.SetActive(true);
        Combo.GetComponent<Combo>().SetCombo(comboCount.ToString(), ComplimentGenerator.GetRandomCombo(comboCount));
    }

    void SetQuestion()
    {
        PrintList(Questions[questionNum]);


        Debug.Log("ÁøÂ¥ ¹Ì¸® Á¤´ä ³»°¡ ¾Ë·ÁÁØ´Ù." + Questions[questionNum][5]);
        switch (Questions[questionNum][0]) //  Questions[questionNum][0]
        {
            case "OX":
                // "OX"ÀÏ ¶§ ½ÇÇàÇÒ ÄÚµå
                Debug.Log("OX Å¸ÀÔ Áú¹® Ã³¸®: ");
                OXQuiz.SetActive(true);
                OXQuiz.GetComponent<OXQuiSet>().SetQuestion(Questions[questionNum][4]);
                break;

            case "SA":
                // "SA"ÀÏ ¶§ ½ÇÇàÇÒ ÄÚµå
                Debug.Log("SA Å¸ÀÔ Áú¹® Ã³¸®");

                SAQuiz.SetActive(true);
                SAQuiz.GetComponent<SAQuizSet>().SetQuestion(Questions[questionNum][4], Questions[questionNum][6]);
                break;

            case "SO":
                // "SO"ÀÏ ¶§ ½ÇÇàÇÒ ÄÚµå
                Debug.Log("SO Å¸ÀÔ Áú¹® Ã³¸®");
                SOQuiz.SetActive(true);
                SOQuiz.GetComponent<SOQuizSet>().SetQuestion(Questions[questionNum][4]);
                SOQuiz.GetComponent<SOQuizSet>().SetOption(new string[] { Questions[questionNum][5], Questions[questionNum][6], Questions[questionNum][7], Questions[questionNum][8], Questions[questionNum][9] });

                break;

            case "SM":
                // "SM"ÀÏ ¶§ ½ÇÇàÇÒ ÄÚµå
                SMQuiz.SetActive(true);
                SMQuiz.GetComponent<SMQuizSet>().SetQuestion(Questions[questionNum][4]);
                SMQuiz.GetComponent<SMQuizSet>().SetOption(new string[] { Questions[questionNum][6], Questions[questionNum][7], Questions[questionNum][8], Questions[questionNum][9], Questions[questionNum][10] });
                // SM Ã³¸® ·ÎÁ÷ Ãß°¡
                break;

            default:
                // ¿¹¿Ü Ã³¸®: ¿¹»óÄ¡ ¸øÇÑ °ªÀÏ ¶§
                Debug.LogWarning("¾Ë ¼ö ¾ø´Â Áú¹® Å¸ÀÔ: " + Questions[questionNum][0]);
                GameManager.instance.LoadScene();
                break;
        }
    }




    void SetResult()
    {
        Debug.Log("¸¶½ºÅÍ" + Questions[questionNum][0]);


        switch (Questions[questionNum][0]) //  Questions[questionNum][0]
        {

            case "OX":
                // "OX"ÀÏ ¶§ ½ÇÇàÇÒ ÄÚµå
                correctAnswer = GameManager.instance.Normalization(Questions[questionNum][5]);
                if (answer == correctAnswer)
                {
                    showTxt = "Á¤´äÀÔ´Ï´Ù!";
                    ProcessCorrectAnswer();
                }
                else
                {
                    showTxt = "¿À´äÀÔ´Ï´Ù!\n Á¤´äÀº " + correctAnswer + " ÀÔ´Ï´Ù.";
                    ProcessIncorrectAnswer();
                }
                Debug.Log("OX Å¸ÀÔ Áú¹® Ã³¸®");
                
                break;

            case "SA":
                // "SA"ÀÏ ¶§ ½ÇÇàÇÒ ÄÚµå correctAnswer = GameManager.instance.Normalization(Questions[questionNum][5]);

                Debug.Log("³ÊÀÇÀÌ¸§Àº");
                correctAnswer = GameManager.instance.Normalization(Questions[questionNum][5]);
                Debug.Log("Á¤´ä ½ºÆ÷´Â: " + correctAnswer);
                Debug.Log("ÀÔ·ÂµÈ ½ºÆ÷´Â: " + answer);
                if (IsAnswerCorrect(correctAnswer, answer))
                {
                    showTxt = "Á¤´äÀÔ´Ï´Ù!";
                    ProcessCorrectAnswer();
                }
                else
                {
                    showTxt = "¿À´äÀÔ´Ï´Ù!\n Á¤´äÀº " + Questions[questionNum][5] + " ÀÔ´Ï´Ù.";
                    ProcessIncorrectAnswer();
                }
                break;

            case "SO":
                correctAnswer = Questions[questionNum][5];
                Debug.Log("Á¤´ä ½ºÆ÷: " + correctAnswer);
                Debug.Log("´äº¯ ½ºÆ÷: " + answer);
                if (answer == correctAnswer)
                {
                    showTxt = "Á¤´äÀÔ´Ï´Ù!";
                    ProcessCorrectAnswer();
                }
                else
                {
                    showTxt = "¿À´äÀÔ´Ï´Ù!\n Á¤´äÀº " + correctAnswer + " ÀÔ´Ï´Ù.";
                    ProcessIncorrectAnswer();
                }
                Debug.Log("SO Å¸ÀÔ Áú¹® Ã³¸®");
                break;

            case "SM":
                correctAnswer = GameManager.instance.Normalization(Questions[questionNum][5]); Debug.Log(correctAnswer);
                Debug.Log("Á¤´ä ½ºÆ÷: " + correctAnswer);
                if (answer == correctAnswer)
                {
                    showTxt = "Á¤´äÀÔ´Ï´Ù!";
                    ProcessCorrectAnswer();
                }
                else
                {
                    showTxt = "¿À´äÀÔ´Ï´Ù!\n Á¤´äÀº " + correctAnswer + " ÀÔ´Ï´Ù.";
                    ProcessIncorrectAnswer();
                }
                Debug.Log("SM Å¸ÀÔ Áú¹® Ã³¸®");
                break;

            default:
                // ¿¹¿Ü Ã³¸®: ¿¹»óÄ¡ ¸øÇÑ °ªÀÏ ¶§
                Debug.LogWarning("¾Ë ¼ö ¾ø´Â Áú¹® Å¸ÀÔ: " + Questions[questionNum][0]);
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

    void ProcessCorrectAnswer() //Á¤´ä
    {
        //Á¡¼ö

        comboCount += 1;
        maxCombo = maxCombo > comboCount ? maxCombo : comboCount;

        Debug.Log("ÄÞº¸: " + comboCount.ToString());

        //Ç¬ È½¼ö¿¡ 1 ´õÇÏ±â
        // µÚ¿¡¼­ µÎ ¹øÂ° ¿ø¼Ò °¡Á®¿À±â (¹®Á¦ Ç¬ È½¼ö)
        string secondLastElement = Questions[questionNum][Questions[questionNum].Count - 2];

        // µÚ¿¡¼­ µÎ ¹øÂ° ¿ø¼Ò¸¦ int·Î º¯È¯ÇÏ¿© 1 ´õÇÏ±â
        int secondLastNumber = int.Parse(secondLastElement);
        secondLastNumber += 1;

        // 1 ´õÇÑ °ªÀ» ´Ù½Ã stringÀ¸·Î º¯È¯ÇÏ¿© µ¤¾î¾²±â
        Questions[questionNum][Questions[questionNum].Count - 2] = secondLastNumber.ToString();


    }

    void ProcessIncorrectAnswer()  //¿À´ä
    {
        incorrectQuestionCount += 1;
        comboCount = 0;
        lifeCount -= 1;
        TopBar.GetComponent<TopBar>().SetHeartCount(lifeCount);

        //Ç¬ È½¼ö¶û Æ²¸° È½¼ö ¸ðµÎ ´õÇÏ±â
        // µÚ¿¡¼­ µÎ ¹øÂ° ¿ø¼Ò °¡Á®¿À±â
        string secondLastElement = Questions[questionNum][Questions[questionNum].Count - 2];

        // ¸¶Áö¸· ¿ø¼Ò °¡Á®¿À±â
        string lastElement = Questions[questionNum][Questions[questionNum].Count - 1];

        // µÎ ¿ø¼Ò¸¦ int·Î º¯È¯ÇÏ¿© °¢°¢ 1 ´õÇÏ±â
        int secondLastNumber = int.Parse(secondLastElement);
        secondLastNumber += 1;

        int lastNumber = int.Parse(lastElement);
        lastNumber += 1;

        // 1 ´õÇÑ °ªÀ» ´Ù½Ã stringÀ¸·Î º¯È¯ÇÏ¿© °¢°¢ µ¤¾î¾²±â
        Questions[questionNum][Questions[questionNum].Count - 2] = secondLastNumber.ToString();
        Questions[questionNum][Questions[questionNum].Count - 1] = lastNumber.ToString();

        //¿À´ä ¸®½ºÆ®¿¡ ³Ö±â
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
                // ¿¹¿Ü Ã³¸®: ¿¹»óÄ¡ ¸øÇÑ °ªÀÏ ¶§
                Debug.LogWarning("¾Ë ¼ö ¾ø´Â Áú¹® Å¸ÀÔ: " + Questions[questionNum][0]);
                GameManager.instance.LoadScene();
                break;
        }
    }

    void PrintList(List<string> list)
    {
        Debug.Log("¹®Á¦ Ãâ·Â ÇØÁàÀ×!");
        foreach (string item in list)
        {
            Debug.Log(item);
        }
    }

    // Á¤´ä°ú ´äº¯À» ºñ±³ÇÏ´Â ÇÔ¼ö
    bool IsAnswerCorrect(string answer, string correctAnswer) 
    {
        // 1. ´äº¯°ú Á¤´ä¿¡¼­ ÇÑ±Û, ¾ËÆÄºª, ¼ýÀÚ, -¸¸ ³²±â±â
        string processedAnswer = CleanString(answer);
        string processedCorrectAnswer = CleanString(correctAnswer);

        // 2. ´ë¼Ò¹®ÀÚ ±¸ºÐ ¾øÀ½ (¼Ò¹®ÀÚ·Î º¯È¯)
        processedAnswer = processedAnswer.ToLower();
        processedCorrectAnswer = processedCorrectAnswer.ToLower();

        // 3. ´äº¯ÀÇ ³¡ÀÌ ÇÑ±ÛÀÌ¸é ¸¶Áö¸· ±ÛÀÚ Á¦°Å ÈÄ ºñ±³
        if (IsLastCharacterKorean(processedAnswer))
        {
            processedAnswer = processedAnswer.Substring(0, processedAnswer.Length - 1);
        }

        // ºñ±³ ÈÄ °á°ú ¹ÝÈ¯
        return processedAnswer == processedCorrectAnswer;
    }

    // ÇÑ±Û, ¾ËÆÄºª, ¼ýÀÚ, -¸¸ ³²±â´Â ÇÔ¼ö
    private static string CleanString(string input)
    {
        return Regex.Replace(input, "[^°¡-ÆRa-zA-Z0-9-]", "");
    }

    // ¹®ÀÚ¿­ ¸¶Áö¸· ¹®ÀÚ°¡ ÇÑ±ÛÀÎÁö È®ÀÎÇÏ´Â ÇÔ¼ö
    private static bool IsLastCharacterKorean(string input)
    {
        return input.Length > 0 && Regex.IsMatch(input[input.Length - 1].ToString(), @"[\uac00-\ud7af]");
    }
}