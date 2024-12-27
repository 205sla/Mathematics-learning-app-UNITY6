using System.Collections;
using System.Text.RegularExpressions;
using TMPro;  // TextMesh Pro ���ӽ����̽� �߰�
using UnityEngine;

public class Combo : MonoBehaviour
{
    [SerializeField]
    TMP_Text ComboNum, ComboStr;
    
    public void SetCombo(string comboNum, string comboStr)
    {
        Debug.Log("�޺� ��� ������");
        ComboNum.text = comboNum + "���� ���� ����";
        ComboStr.text = AddNewLineAfterPunctuation(comboStr);
    }

    static string AddNewLineAfterPunctuation(string input)
    {
        // !�� , �ڿ� \n�� �߰�
        string pattern = @"([!,])";  // ! �Ǵ� ,�� ã�Ƽ�
        string replacement = "$1\n";  // �ش� ���ڸ� ã�� �� �ڿ� \n�� �߰�

        // Replace ����
        return Regex.Replace(input, pattern, replacement);
    }

}
