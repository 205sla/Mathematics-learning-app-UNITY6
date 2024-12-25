using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class TopBar : MonoBehaviour
{
    [SerializeField]
    TMP_Text HeartCount;

    [SerializeField]
    ProgressBar progressBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SetHeartCount(int heartCount)
    {
        HeartCount.text = heartCount.ToString();
    }

    public void SetProgress(int progress) {
        //progressBar.SetValue(progress);
    }
}
