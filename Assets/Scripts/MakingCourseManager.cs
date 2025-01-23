using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class MakingCourseManager : MonoBehaviour
{
    private Coroutine myCoroutine;

    string courseName = "";
    Vector2 courseType = new Vector2(0, 0);
    int courseNum = -1;

    List<List<string>> ListOXdata, ListSAdata, ListSOdata, ListSMdata;
    List<List<string>> possibleQuestion = new List<List<string>>();

    void Start()
    {
        // Coroutine ����
        myCoroutine = StartCoroutine(MyCoroutine());
    }

    IEnumerator MyCoroutine()
    {
        // 0.5�� ���
        yield return new WaitForSeconds(0.5f);

        // ���������� MakingCourse �Լ� ����
        yield return StartCoroutine(MakingCourse());
    }

    IEnumerator MakingCourse()
    {
        // "course" �����Ͱ� �����ϴ��� üũ
        if (ES3.KeyExists("course"))
        {
            // SetCourse, LoadData, SetPossibleQuestion�� ���������� ����
            yield return StartCoroutine(SetCourse());  // course ����
            yield return StartCoroutine(LoadData());   // ������ �ε�
            yield return StartCoroutine(SetPossibleQuestion());  // ������ ���� ����

            int questionCount = 0;
            // ����Ʈ���� ������ �׸��� ����
            if ((int)courseType.x == 0)
            {
                if((int)courseType.y == 10)
                {
                    questionCount = 5;
                }
                else
                {
                    questionCount = 0;
                }
            }
            else
            {
                questionCount = 20;
            }
            SelectRandomLists(ref possibleQuestion, questionCount);

            // ����� ����
            ES3.Save("courseData", possibleQuestion);

            // course �����͸� �ҷ����� ���
            //PrintList(ES3.Load<List<List<string>>>("courseData"));

            // ���� Ǯ�� ȭ������ �̵�
            GameManager.instance.LoadScene("SolvingQuestions");
        }
        else
        {
            // "course" �����Ͱ� ���ٸ� ���� ó��
            GameManager.instance.LoadScene();
            yield break;  // �ߴ�
        }
    }

    // PrintList �Լ�: ����׿� ����Ʈ ���
    void PrintList(List<List<string>> list)
    {
        foreach (var sublist in list)
        {
            string output = "Sublist: ";
            foreach (var item in sublist)
            {
                output += item + " ";  // ���긮��Ʈ�� �׸���� �ϳ��� �߰�
            }
            Debug.Log(output);  // ����� ���
        }
    }

    // ���� ����Ʈ ���� �Լ�
    void SelectRandomLists(ref List<List<string>> sourceLists, int numberOfSelections)
    {
        if (numberOfSelections == 0)
        {
            return;
        }



            List<int> availableIndexes = new List<int>();
        for (int i = 0; i < sourceLists.Count; i++)
        {
            availableIndexes.Add(i);
        }

        // ����Ʈ�� �����ϰ� ���� ���� Fisher-Yates shuffle ���
        for (int i = availableIndexes.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            int temp = availableIndexes[i];
            availableIndexes[i] = availableIndexes[j];
            availableIndexes[j] = temp;
        }


        List<List<string>> selectedLists = new List<List<string>>();
        
            // �������� 5�� �ε����� �����ϰ�, ������ ����Ʈ���� ����
            for (int i = 0; i < numberOfSelections; i++)
            {
                selectedLists.Add(sourceLists[availableIndexes[i]]);
            }
        
       

      

        // ���õ� ����Ʈ�� ���� ����Ʈ�� ���
        sourceLists.Clear();
        sourceLists.AddRange(selectedLists);
    }

    // ������ ���� ����Ʈ ����
    IEnumerator SetPossibleQuestion()
    {
        possibleQuestion.Clear();

        if ((int)courseType.x == 0 && (int)courseType.y != 10)
        {
            List<List<List<string>>> Datas = new List<List<List<string>>>();

            if (ES3.KeyExists("WrongProblemData"))
            {
                // ���� ������ �ε�
                Datas = ES3.Load<List<List<List<string>>>>("WrongProblemData");
                Debug.Log("���� ������ �ε� �Ϸ�.");
            }
            List<List<string>> reviewWrongAnswers = new List<List<string>>();
            reviewWrongAnswers = Datas[(int)courseType.y];

            foreach (List<string> P in reviewWrongAnswers)
            {

                if (P.Count == 1)
                {
                    continue;
                }
                possibleQuestion.Add(P);
                if (P[0] == "OX")
                {
                    List<string> tempList = new List<string>();
                    tempList.Add("OX");
                    if (int.Parse(P[1]) % 2 == 0)
                    {
                        tempList.AddRange(ListOXdata[int.Parse(P[1])-2]);
                    }
                    else
                    {
                        tempList.AddRange(ListOXdata[int.Parse(P[1])]);
                    }
                    possibleQuestion.Add(tempList);
                }

                if (P[0] == "SA")
                {
                    List<string> tempList = new List<string>();
                    tempList.Add("SA");
                    if (int.Parse(P[1]) % 2 == 0)
                    {
                        tempList.AddRange(ListSAdata[int.Parse(P[1]) - 2]);
                    }
                    else
                    {
                        tempList.AddRange(ListSAdata[int.Parse(P[1])]);
                    }
                    possibleQuestion.Add(tempList);
                }

                if (P[0] == "SO")
                {
                    List<string> tempList = new List<string>();
                    tempList.Add("SO");
                    if (int.Parse(P[1]) % 2 == 0)
                    {
                        tempList.AddRange(ListSOdata[int.Parse(P[1]) - 2]);
                    }
                    else
                    {
                        tempList.AddRange(ListSOdata[int.Parse(P[1])]);
                    }
                    possibleQuestion.Add(tempList);
                }

                if (P[0] == "SM")
                {
                    List<string> tempList = new List<string>();
                    tempList.Add("SM");
                    if (int.Parse(P[1]) % 2 == 0)
                    {
                        tempList.AddRange(ListSMdata[int.Parse(P[1]) - 2]);
                    }
                    else
                    {
                        tempList.AddRange(ListSMdata[int.Parse(P[1])]);
                    }
                    possibleQuestion.Add(tempList);
                }
            }



        }
        else
        {




            // OX
            foreach (List<string> P in ListOXdata)
            {
                if (CheckProblemCorrect(P))
                {
                    List<string> tempList = new List<string>();
                    tempList.Add("OX");
                    tempList.AddRange(P);
                    possibleQuestion.Add(tempList);
                }
            }

            // SA
            foreach (List<string> P in ListSAdata)
            {
                if (CheckProblemCorrect(P))
                {
                    List<string> tempList = new List<string>();
                    tempList.Add("SA");
                    tempList.AddRange(P);
                    possibleQuestion.Add(tempList);
                }
            }

            // SO
            foreach (List<string> P in ListSOdata)
            {
                if (CheckProblemCorrect(P))
                {
                    List<string> tempList = new List<string>();
                    tempList.Add("SO");
                    tempList.AddRange(P);
                    possibleQuestion.Add(tempList);
                }
            }

            // SM
            foreach (List<string> P in ListSMdata)
            {
                if (CheckProblemCorrect(P))
                {
                    List<string> tempList = new List<string>();
                    tempList.Add("SM");
                    tempList.AddRange(P);
                    possibleQuestion.Add(tempList);
                }
            }
        }
        yield return null;  // ��� ����Ͽ�, ���� �۾��� �ٷ� ����ǵ��� �մϴ�.
    }

    // ���� ���� �Լ�
    bool CheckProblemCorrect(List<string> p)
    {
        if (p[0] == "end")
        {
            return false;
        }
        if (courseNum == 8)
        {
            return GameManager.instance.Normalization(p[2]) == courseName;
        }
        else
        {
            return GameManager.instance.Normalization(p[1]) == courseNum.ToString();
        }
    }

    // ������ �ε� �Լ�
    IEnumerator LoadData()
    {
        // OX ������ �ҷ�����
        if (ES3.KeyExists("ListOXdata"))
        {
            ListOXdata = ES3.Load<List<List<string>>>("ListOXdata");
        }
        else
        {
            GameManager.instance.LoadScene();
            yield break;  // �ߴ�
        }

        // SO ������ �ҷ�����
        if (ES3.KeyExists("ListSOdata"))
        {
            ListSOdata = ES3.Load<List<List<string>>>("ListSOdata");
        }
        else
        {
            GameManager.instance.LoadScene();
            yield break;  // �ߴ�
        }

        // SA ������ �ҷ�����
        if (ES3.KeyExists("ListSAdata"))
        {
            ListSAdata = ES3.Load<List<List<string>>>("ListSAdata");
        }
        else
        {
            GameManager.instance.LoadScene();
            yield break;  // �ߴ�
        }

        // SM ������ �ҷ�����
        if (ES3.KeyExists("ListSMdata"))
        {
            ListSMdata = ES3.Load<List<List<string>>>("ListSMdata");
        }
        else
        {
            GameManager.instance.LoadScene();
            yield break;  // �ߴ�
        }

        yield return null;  // ��� ������ �ε� �Ϸ� �� ����
    }

    // �ڽ� ���� �Լ�
    IEnumerator SetCourse()
    {
        // "course" Ű�� �����ϸ� �����͸� �ҷ�����, ������ ���� ó��
        if (ES3.KeyExists("course"))
        {
            courseName = GameManager.instance.Normalization(ES3.Load<string>("course"));
            Debug.Log("�ڽ� �̸�: " + courseName);
            courseType = CheckCourseType(courseName);
            courseNum = (int)courseType.x * 2 + (int)courseType.y - 2;
            Debug.Log("�ڽ� ��ȣ: " + courseNum);
        }
        else
        {
            // course�� ������ ���� ó��
            GameManager.instance.LoadScene();
            yield break;  // �ߴ�
        }

        yield return null;  // �ڽ� ���� �Ϸ� �� ����
    }

    // �ڽ� Ÿ�� üũ �Լ�
    Vector2 CheckCourseType(string input)
    {
        string pattern = @"^(\d+)�г�(\d+)�б�$";
        string pattern2 = @"^���亹��(\d+)$";
        Match match = Regex.Match(input, pattern);

        if (match.Success)
        {
            
            string a = match.Groups[1].Value;  // ù ��° �׷��� a (�г� �� ����)
            string b = match.Groups[2].Value;  // �� ��° �׷��� b (�б� �� ����)
            Debug.Log("�ڽ��� "+ a +"�г�"+b+"�б�");
            return new Vector2(a[0] - '0', b[0] - '0');
        }
        else
        {
            Match match2 = Regex.Match(input, pattern2);
            if (match2.Success)
            {
                Debug.Log("�ڽ��� ����"+ match2.Groups[1].Value[0]);

                return new Vector2(0, match2.Groups[1].Value[0]-'0');
            }
            else
            {
                Debug.Log("�ڽ��� �ܿ����̾�");
                return new Vector2(0, 10);
            }
        }
    }
}
