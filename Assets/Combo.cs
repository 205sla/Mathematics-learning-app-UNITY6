using System.Collections;
using System.Text.RegularExpressions;
using TMPro;  // TextMesh Pro 네임스페이스 추가
using UnityEngine;

public class Combo : MonoBehaviour
{
    [SerializeField]
    TMP_Text ComboNum, ComboStr;
    
    public void SetCombo(string comboNum, string comboStr)
    {
        Debug.Log("콤보 출력 마스터");
        ComboNum.text = comboNum + "문제 연속 정답";
        ComboStr.text = AddNewLineAfterPunctuation(comboStr);
    }

    static string AddNewLineAfterPunctuation(string input)
    {
        // !나 , 뒤에 \n을 추가
        string pattern = @"([!,])";  // ! 또는 ,를 찾아서
        string replacement = "$1\n";  // 해당 문자를 찾고 그 뒤에 \n을 추가

        // Replace 실행
        return Regex.Replace(input, pattern, replacement);
    }

}
