using UnityEngine;

public class ButtonsManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    RectTransform Content;

    float lastLearnedSemester = 0;
    void Awake()
    {
        //마지막으로 학습한 학기 불러오기
        lastLearnedSemester = ES3.Load("lastLearnedSemester", 0f);
        Content.anchoredPosition = new Vector2(lastLearnedSemester, 1700);

    }

    public void SetCourse(string course)
    {
        if (string.IsNullOrEmpty(course))
        {
            //에러
            GameManager.instance.LoadScene();
            return;
        }
        ES3.Save("lastLearnedSemester", Content.position.x);
        ES3.Save("course", course);
        GameManager.instance.LoadScene("MakingCourse");
    }

}
