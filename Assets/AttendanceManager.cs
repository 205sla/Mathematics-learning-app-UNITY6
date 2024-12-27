using UnityEngine;
using System;
using System.Collections.Generic;

public class AttendanceManager : MonoBehaviour
{
    private int consecutiveDays = 0;  // 연속 출석일 수
    private string lastAttendanceDateKey = "LastAttendanceDate";  // 마지막 출석일을 저장할 키

    List<string> attendanceMessages = new List<string>
{
    "첫 번째 학습! 오늘부터 시작이에요. 내일도 계속해서 이어가 보세요!",
    "두 번째 날! 학습을 연속으로 이어가고 있어요. 오늘도 잘 했어요!",
    "3일째 학습! 조금씩 더 나아가고 있네요. 내일도 화이팅!",
    "4일 연속 학습! 조금씩 습관이 되고 있어요. 계속 도전하세요!",
    "5일째! 한 주의 절반이 지났어요. 이렇게 꾸준히 하면 큰 성과가 있어요!",
    "6일 연속 학습! 점점 더 익숙해지고 있죠. 내일도 기대돼요!",
    "7일 연속 학습! 한 주를 마무리했어요. 이제는 정말 습관이 되어가고 있어요!",
    "8일째! 한 주를 넘어섰어요. 더 큰 목표를 향해 나아가고 있어요!",
    "9일 연속 학습! 이제 거의 10일이에요. 꾸준함이 힘이 되어줄 거예요!",
    "10일 연속 학습! 정말 대단해요. 이 작은 습관들이 큰 변화를 만들어요!",
    "11일째! 조금 더 힘내서 계속 이어나가면 더 큰 성취가 기다리고 있어요!",
    "12일 연속 학습! 점점 더 좋은 결과를 가져올 거예요. 끝까지 가보세요!",
    "13일째! 이제 학습이 자연스러워지고 있죠? 내일도 계속해서 도전해보세요!",
    "14일 연속 학습! 두 주 연속 학습! 정말 자랑스러워요!",
    "15일! 이제 거의 2주가 지났어요. 학습을 꾸준히 이어가고 있어요!",
    "16일째! 매일 조금씩 더 나아지고 있어요. 내일도 꼭 이어가세요!",
    "17일 연속 학습! 하루하루 성과가 쌓여가고 있어요. 정말 멋져요!",
    "18일째! 이제 학습이 일상이 되었어요. 계속해서 힘내요!",
    "19일 연속 학습! 멋진 연속 학습 기록을 세우고 있어요. 계속해서 도전해보세요!",
    "20일 연속 학습! 대단해요! 꾸준히 가면 큰 성과가 올 거예요!",
    "21일째! 3주 연속 학습! 이제는 학습이 완벽한 습관이 되었어요!",
    "22일 연속 학습! 정말 꾸준하게 이어가고 있어요. 당신의 노력은 반드시 보답받을 거예요!",
    "23일째! 조금씩 더 나아지고 있네요. 매일 학습은 큰 변화를 가져와요!",
    "24일 연속 학습! 이렇게 꾸준히 하면 더 큰 성과가 올 거예요!",
    "25일째! 목표까지 한 걸음씩 다가가고 있어요. 계속해서 이어가세요!",
    "26일 연속 학습! 진정한 꾸준함이 빛을 발하는 순간이에요!",
    "27일째! 이제 학습이 정말 일상이 되었죠? 계속해서 멋진 성과를 보여주세요!",
    "28일 연속 학습! 한 달이 거의 다 되어 가요. 그동안의 노력이 정말 멋져요!",
    "29일째! 정말 멋져요! 끝까지 꾸준히 가면 더 큰 변화가 있을 거예요!",
    "30일 연속 학습! 한 달을 넘겼어요. 이제 학습이 당신의 일부가 되었어요. 계속해서 더 큰 목표를 향해 나아가세요!"
};

    void Start()
    {
        // 연속 출석일 카운트 갱신
        CheckAttendance();
    }

    void CheckAttendance()
    {
        // 현재 날짜를 가져옵니다.
        string currentDate = DateTime.Now.ToString("yyyy-MM-dd");

        // 저장된 마지막 출석일을 가져옵니다.
        string lastAttendanceDate = "";
        if (ES3.KeyExists(lastAttendanceDateKey))
        {
            lastAttendanceDate = ES3.Load<string>(lastAttendanceDateKey);
        }
         

        // 첫 번째 출석이라면 연속 출석일을 1로 설정
        if (lastAttendanceDate == "")
        {
            consecutiveDays = 1;
            Debug.Log("첫 출석입니다.");
        }
        else
        {
            // 마지막 출석일이 현재 날짜와 하루 차이가 나는지 확인
            DateTime lastDate = DateTime.Parse(lastAttendanceDate);
            DateTime current = DateTime.Now;

            // 하루 차이가 나는지 확인 (연속 출석인지 확인)
            if ((current - lastDate).Days == 1)
            {
                consecutiveDays++;  // 연속 출석일 증가
                Debug.Log("연속 출석: " + consecutiveDays + "일");
            }
            else if ((current - lastDate).Days > 1)
            {
                consecutiveDays = 1;  // 출석을 놓친 경우 연속 출석 초기화
                Debug.Log("연속 출석이 끊어졌습니다.");
            }
            else
            {
                Debug.Log("이미 오늘 출석했습니다.");
            }
        }

        // 현재 날짜를 저장하여, 다음 출석 시 비교할 수 있게 합니다.
        ES3.Save(lastAttendanceDateKey, currentDate);  // ES3로 저장
    }

    // 연속 출석일을 반환하는 메서드
    public string GetConsecutiveDays()
    {
        string tmepS = "";

        // 연속 학습일에 맞는 메시지를 리스트에서 가져오기
        if (consecutiveDays > 0 && consecutiveDays <= 30)
        {
            tmepS = attendanceMessages[consecutiveDays - 1];
        }
        else
        {
            tmepS = consecutiveDays + "일 연속 학습! 정말 대단해요! 이렇게 꾸준히 하면 큰 성과가 있을 거예요!";
        }

        return tmepS;
    }


}
