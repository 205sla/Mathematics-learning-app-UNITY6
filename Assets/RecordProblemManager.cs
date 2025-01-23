using Michsky.MUIP;
using System.Collections.Generic;
using UnityEngine;

public class RecordProblemManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        List<List<List<string>>> Datas = new List<List<List<string>>>();

        if (ES3.KeyExists("WrongProblemData"))
        {
            // ���� ������ �ε�
            Datas = ES3.Load<List<List<List<string>>>>("WrongProblemData");
            Debug.Log("���� ������ �ε� �Ϸ�.");
        }

        for (int i = 0; i < 10; i++) {
            if (i < Datas.Count) {
                
                button[i].gameObject.SetActive(true);
                button[i].GetComponent<ButtonManager>().SetText(Datas[i][0][0]);

            }
        }
    }

    // Update is called once per frame
    public void Btn(int num)
    {
           
            ES3.Save("course", "���亹��"+num.ToString());
            GameManager.instance.LoadScene("MakingCourse");
        
    }
}
