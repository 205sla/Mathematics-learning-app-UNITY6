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
        // ���� �迭�� UI�� ����
        for (int i = 0; i < 5; i++)
        {
            OptionTEXDraw[i].GetComponent<TEXDraw>().text = str[i];
        }
    }

    public void SelectAnswer(int answer)
    {
        chosenAnswers[answer - 1] = !chosenAnswers[answer - 1];
        Check[answer - 1].GetComponent<Check>().SetHighlightON(chosenAnswers[answer - 1]);


        string result = ""; // ����� ������ ���ڿ�

        // �迭�� ��ȸ�ϸ鼭 true�� �ε����� ã��
        for (int i = 0; i < chosenAnswers.Length; i++)
        {
            if (chosenAnswers[i]) // true�� ���
            {
                result += (i + 1).ToString(); // 1-based �ε����� �߰�
            }
        }
        SolvingQuestionsManager.GetComponent<SolvingQuestionsManager>().InputAnswer(result);
    }
}
