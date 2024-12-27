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


    public void UpdateAns()
    {
        returnAnsTxt = inputField.text;
        inputField.text = "";
        StartCoroutine(HandleSubmitAfterDelay());
    }

    private IEnumerator HandleSubmitAfterDelay() {
        yield return new WaitForSeconds(0.2f);  // 0.1초 정도 기다리면 충분할 수 있다.
        returnAnsTxt += inputField.text;
        inputField.text = "";
        Debug.Log("진짜 답변" + returnAnsTxt);
        SolvingQuestionsManager.GetComponent<SolvingQuestionsManager>().InputAnswer(inputField.text);
    }


}
