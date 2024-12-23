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
        // Coroutine 시작
        myCoroutine = StartCoroutine(MyCoroutine());
    }

    IEnumerator MyCoroutine()
    {
        // 0.5초 대기
        yield return new WaitForSeconds(0.5f);

        // 순차적으로 MakingCourse 함수 실행
        yield return StartCoroutine(MakingCourse());
    }

    IEnumerator MakingCourse()
    {
        // "course" 데이터가 존재하는지 체크
        if (ES3.KeyExists("course"))
        {
            // SetCourse, LoadData, SetPossibleQuestion을 순차적으로 실행
            yield return StartCoroutine(SetCourse());  // course 설정
            yield return StartCoroutine(LoadData());   // 데이터 로딩
            yield return StartCoroutine(SetPossibleQuestion());  // 가능한 문제 설정

            // 리스트에서 랜덤한 항목을 선택
            SelectRandomLists(ref possibleQuestion, courseNum == -1 ? 5 : 20);

            // 결과를 저장
            ES3.Save("courseData", possibleQuestion);

            // course 데이터를 불러오고 출력
            //PrintList(ES3.Load<List<List<string>>>("courseData"));

            // 문제 풀이 화면으로 이동
            GameManager.instance.LoadScene("SolvingQuestions");
        }
        else
        {
            // "course" 데이터가 없다면 오류 처리
            GameManager.instance.LoadScene();
            yield break;  // 중단
        }
    }

    // PrintList 함수: 디버그용 리스트 출력
    void PrintList(List<List<string>> list)
    {
        foreach (var sublist in list)
        {
            string output = "Sublist: ";
            foreach (var item in sublist)
            {
                output += item + " ";  // 서브리스트의 항목들을 하나씩 추가
            }
            Debug.Log(output);  // 디버그 출력
        }
    }

    // 랜덤 리스트 선택 함수
    void SelectRandomLists(ref List<List<string>> sourceLists, int numberOfSelections)
    {
        List<int> availableIndexes = new List<int>();
        for (int i = 0; i < sourceLists.Count; i++)
        {
            availableIndexes.Add(i);
        }

        // 리스트를 랜덤하게 섞기 위해 Fisher-Yates shuffle 사용
        for (int i = availableIndexes.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            int temp = availableIndexes[i];
            availableIndexes[i] = availableIndexes[j];
            availableIndexes[j] = temp;
        }

        // 랜덤으로 5개 인덱스를 선택하고, 나머지 리스트들은 삭제
        List<List<string>> selectedLists = new List<List<string>>();
        for (int i = 0; i < numberOfSelections; i++)
        {
            selectedLists.Add(sourceLists[availableIndexes[i]]);
        }

        // 선택된 리스트를 원본 리스트에 덮어씀
        sourceLists.Clear();
        sourceLists.AddRange(selectedLists);
    }

    // 가능한 문제 리스트 설정
    IEnumerator SetPossibleQuestion()
    {
        possibleQuestion.Clear();

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

        yield return null;  // 잠시 대기하여, 다음 작업이 바로 실행되도록 합니다.
    }

    // 문제 검증 함수
    bool CheckProblemCorrect(List<string> p)
    {
        if (p[0] == "end")
        {
            return false;
        }
        if (courseNum == -1)
        {
            return GameManager.instance.Normalization(p[2]) == courseName;
        }
        else
        {
            return GameManager.instance.Normalization(p[1]) == courseNum.ToString();
        }
    }

    // 데이터 로딩 함수
    IEnumerator LoadData()
    {
        // OX 데이터 불러오기
        if (ES3.KeyExists("ListOXdata"))
        {
            ListOXdata = ES3.Load<List<List<string>>>("ListOXdata");
        }
        else
        {
            GameManager.instance.LoadScene();
            yield break;  // 중단
        }

        // SO 데이터 불러오기
        if (ES3.KeyExists("ListSOdata"))
        {
            ListSOdata = ES3.Load<List<List<string>>>("ListSOdata");
        }
        else
        {
            GameManager.instance.LoadScene();
            yield break;  // 중단
        }

        // SA 데이터 불러오기
        if (ES3.KeyExists("ListSAdata"))
        {
            ListSAdata = ES3.Load<List<List<string>>>("ListSAdata");
        }
        else
        {
            GameManager.instance.LoadScene();
            yield break;  // 중단
        }

        // SM 데이터 불러오기
        if (ES3.KeyExists("ListSMdata"))
        {
            ListSMdata = ES3.Load<List<List<string>>>("ListSMdata");
        }
        else
        {
            GameManager.instance.LoadScene();
            yield break;  // 중단
        }

        yield return null;  // 모든 데이터 로딩 완료 후 진행
    }

    // 코스 설정 함수
    IEnumerator SetCourse()
    {
        // "course" 키가 존재하면 데이터를 불러오고, 없으면 에러 처리
        if (ES3.KeyExists("course"))
        {
            courseName = GameManager.instance.Normalization(ES3.Load<string>("course"));
            Debug.Log("코스 이름: " + courseName);
            courseType = CheckCourseType(courseName);
            courseNum = (int)courseType.x * 2 + (int)courseType.y - 2;
            Debug.Log("코스 번호: " + courseNum);
        }
        else
        {
            // course가 없으면 오류 처리
            GameManager.instance.LoadScene();
            yield break;  // 중단
        }

        yield return null;  // 코스 설정 완료 후 진행
    }

    // 코스 타입 체크 함수
    Vector2 CheckCourseType(string input)
    {
        string pattern = @"^(\d+)학년(\d+)학기$";
        Match match = Regex.Match(input, pattern);

        if (match.Success)
        {
            string a = match.Groups[1].Value;  // 첫 번째 그룹은 a (학년 앞 숫자)
            string b = match.Groups[2].Value;  // 두 번째 그룹은 b (학기 앞 숫자)
            return new Vector2(a[0] - '0', b[0] - '0');
        }
        else
        {
            return new Vector2(0, 1);
        }
    }
}
