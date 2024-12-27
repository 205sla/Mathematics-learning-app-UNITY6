using System; /* for Serializable */
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReadQuestionManager : MonoBehaviour
{
    [Serializable]
    public class ProblemOX
    {
        public string index;
        public string semester;
        public string type;
        public string question;
        public string correct_answer;

        public List<string> GetProblem()
        {
            return new List<string> { index, semester, type, question, correct_answer };
        }
    }
    [Serializable]
    public class ProblemSA
    {
        public string index;
        public string semester;
        public string type;
        public string question;
        public string correct_answer;
        public string input_form;

        public List<string> GetProblem()
        {
            return new List<string> { index, semester, type, question, correct_answer, input_form };
        }
    }
    [Serializable]
    public class ProblemSO
    {
        public string index;
        public string semester;
        public string type;
        public string question;
        public string correct_answer;

        public string wrong_answer1;
        public string wrong_answer2;
        public string wrong_answer3;
        public string wrong_answer4;

        public List<string> GetProblem()
        {
            return new List<string> { index, semester, type, question, correct_answer, wrong_answer1, wrong_answer2, wrong_answer3, wrong_answer4 };
        }
    }
    [Serializable]
    public class ProblemSM
    {
        public string index;
        public string semester;
        public string type;
        public string question;
        public string number_of_correct_answers;

        public string answer1;
        public string answer2;
        public string answer3;
        public string answer4;
        public string answer5;


        public List<string> GetProblem()
        {
            return new List<string> { index, semester, type, question, number_of_correct_answers, answer1, answer2, answer3, answer4, answer5 };
        }
    }
    public class Problems
    {
        public ProblemOX[] OXQuiz;
        public ProblemSA[] short_answer;
        public ProblemSO[] select_one;
        public ProblemSM[] select_multiple;
    }

    public void RoadManagerStart()
    {
        Debug.Log("시작됨");
        if (!(ES3.KeyExists("ListOXdata") && ES3.KeyExists("ListSAdata") && ES3.KeyExists("ListSOdata") && ES3.KeyExists("ListSMdata")))
        {
            Debug.Log("문제가 없어서 불러와라");
            ReadQuestion();
            Debug.Log("문제 다 불러옴");
        }
        else
        {
            Debug.Log("문제 이미 있어요.");
        }


        Debug.Log("문제에 이상 없는지 확인");
        if (!checkRoad("ListOXdata") || !checkRoad("ListSAdata")|| !checkRoad("ListSOdata") || !checkRoad("ListSMdata"))
        {
            Debug.LogError("문제에 이상 있음");
            PlayerPrefs.SetInt("errorCode", 20);
            SceneManager.LoadScene("Error");
        }
        else
        {
            Debug.Log("문제에 문제 없어");
        }

        GameManager.instance.ShowButton();


    }
    bool checkRoad(string key)
    {
        List<List<string>> checkRoad = ES3.Load<List<List<string>>>(key);

        // checkRoad가 비어있지 않고 마지막 항목이 "end"만 포함하는 리스트인지 확인
        if (checkRoad.Count > 0)
        {
            // 마지막 항목이 "end"만 포함하는 리스트인지 확인
            List<string> lastItem = checkRoad[checkRoad.Count - 1]; // 마지막 리스트 가져오기

            if (lastItem.Count == 1 && lastItem[0] == "end")
            {
                Debug.Log("The last item is a list containing only 'end'.");
                return true;
            }
            else
            {
                Debug.Log("The last item is not a list containing only 'end'.");
                return false;
            }
        }
        else
        {
            Debug.LogError("The list is empty.");
            return false;

        }
    }


    void ReadQuestion()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Json/Problem Data");

        Problems ProblemList = JsonUtility.FromJson<Problems>(textAsset.text);

        List<List<string>> ListOX = new List<List<string>>();
        foreach (ProblemOX lt in ProblemList.OXQuiz)
        {
            List<string> tempList = lt.GetProblem();
            tempList.Add("0");
            tempList.Add("0");
            ListOX.Add(tempList);
        }
        ListOX.Add(new List<string> { "end" });
        ES3.Save<List<List<string>>>("ListOXdata", ListOX);


        List<List<string>> ListSA = new List<List<string>>();
        foreach (ProblemSA lt in ProblemList.short_answer)
        {
            List<string> tempList = lt.GetProblem();
            tempList.Add("0");
            tempList.Add("0");
            ListSA.Add(tempList);
        }
        ListSA.Add(new List<string> { "end" });
        ES3.Save<List<List<string>>>("ListSAdata", ListSA);


        List<List<string>> ListSO = new List<List<string>>();
        foreach (ProblemSO lt in ProblemList.select_one)
        {
            List<string> tempList = lt.GetProblem();
            tempList.Add("0");
            tempList.Add("0");
            ListSO.Add(tempList);
        }
        ListSO.Add(new List<string> { "end" });
        ES3.Save<List<List<string>>>("ListSOdata", ListSO);


        List<List<string>> ListSM = new List<List<string>>();
        foreach (ProblemSM lt in ProblemList.select_multiple)
        {
            List<string> tempList = lt.GetProblem();
            tempList.Add("0");
            tempList.Add("0");
            ListSM.Add(tempList);
        }
        ListSM.Add(new List<string> { "end" });
        ES3.Save<List<List<string>>>("ListSMdata", ListSM);
    }
}
