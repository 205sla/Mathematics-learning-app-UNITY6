using DG.Tweening;
using Michsky.MUIP;
using TexDrawLib;
using UnityEngine;

public class ResultBoard : MonoBehaviour
{

    [SerializeField]
    GameObject ResultButton, CorrectAnswer;

    RectTransform rectTransform;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetBtn(string txt, int y, float time)
    {
        Debug.Log(txt);
        ResultButton.GetComponent<ButtonManager>().SetText(txt);

        rectTransform.DOAnchorPosY(y, time).SetEase(Ease.OutBounce);
    }

    public void SetCorrectAnswer(string txt)
    {
        if (CorrectAnswer != null)
        {
            CorrectAnswer.GetComponent<TEXDraw>().text = txt;
        }
    }


}
