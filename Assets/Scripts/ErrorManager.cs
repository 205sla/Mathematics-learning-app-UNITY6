using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class ErrorManager : MonoBehaviour
{
    public TextMeshProUGUI errorText;    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int errorCode = PlayerPrefs.GetInt("errorCode", -1);  // �⺻���� -1�� ���� (���� ���� ��� 0�� �ƴ� �ٸ� ������ ó��)

        if (errorCode == 20) {
            errorText.text = "error : Failed to get questions\nRestart the app.";
            Debug.Log("����� ������ ���� ����");
        }else if (errorCode == 30)
        {
            errorText.text = "error : Logo display error\nRestart the app.";
            Debug.Log("�ΰ� �����ֱ� ����");
        }
        else
        {
            errorText.text = "error : null\nRestart the app.";
            Debug.Log("���� ��");
        }

        ES3.DeleteKey("ListOXdata");
        ES3.DeleteKey("ListSAdata");
        ES3.DeleteKey("ListSOdata");
        ES3.DeleteKey("ListSMdata");
        PlayerPrefs.DeleteAll();


    }

    
}
