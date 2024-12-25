using UnityEngine;

public class ButtonsManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCourse(string course)
    {
        if (string.IsNullOrEmpty(course))
        {
            //¿¡·¯
            GameManager.instance.LoadScene();
            return;
        }

        ES3.Save("course", course);
        GameManager.instance.LoadScene("MakingCourse");
    }
}
