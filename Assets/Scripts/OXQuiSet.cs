using System;
using UnityEngine;
using TMPro;
using TexDrawLib;
using UnityEditor;
public class OXQuiSet : MonoBehaviour
{
    


    [SerializeField]
    GameObject SolvingQuestionsManager, QuestionTxt, CheckO, CheckX;

    public void SetQuestion(string str)
    {
        QuestionTxt.GetComponent<TEXDraw>().text = str;
        CheckO.GetComponent<Check>().SetHighlightON(false);
        CheckX.GetComponent<Check>().SetHighlightON(false);
    }

    public void OBtn()
    {
        SolvingQuestionsManager.GetComponent<SolvingQuestionsManager>().InputAnswer("O");
        CheckO.GetComponent<Check>().SetHighlightON(true);
        CheckX.GetComponent<Check>().SetHighlightON(false);
    }

    public void XBtn()
    {
        SolvingQuestionsManager.GetComponent<SolvingQuestionsManager>().InputAnswer("X");
        CheckX.GetComponent<Check>().SetHighlightON(true);
        CheckO.GetComponent<Check>().SetHighlightON(false);
    }


}
