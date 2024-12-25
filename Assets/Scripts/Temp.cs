using UnityEngine;
using UnityEngine.SceneManagement;
public class Temp : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(GameManager.instance != null)
        {
            Debug.Log("dddddd");
            SceneManager.LoadScene("Title");
        }
        else
        {
            Debug.Log("ddd");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
