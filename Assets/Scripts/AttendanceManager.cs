using System;
using System.Collections.Generic;
using UnityEngine;

public class AttendanceManager : MonoBehaviour
{
    private int consecutiveDays = 0;  // 연속 출석일 수
    private string lastAttendanceDateKey = "LastAttendanceDate";  // 마지막 출석일을 저장할 키
    private string consecutiveDaysKey = "ConsecutiveDays";  // 연속 출석일 수를 저장할 키
    private string todayCountKey = "ToDayCount";  // 연속 출석일 수를 저장할 키
    private int todayCount = 0;

    List<string> attendanceMessages = new List<string>
    {
        "첫 발을 내디뎠어요! 이젠 수학의 늪에 빠질 준비 되셨나요?",
    "두 번째 도전! 혹시 벌써 천재의 기운이 느껴지나요?",
    "3일째라니, 이 정도면 수학과 절친 선언해야겠는데요?",
    "수학과 함께한 4일! 이제 좀 편해졌죠? 안 편해도 괜찮아요!",
    "5일 연속이라니, 벌써 수학왕 국왕 직함이 눈앞이에요!",
    "6일째 도전 성공! 수학 근육이 슬슬 튼튼해지고 있대요. (출처: 비공식)",
    "일주일 동안이나! 수학신도 놀라서 손뼉 치고 있을걸요?",
    "8일째! 여기까지 온 김에 80일까지 달려보는 건 어떨까요?",
    "9일 동안 수학과 동거 중! 이제 그만 정이 들었죠?",
    "10일! 벌써 두 자릿수! 수학 퀴즈계의 레전드로 등극 중!",
    "11일째라니, 혹시 비밀리에 수학 공부 머신으로 개조 중인가요?",
    "12일 연속 학습! 내일부터는 문제도 알아서 풀리겠는데요?",
    "13일째, 무서운 속도로 똑똑해지고 있어요! (우리는 보고 있다)",
    "2주째 학습! 14일 동안 잘 버텼어요. 그나저나 언제까지 갈 건가요?",
    "15일 연속이라니, 이쯤 되면 수학의 달인이 되는 꿈을 꾸어도 됩니다!",
    "16일째 성공! 학습의 달인이 된 당신, 오늘도 쉬지 않아요!",
    "17일째! 수학신이 오늘도 ‘좋아요’를 눌렀습니다.",
    "18일 연속! 이제 수학 책도 당신을 보면 도망칠 준비를 할 겁니다.",
    "19일째! 혹시 오늘도 풀릴 문제들이 떨고 있진 않았나요?",
    "20일 연속 학습이라니! 이 정도면 수학 덕후 인정이에요!",
    "21일째! 혹시 달력에 ‘수학 사랑의 날’로 기념하고 있나요?",
    "22일째 성공! 이제 문제를 보면 ‘이것도 식은 죽 먹기지!’ 하겠죠?",
    "23일 연속! 당신의 수학 열정에 나도 박수를 보냅니다!",
    "24일째! 수학 신이 ‘대체 저 친구는 언제 멈추나’ 하고 궁금해한대요.",
    "25일 연속! 이쯤 되면 수학 문제를 꿈에서도 풀고 있을지도 몰라요.",
    "26일째! 이제 당신은 ‘수학 전설’로 역사에 남을 준비 중입니다.",
    "27일 연속 학습! 조금만 더 힘내세요. 30일 후엔 전설이 됩니다!",
    "28일째라니! 혹시 수학의 신과 계약이라도 맺으셨나요?",
    "29일째! 30일 달성 시 멋진(?) 보상이 기다리고 있을지도 모릅니다!",
    "30일 연속이라니! 수학의 왕관은 당신 것입니다. (진짜 왕관은 없지만요!)"
    };
    List<string> multipleAttendanceMessages = new List<string>
    {

    "한 번으로는 부족했군요! 수학이랑 데이트 중인가요?",
    "세 번째 도전! 이 정도면 수학과 절친 인증 가능!",
    "오늘 네 번째라니! 혹시 숨겨진 수학 덕후인가요?",
    "다섯 번째라니 대단해요! 수학 문제들도 놀라 자빠졌을 거예요!",
    "오늘만 여섯 번?! 이렇게 열정적인 모습, 수학의 신이 뿌듯해하겠어요.",
    "일곱 번째 도전! 수학 근육이 불끈불끈 자라는 중!",
    "여덟 번째 학습이라니! 이젠 수학 문제를 보면 웃음이 나겠죠?",
    "아홉 번이나 도전하다니! 문제들이 당신을 두려워하기 시작했어요!",
    "열 번째! 혹시 오늘 수학과 사랑에 빠진 건 아니죠?",
    "열한 번째 학습이라니! 혹시 ‘수학 최강자’라는 타이틀을 노리는 중인가요?",
    "열두 번째 도전! 이제 문제들이 당신을 피하려고 할지도 몰라요.",
    "열세 번째라니! 혹시 오늘 ‘수학 달인’ 레벨업 예정인가요?",
    "열네 번이라니! 이건 진짜 전설로 남을 기록입니다.",
    "열다섯 번째라니! 수학 문제들도 ‘오늘은 여기까지 하죠...’라고 말할 것 같아요."
    };

    List<string> ContinuousLearningMessages = new List<string>
    {
"일 연속 학습! 꾸준함이 곧 실력입니다!",
"일 연속 학습! 오늘도 수학이랑 멋진 하루 보내세요!",
"일 연속 학습! 대단해요! 이젠 수학 전설로 불러도 될 듯?",
 "일 연속 학습! 혹시 ‘꾸준함의 화신’이라는 별명이 있나요?",
"일 연속 학습! 진정한 수학 챔피언이 여기에 있습니다!",
"일 연속 학습! 매일매일 성장하는 당신, 최고예요!",
  "일 연속 학습! 수학신도 놀라서 구독 버튼을 눌렀대요!",
     "일 연속 학습! 혹시 수학이랑 비밀 계약이라도 맺으셨나요?",
    "일 연속 학습! 이 정도면 수학도 당신에게 반했겠어요!",
    "일 연속 학습! 계속 이렇게 멋진 페이스를 유지해봐요!"
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

    // 연속 출석일을 반환하는 메서드
    public string GetConsecutiveDays()
    {
        // 현재 날짜를 가져옵니다.
        string currentDate = DateTime.Now.Date.ToString("yyyy-MM-dd");  // 날짜만 비교


        Debug.Log("지금 날짜: " + currentDate);

        // 저장된 마지막 출석일을 가져옵니다.
        string lastAttendanceDate = "";
        if (ES3.KeyExists(lastAttendanceDateKey))
        {
            lastAttendanceDate = ES3.Load<string>(lastAttendanceDateKey);
        }

        if (ES3.KeyExists(todayCountKey))
        {
            todayCount = ES3.Load<int>(todayCountKey);
        }

        // 저장된 연속 출석일 수를 가져옵니다.
        if (ES3.KeyExists(consecutiveDaysKey))
        {
            consecutiveDays = ES3.Load<int>(consecutiveDaysKey);
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
            DateTime lastDate;
            if (!DateTime.TryParse(lastAttendanceDate, out lastDate))
            {
                Debug.LogError("잘못된 날짜 형식입니다.");
            }

            DateTime current;
            if (!DateTime.TryParse(currentDate, out current))
            {
                Debug.LogError("잘못된 날짜 형식입니다.");
            }




            Debug.Log("이전날짜: " + lastDate + "지금 날짜: :" + current);
            // 하루 차이가 나는지 확인 (연속 출석인지 확인)
            if ((current - lastDate).Days == 1)
            {
                consecutiveDays++;  // 연속 출석일 증가
                Debug.Log("연속 출석: " + consecutiveDays + "일");
                todayCount = 1;
            }
            else if ((current - lastDate).Days > 1)
            {
                consecutiveDays = 1;  // 출석을 놓친 경우 연속 출석 초기화
                Debug.Log("연속 출석이 끊어졌습니다.");
                todayCount = 1;
            }
            else
            {
                Debug.Log("이미 오늘 출석했습니다.");
                todayCount += 1;


            }
            ES3.Save(todayCountKey, todayCount);
        }

        // 현재 날짜를 저장하여, 다음 출석 시 비교할 수 있게 합니다.
        ES3.Save(lastAttendanceDateKey, currentDate);  // ES3로 저장
        ES3.Save(consecutiveDaysKey, consecutiveDays);  // 연속 출석일 수 저장


        string tmepS = "";
        Debug.Log("연속 출석일" + consecutiveDays);
        // 연속 학습일에 맞는 메시지를 리스트에서 가져오기
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
            int messageIndex = todayCount - 2;  // 2번째 출석부터 멘트 시작
            Debug.Log("오늘 출석 횟수" + todayCount);
            if (messageIndex >= 0 && messageIndex < multipleAttendanceMessages.Count)
            {
                tmepS = multipleAttendanceMessages[messageIndex];
            }
            else
            {
                tmepS = "이쯤 되면 수학 문제가 아니라 새로운 차원을 푸는 중인가요?";
            }
        }

        return tmepS;
    }
}
