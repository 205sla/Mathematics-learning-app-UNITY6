using TexDrawLib;
using UnityEngine;

public class SOQuizSet : MonoBehaviour
{
    [SerializeField]
    GameObject SolvingQuestionsManager, QuestionTxt;

    [SerializeField]
    GameObject[] Check, OptionTEXDraw;


    public void SetQuestion(string str)
    {
        QuestionTxt.GetComponent<TEXDraw>().text = str;
        for (int i = 0; i < 5; i++) { 
            Check[i].GetComponent<Check>().SetHighlightON(false);
        }
    }

    public void SetOption(string[] str) {
        // Fisher-Yates 알고리즘을 이용하여 배열 섞기
        for (int i = 0; i < str.Length; i++)
        {
            // 임의의 인덱스를 선택
            int randomIndex = Random.Range(i, str.Length);

            // 현재 인덱스와 랜덤 인덱스를 교환
            string temp = str[i];
            str[i] = str[randomIndex];
            str[randomIndex] = temp;
        }

        // 섞은 배열을 UI에 설정
        for (int i = 0; i < 5; i++)
        {
            OptionTEXDraw[i].GetComponent<TEXDraw>().text = str[i];
        }
    }

    public void SelectAnswer(int answer)
    {
        SolvingQuestionsManager.GetComponent<SolvingQuestionsManager>().InputAnswer(OptionTEXDraw[answer-1].GetComponent<TEXDraw>().text, false);
        for (int i = 0; i < 5; i++)
        {
            Check[i].GetComponent<Check>().SetHighlightON(answer-1 == i);
            
        }
    }
}
