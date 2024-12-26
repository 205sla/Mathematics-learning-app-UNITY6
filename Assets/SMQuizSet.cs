using TexDrawLib;
using UnityEngine;


public class SMQuizSet : MonoBehaviour
{

    [SerializeField]
    GameObject SolvingQuestionsManager, QuestionTxt;

    [SerializeField]
    GameObject[] Check, OptionTEXDraw;

    bool[] chosenAnswers = new bool[5];

    public void SetQuestion(string str)
    {
        QuestionTxt.GetComponent<TEXDraw>().text = str;
        for (int i = 0; i < 5; i++)
        {
            Check[i].GetComponent<Check>().SetHighlightON(false);
        }
    }

    public void SetOption(string[] str)
    {
        // 섞은 배열을 UI에 설정
        for (int i = 0; i < 5; i++)
        {
            OptionTEXDraw[i].GetComponent<TEXDraw>().text = str[i];
        }
    }

    public void SelectAnswer(int answer)
    {
        chosenAnswers[answer - 1] = !chosenAnswers[answer - 1];
        Check[answer - 1].GetComponent<Check>().SetHighlightON(chosenAnswers[answer - 1]);


        string result = ""; // 결과를 저장할 문자열

        // 배열을 순회하면서 true인 인덱스를 찾기
        for (int i = 0; i < chosenAnswers.Length; i++)
        {
            if (chosenAnswers[i]) // true인 경우
            {
                result += (i + 1).ToString(); // 1-based 인덱스를 추가
            }
        }
        SolvingQuestionsManager.GetComponent<SolvingQuestionsManager>().InputAnswer(result);
    }
}
