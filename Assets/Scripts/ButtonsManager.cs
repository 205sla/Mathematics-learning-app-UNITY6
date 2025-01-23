using UnityEngine;

public class ButtonsManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    RectTransform Content;

    float lastLearnedSemester = 0;
    void Awake()
    {
        //���������� �н��� �б� �ҷ�����
        lastLearnedSemester = ES3.Load("lastLearnedSemester", 0f);
        Content.anchoredPosition = new Vector2(lastLearnedSemester, 1700);

    }

    public void SetCourse(string course)
    {
        if (string.IsNullOrEmpty(course))
        {
            //����
            GameManager.instance.LoadScene();
            return;
        }
        ES3.Save("lastLearnedSemester", Content.position.x);
        ES3.Save("course", course);
        GameManager.instance.LoadScene("MakingCourse");
    }

}
