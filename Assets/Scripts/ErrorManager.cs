using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class ErrorManager : MonoBehaviour
{
    public TextMeshProUGUI errorText;    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int errorCode = PlayerPrefs.GetInt("errorCode", -1);  // 기본값을 -1로 설정 (값이 없을 경우 0이 아닌 다른 값으로 처리)

        if (errorCode == 20) {
            errorText.text = "error : Failed to get questions\nRestart the app.";
            Debug.Log("저장된 문제에 오류 있음");
        }else if (errorCode == 30)
        {
            errorText.text = "error : Logo display error\nRestart the app.";
            Debug.Log("로고 보여주기 오류");
        }
        else
        {
            errorText.text = "error : null\nRestart the app.";
            Debug.Log("이유 모름");
        }

        ES3.DeleteKey("ListOXdata");
        ES3.DeleteKey("ListSAdata");
        ES3.DeleteKey("ListSOdata");
        ES3.DeleteKey("ListSMdata");
        PlayerPrefs.DeleteAll();


    }

    
}
