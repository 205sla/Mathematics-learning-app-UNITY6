using UnityEngine;
using TexDrawLib;
using UnityEngine.UI;
using TMPro;

public class SAQuizSet : MonoBehaviour
{
    [SerializeField]
    GameObject SolvingQuestionsManager, QuestionTxt;

    [SerializeField]
    TMP_InputField inputField;

    [SerializeField]
    TMP_Text placeholderText;


    public void SetQuestion(string str, string inpinputForm)
    {
        QuestionTxt.GetComponent<TEXDraw>().text = str;
        placeholderText.text = inpinputForm;
        inputField.text = "";
    }

    public void InputAns()
    {
        Debug.Log("��¥ �亯"+inputField.text);
        SolvingQuestionsManager.GetComponent<SolvingQuestionsManager>().InputAnswer(inputField.text);
    }
}