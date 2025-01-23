using TexDrawLib;
using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class SAQuizSet : MonoBehaviour
{
    [SerializeField]
    GameObject SolvingQuestionsManager, QuestionTxt;

    [SerializeField]
    TMP_InputField inputField;

    [SerializeField]
    TMP_Text placeholderText, ansK;

    string returnAnsTxt = "";


    public void SetQuestion(string str, string inpinputForm)
    {
        QuestionTxt.GetComponent<TEXDraw>().text = str;
        placeholderText.text = inpinputForm;
        inputField.text = "";
    }

    public void InputAns()
    {
        Debug.Log("가짜 답변" + inputField.text);
        SolvingQuestionsManager.GetComponent<SolvingQuestionsManager>().InputAnswer(inputField.text);
    }



}
