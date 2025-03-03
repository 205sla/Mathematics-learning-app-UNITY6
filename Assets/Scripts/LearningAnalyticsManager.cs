using Michsky.MUIP; // MUIP ���ӽ����̽�
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LearningAnalyticsManager : MonoBehaviour
{
    [SerializeField]
    TMP_Text title, perfectScoreJokeTMP;

    [SerializeField] private PieChart myChart; // ��Ʈ ����

    [SerializeField]
    GameObject PieChart, Btnn, perfectScoreJoke;

    int courseNum = 0;
    List<List<List<string>>> existingData = new List<List<List<string>>>();

    List<string> perfectScoreJokes = new List<string>
{
    "���� ������?\n�Ƹ� ����� ������ �̸԰� �������� ����!",
    "�����ؿ�!\n���� �׷����� �� ������.\n����� ��� ������ �Ϻ��ϰ� �ذ������ϱ��!",
    "�� �ۿ����� ������ �ʿ� �����.\n�ֳ��ϸ� ����� ������ õ��, �����ڴϱ��!",
    "������ ��� ���� ������ ��!\n����� ������ ��� �� �����ֳ׿�.",
    "������ ���� ��Ÿ�� ���!\n���� �׷����� �� ĵ������ ���̿���.",
    "������ ���?\n������ ���� �������� �� �ϴ� õ������ �γ�!",
    "����� ������ ���� ���� ������ǰ�̳׿�.\n������ ���� �������Դϴ�!",
    "�������� ���� ������ ���!\n������ �� ������ �Ѱܳ� ����̿���.",
    "���� �����Ͱ� ���������.\n����� �Ϻ��� ��� �Ƿ¿� �̸��� ����Դϴ�!",
    "������ ����!\n���� �׷����� 0�� �� ����� ����� ������ �����մϴ�.",
    "���� �׷����� �����̶��,\n������ ����� õ�缺�� �������� ���� �ǰ�����!",
    "�����̸� ������ �ʿ� ����.\n����� ������ �װ� �����ְ� �־��!",
    "������ ��Ģ:\n�����ڿ��Դ� ������ ������ �� �����.\n�����մϴ�!",
    "���� �׷����� 0�� ��,\n����� ������ ���� �����߱� �����̰���?",
    "õ���� ����!\n���� �׷����� ��� �ִ� ��\n����� ����� �Ϻ��ϱ� �����Դϴ�.",
    "�����̸� ������ �޼ӿ����� �����ϴ� ��!\nȯ������ ���� �Ƿ�, ����ؿ�!",
    "����� �Ϻ��� ���� ���п�\n���� �׷����� ������ǰó�� �����ϰ� ������ �־��.",
    "���� �����͸� ã�� �� ���ٸ�,\n�װ��� ����� ��� ������ �������� �ذ��߱� �����Դϴ�!",
    "�Ϻ��� ���!\n���� �׷����� 0�� ��\n����� ���а��� �������� �����մϴ�.",
    "������ �������, �������� ���Ҿ��.\n����� õ�缺�� �� ��� ���� �����ֳ׿�!"
};


    private void Start()
    {
        if (ES3.KeyExists("courseNum"))
        {
            courseNum = ES3.Load<int>("courseNum");

            if (ES3.KeyExists("WrongProblemData"))
            {
                // ���� ������ �ε�
                existingData = ES3.Load<List<List<List<string>>>>("WrongProblemData");
                Debug.Log("���� ������ �ε� �Ϸ�." + courseNum);
                title.text = existingData[courseNum][0][1];

                MakePieChart();

            }
            else
            {
                Debug.Log("����");
            }
        }
        else
        {
            Debug.Log("����");
        }
    }

    void MakePieChart()
    {
        if (existingData[courseNum].Count == 1)
        {
            PieChart.gameObject.SetActive(false);
            Btnn.gameObject.SetActive(false);
            perfectScoreJoke.gameObject.SetActive(true);

            string randomJoke = perfectScoreJokes[UnityEngine.Random.Range(0, perfectScoreJokes.Count)];
            perfectScoreJokeTMP.text = randomJoke;

            return;
        }

        Dictionary<string, int> chapterCounts = new Dictionary<string, int>();

        // ù ��° ����Ʈ�� ��� ������� ����(��: ��¥, ���� ��)
        // ���� ������ �� ��° ���(index 1)���� �����մϴ�.
        for (int i = 1; i < existingData[courseNum].Count; i++)
        {
            List<string> problemInfo = existingData[courseNum][i];

            // ���� ���� ����Ʈ�� �ּ� 4���� �׸�(�ε��� 0~3)�� ������ �ִ��� Ȯ��
            if (problemInfo.Count > 3)
            {
                // �ε��� 3�� �ܿ� ������ ����ִٰ� ���� ("���μ�����" ��)
                string chapter = problemInfo[3];

                // �̹� �ش� �ܿ��� ������ ī��Ʈ�� ������Ű��, ������ ���� �߰�
                if (chapterCounts.ContainsKey(chapter))
                {
                    chapterCounts[chapter]++;
                }
                else
                {
                    chapterCounts[chapter] = 1;
                }
            }
        }

        int j = 0;
        foreach (var kv in chapterCounts)
        {// chartData ����Ʈ�� ������ �ε����� ���� �߰��� �׸���
            int newIndex = myChart.chartData.Count - 1;

            // �� �׸��� �̸� ����
            myChart.chartData[newIndex].name = kv.Key;

            // �� �׸��� ���� ����
            myChart.chartData[newIndex].color = new Color32(
                (byte)UnityEngine.Random.Range(0, 256),
                (byte)UnityEngine.Random.Range(0, 256),
                (byte)UnityEngine.Random.Range(0, 256),
                255);

            // �� �׸��� �� ����
            myChart.chartData[newIndex].value = kv.Value;

            myChart.UpdateIndicators(); // ��� ���̺� ��ǥ ������Ʈ
            j++;
            if (j != chapterCounts.Count)
            {
                myChart.AddNewItem(); // ���ο� ��Ʈ �׸� �߰�\
            }
            
        }



    }

    public void Btn()
    {

        GameManager.instance.LoadScene("MakingCourse");
    }
}
