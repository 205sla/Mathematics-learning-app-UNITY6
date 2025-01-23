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
        // Fisher-Yates �˰����� �̿��Ͽ� �迭 ����
        for (int i = 0; i < str.Length; i++)
        {
            // ������ �ε����� ����
            int randomIndex = Random.Range(i, str.Length);

            // ���� �ε����� ���� �ε����� ��ȯ
            string temp = str[i];
            str[i] = str[randomIndex];
            str[randomIndex] = temp;
        }

        // ���� �迭�� UI�� ����
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
