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
        "ù ��° �н�! ���ú��� �����̿���. ���ϵ� ����ؼ� �̾ ������!",
        "�� ��° ��! �н��� �������� �̾�� �־��. ���õ� �� �߾��!",
        "3��° �н�! ���ݾ� �� ���ư��� �ֳ׿�. ���ϵ� ȭ����!",
        "4�� ���� �н�! ���ݾ� ������ �ǰ� �־��. ��� �����ϼ���!",
        "5��°! �� ���� ������ �������. �̷��� ������ �ϸ� ū ������ �־��!",
        "6�� ���� �н�! ���� �� �ͼ������� ����. ���ϵ� ���ſ�!",
        "7�� ���� �н�! �� �ָ� �������߾��. ������ ���� ������ �Ǿ�� �־��!",
        "8��°! �� �ָ� �Ѿ���. �� ū ��ǥ�� ���� ���ư��� �־��!",
        "9�� ���� �н�! ���� ���� 10���̿���. �������� ���� �Ǿ��� �ſ���!",
        "10�� ���� �н�! ���� ����ؿ�. �� ���� �������� ū ��ȭ�� ������!",
        "11��°! ���� �� ������ ��� �̾���� �� ū ���밡 ��ٸ��� �־��!",
        "12�� ���� �н�! ���� �� ���� ����� ������ �ſ���. ������ ��������!",
        "13��°! ���� �н��� �ڿ����������� ����? ���ϵ� ����ؼ� �����غ�����!",
        "14�� ���� �н�! �� �� ���� �н�! ���� �ڶ���������!",
        "15��! ���� ���� 2�ְ� �������. �н��� ������ �̾�� �־��!",
        "16��°! ���� ���ݾ� �� �������� �־��. ���ϵ� �� �̾����!",
        "17�� ���� �н�! �Ϸ��Ϸ� ������ �׿����� �־��. ���� ������!",
        "18��°! ���� �н��� �ϻ��� �Ǿ����. ����ؼ� ������!",
        "19�� ���� �н�! ���� ���� �н� ����� ����� �־��. ����ؼ� �����غ�����!",
        "20�� ���� �н�! ����ؿ�! ������ ���� ū ������ �� �ſ���!",
        "21��°! 3�� ���� �н�! ������ �н��� �Ϻ��� ������ �Ǿ����!",
        "22�� ���� �н�! ���� �����ϰ� �̾�� �־��. ����� ����� �ݵ�� ������� �ſ���!",
        "23��°! ���ݾ� �� �������� �ֳ׿�. ���� �н��� ū ��ȭ�� �����Ϳ�!",
        "24�� ���� �н�! �̷��� ������ �ϸ� �� ū ������ �� �ſ���!",
        "25��°! ��ǥ���� �� ������ �ٰ����� �־��. ����ؼ� �̾����!",
        "26�� ���� �н�! ������ �������� ���� ���ϴ� �����̿���!",
        "27��°! ���� �н��� ���� �ϻ��� �Ǿ���? ����ؼ� ���� ������ �����ּ���!",
        "28�� ���� �н�! �� ���� ���� �� �Ǿ� ����. �׵����� ����� ���� ������!",
        "29��°! ���� ������! ������ ������ ���� �� ū ��ȭ�� ���� �ſ���!",
        "30�� ���� �н�! �� ���� �Ѱ���. ���� �н��� ����� �Ϻΰ� �Ǿ����. ����ؼ� �� ū ��ǥ�� ���� ���ư�����!"
    };
    List<string> multipleAttendanceMessages = new List<string>
    {
    // 2��° �н�
    "�� ��° �н�! ������ ���� ���߷� ���! �� ��° �н��� ������ �������ϼ���!",

    // 3��° �н�
    "�� ��° �н�! �̷��� �����ؼ� �н��ϴ� ����� ���� ������. ����ؼ� ��������!",

    // 4��° �н�
    "�� ��° �н�! �̹� 4���̳� �н��߾��! ������ ���� �� ū ������ ��ٸ��� �־��!",

    // 5��° �н�
    "�ټ� ��° �н�! �̷��� �����ؼ� �н��ϴ� ����� ���� ����ؿ�. ���ϵ� ��� �̾����!",

    // 6��° �н�
    "���� ��° �н�! �� ��, �� ��, �� ��... ���� 6��! �̷��� ������ ���� ū ��ȭ�� ���� �� �־��!",

    // 7��° �н�
    "�ϰ� ��° �н�! �Ϸ翡 �̷��� ���� �н��� �Ѵٴ� �� ���� ����� ���̿���. ����ؼ� �����ϼ���!",

    // 8��° �н�
    "���� ��° �н�! ���� �н��� ������ ������ �Ǿ����. �������� ���� �����׿�!",

    // 9��° �н�
    "��ȩ ��° �н�! ���� ����ؿ�! �Ϸ翡 9���̳� �н��� �� �ִ� ���߷��� �����ְ� �־��!",

    // 10��° �н�
    "�� ��° �н�! ����ؿ�! �̷��� �� ���̳� �н��ϴ� �� ���� ���� ��ǥ �޼��� �����̿���!",

    // 11��° �н� �̻�
    "������ ���� ������ �н��ϰ� �־��! �̷��� ����ؼ� �н��� �̾�� ū ������ ���� �ſ���!"
    };


    void CheckAttendance()
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
                return;
            }

            DateTime current;
            if (!DateTime.TryParse(currentDate, out current))
            {
                Debug.LogError("�߸��� ��¥ �����Դϴ�.");
                return;
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
                ES3.Save(todayCountKey, todayCount);
                return; // �̹� ���� �⼮�� ��� �߰� ������ �������� ����
            }
        }

        // ���� ��¥�� �����Ͽ�, ���� �⼮ �� ���� �� �ְ� �մϴ�.
        ES3.Save(lastAttendanceDateKey, currentDate);  // ES3�� ����
        ES3.Save(consecutiveDaysKey, consecutiveDays);  // ���� �⼮�� �� ����
    }

    // ���� �⼮���� ��ȯ�ϴ� �޼���
    public string GetConsecutiveDays()
    {
        CheckAttendance();
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
                tmepS = consecutiveDays + "�� ���� �н�! ���� ����ؿ�! �̷��� ������ �ϸ� ū ������ ���� �ſ���!";
            }
        }
        else
        {
            int messageIndex = todayCount - 2;  // 2��° �⼮���� ��Ʈ ����
            if (messageIndex >= 0 && messageIndex < multipleAttendanceMessages.Count)
            {
                tmepS = multipleAttendanceMessages[messageIndex];
            }
            else
            {
                tmepS = "���õ� �н��� �����ϰ� �ֳ׿�! ����ؼ� ��ǥ�� ���� ���ư�����!";
            }
        }

        return tmepS;
    }
}
