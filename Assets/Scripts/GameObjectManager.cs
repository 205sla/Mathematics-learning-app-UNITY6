using UnityEngine;

public class GameObjectManager : MonoBehaviour
{
    public GameObject ReadQuestionManager, Loading, Buttons;

    private void Start()
    {
        GameManager.instance.FindMe();
    }

    public void PlayButton()
    {
        GameManager.instance.LoadScene("SelectProblem");
    }
    public void RecordButton()
    {
        GameManager.instance.LoadScene("RecordProblem");
    }
    public void SettingButton()
    {
        GameManager.instance.LoadScene("Setting");
    }

    public void ShowButton()
    {
        Loading.gameObject.SetActive(false);
        Buttons.gameObject.SetActive(true);
    }


}
