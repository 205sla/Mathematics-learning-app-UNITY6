using System;
using System.Collections.Generic;
using UnityEngine;

public class AttendanceManager : MonoBehaviour
{
    private int consecutiveDays = 0;  // ���� �⼮�� ��
    private string lastAttendanceDateKey = "LastAttendanceDate";  // ������ �⼮���� ������ Ű
    private string consecutiveDaysKey = "ConsecutiveDays";  // ���� �⼮�� ���� ������ Ű
    private string todayCountKey = "ToDayCount";  // ���� �⼮�� ���� ������ Ű
    private int todayCount = 0;

    List<string> attendanceMessages = new List<string>
    {
        "ù ���� ���𵱾��! ���� ������ �˿� ���� �غ� �Ǽ̳���?",
    "�� ��° ����! Ȥ�� ���� õ���� ����� ����������?",
    "3��°���, �� ������ ���а� ��ģ �����ؾ߰ڴµ���?",
    "���а� �Բ��� 4��! ���� �� ��������? �� ���ص� �����ƿ�!",
    "5�� �����̶��, ���� ���п� ���� ������ �����̿���!",
    "6��° ���� ����! ���� ������ ���� ưư������ �ִ��. (��ó: �����)",
    "������ �����̳�! ���нŵ� ��� �ջ� ġ�� �����ɿ�?",
    "8��°! ������� �� �迡 80�ϱ��� �޷����� �� ����?",
    "9�� ���� ���а� ���� ��! ���� �׸� ���� �����?",
    "10��! ���� �� �ڸ���! ���� ������� ������� ��� ��!",
    "11��°���, Ȥ�� ��и��� ���� ���� �ӽ����� ���� ���ΰ���?",
    "12�� ���� �н�! ���Ϻ��ʹ� ������ �˾Ƽ� Ǯ���ڴµ���?",
    "13��°, ������ �ӵ��� �ȶ������� �־��! (�츮�� ���� �ִ�)",
    "2��° �н�! 14�� ���� �� ������. �׳����� �������� �� �ǰ���?",
    "15�� �����̶��, ���� �Ǹ� ������ ������ �Ǵ� ���� �پ �˴ϴ�!",
    "16��° ����! �н��� ������ �� ���, ���õ� ���� �ʾƿ�!",
    "17��°! ���н��� ���õ� �����ƿ䡯�� �������ϴ�.",
    "18�� ����! ���� ���� å�� ����� ���� ����ĥ �غ� �� �̴ϴ�.",
    "19��°! Ȥ�� ���õ� Ǯ�� �������� ���� ���� �ʾҳ���?",
    "20�� ���� �н��̶��! �� ������ ���� ���� �����̿���!",
    "21��°! Ȥ�� �޷¿� ������ ����� ������ ����ϰ� �ֳ���?",
    "22��° ����! ���� ������ ���� ���̰͵� ���� �� �Ա���!�� �ϰ���?",
    "23�� ����! ����� ���� ������ ���� �ڼ��� �����ϴ�!",
    "24��°! ���� ���� ����ü �� ģ���� ���� ���߳��� �ϰ� �ñ����Ѵ��.",
    "25�� ����! ���� �Ǹ� ���� ������ �޿����� Ǯ�� �������� �����.",
    "26��°! ���� ����� ������ �������� ���翡 ���� �غ� ���Դϴ�.",
    "27�� ���� �н�! ���ݸ� �� ��������. 30�� �Ŀ� ������ �˴ϴ�!",
    "28��°���! Ȥ�� ������ �Ű� ����̶� �����̳���?",
    "29��°! 30�� �޼� �� ����(?) ������ ��ٸ��� �������� �𸨴ϴ�!",
    "30�� �����̶��! ������ �հ��� ��� ���Դϴ�. (��¥ �հ��� ��������!)"
    };
    List<string> multipleAttendanceMessages = new List<string>
    {

    "�� �����δ� �����߱���! �����̶� ����Ʈ ���ΰ���?",
    "�� ��° ����! �� ������ ���а� ��ģ ���� ����!",
    "���� �� ��°���! Ȥ�� ������ ���� �����ΰ���?",
    "�ټ� ��°��� ����ؿ�! ���� �����鵵 ��� �ں����� �ſ���!",
    "���ø� ���� ��?! �̷��� �������� ���, ������ ���� �ѵ����ϰھ��.",
    "�ϰ� ��° ����! ���� ������ �Ҳ��Ҳ� �ڶ�� ��!",
    "���� ��° �н��̶��! ���� ���� ������ ���� ������ ������?",
    "��ȩ ���̳� �����ϴٴ�! �������� ����� �η����ϱ� �����߾��!",
    "�� ��°! Ȥ�� ���� ���а� ����� ���� �� �ƴ���?",
    "���� ��° �н��̶��! Ȥ�� ������ �ְ��ڡ���� Ÿ��Ʋ�� �븮�� ���ΰ���?",
    "���� ��° ����! ���� �������� ����� ���Ϸ��� ������ �����.",
    "���� ��°���! Ȥ�� ���� ������ ���Ρ� ������ �����ΰ���?",
    "���� ���̶��! �̰� ��¥ ������ ���� ����Դϴ�.",
    "���ټ� ��°���! ���� �����鵵 �������� ������� ����...����� ���� �� ���ƿ�."
    };

    List<string> ContinuousLearningMessages = new List<string>
    {
"�� ���� �н�! �������� �� �Ƿ��Դϴ�!",
"�� ���� �н�! ���õ� �����̶� ���� �Ϸ� ��������!",
"�� ���� �н�! ����ؿ�! ���� ���� ������ �ҷ��� �� ��?",
 "�� ���� �н�! Ȥ�� ���������� ȭ�š��̶�� ������ �ֳ���?",
"�� ���� �н�! ������ ���� è�Ǿ��� ���⿡ �ֽ��ϴ�!",
"�� ���� �н�! ���ϸ��� �����ϴ� ���, �ְ���!",
  "�� ���� �н�! ���нŵ� ��� ���� ��ư�� �������!",
     "�� ���� �н�! Ȥ�� �����̶� ��� ����̶� �����̳���?",
    "�� ���� �н�! �� ������ ���е� ��ſ��� ���߰ھ��!",
    "�� ���� �н�! ��� �̷��� ���� ���̽��� �����غ���!"
    };
    public string GetRandomMessage()
    {
        System.Random random = new System.Random();
        int index = random.Next(ContinuousLearningMessages.Count);
        return ContinuousLearningMessages[index];
    }
    void CheckAttendance()
    {

    }

    // ���� �⼮���� ��ȯ�ϴ� �޼���
    public string GetConsecutiveDays()
    {
        // ���� ��¥�� �����ɴϴ�.
        string currentDate = DateTime.Now.Date.ToString("yyyy-MM-dd");  // ��¥�� ��


        Debug.Log("���� ��¥: " + currentDate);

        // ����� ������ �⼮���� �����ɴϴ�.
        string lastAttendanceDate = "";
        if (ES3.KeyExists(lastAttendanceDateKey))
        {
            lastAttendanceDate = ES3.Load<string>(lastAttendanceDateKey);
        }

        if (ES3.KeyExists(todayCountKey))
        {
            todayCount = ES3.Load<int>(todayCountKey);
        }

        // ����� ���� �⼮�� ���� �����ɴϴ�.
        if (ES3.KeyExists(consecutiveDaysKey))
        {
            consecutiveDays = ES3.Load<int>(consecutiveDaysKey);
        }

        // ù ��° �⼮�̶�� ���� �⼮���� 1�� ����
        if (lastAttendanceDate == "")
        {
            consecutiveDays = 1;
            Debug.Log("ù �⼮�Դϴ�.");
        }
        else
        {
            // ������ �⼮���� ���� ��¥�� �Ϸ� ���̰� ������ Ȯ��
            DateTime lastDate;
            if (!DateTime.TryParse(lastAttendanceDate, out lastDate))
            {
                Debug.LogError("�߸��� ��¥ �����Դϴ�.");
            }

            DateTime current;
            if (!DateTime.TryParse(currentDate, out current))
            {
                Debug.LogError("�߸��� ��¥ �����Դϴ�.");
            }




            Debug.Log("������¥: " + lastDate + "���� ��¥: :" + current);
            // �Ϸ� ���̰� ������ Ȯ�� (���� �⼮���� Ȯ��)
            if ((current - lastDate).Days == 1)
            {
                consecutiveDays++;  // ���� �⼮�� ����
                Debug.Log("���� �⼮: " + consecutiveDays + "��");
                todayCount = 1;
            }
            else if ((current - lastDate).Days > 1)
            {
                consecutiveDays = 1;  // �⼮�� ��ģ ��� ���� �⼮ �ʱ�ȭ
                Debug.Log("���� �⼮�� ���������ϴ�.");
                todayCount = 1;
            }
            else
            {
                Debug.Log("�̹� ���� �⼮�߽��ϴ�.");
                todayCount += 1;


            }
            ES3.Save(todayCountKey, todayCount);
        }

        // ���� ��¥�� �����Ͽ�, ���� �⼮ �� ���� �� �ְ� �մϴ�.
        ES3.Save(lastAttendanceDateKey, currentDate);  // ES3�� ����
        ES3.Save(consecutiveDaysKey, consecutiveDays);  // ���� �⼮�� �� ����


        string tmepS = "";
        Debug.Log("���� �⼮��" + consecutiveDays);
        // ���� �н��Ͽ� �´� �޽����� ����Ʈ���� ��������
        if (todayCount == 1)
        {
            if (consecutiveDays > 0 && consecutiveDays <= 30)
            {
                tmepS = attendanceMessages[consecutiveDays - 1];
            }
            else
            {
                tmepS = consecutiveDays + GetRandomMessage();
            }
        }
        else
        {
            int messageIndex = todayCount - 2;  // 2��° �⼮���� ��Ʈ ����
            Debug.Log("���� �⼮ Ƚ��" + todayCount);
            if (messageIndex >= 0 && messageIndex < multipleAttendanceMessages.Count)
            {
                tmepS = multipleAttendanceMessages[messageIndex];
            }
            else
            {
                tmepS = "���� �Ǹ� ���� ������ �ƴ϶� ���ο� ������ Ǫ�� ���ΰ���?";
            }
        }

        return tmepS;
    }
}
