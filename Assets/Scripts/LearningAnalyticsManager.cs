using Michsky.MUIP; // MUIP 네임스페이스
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LearningAnalyticsManager : MonoBehaviour
{
    [SerializeField]
    TMP_Text title, perfectScoreJokeTMP;

    [SerializeField] private PieChart myChart; // 차트 변수

    [SerializeField]
    GameObject PieChart, Btnn, perfectScoreJoke;

    int courseNum = 0;
    List<List<List<string>>> existingData = new List<List<List<string>>>();

    List<string> perfectScoreJokes = new List<string>
{
    "오답 데이터?\n아마 당신의 만점에 겁먹고 도망갔나 봐요!",
    "축하해요!\n오답 그래프가 텅 비었어요.\n당신이 모든 문제를 완벽하게 해결했으니까요!",
    "이 앱에서는 오답이 필요 없어요.\n왜냐하면 당신은 수학의 천재, 만점자니까요!",
    "오답은 상상 속의 존재일 뿐!\n당신의 만점이 모든 걸 말해주네요.",
    "수학의 신이 나타난 모양!\n오답 그래프는 빈 캔버스일 뿐이에요.",
    "만점의 비결?\n오답을 만들 생각조차 안 하는 천재적인 두뇌!",
    "당신의 점수가 오답 없는 예술작품이네요.\n진정한 수학 예술가입니다!",
    "문제들을 전부 정복한 당신!\n오답은 문 밖으로 쫓겨난 모양이에요.",
    "오답 데이터가 도망갔어요.\n당신의 완벽한 계산 실력에 겁먹은 모양입니다!",
    "만점의 마술!\n오답 그래프가 0인 건 당신의 비법이 있음을 증명합니다.",
    "오답 그래프가 공백이라니,\n오답이 당신의 천재성을 인정하지 않은 건가봐요!",
    "만점이면 오답은 필요 없죠.\n당신의 점수가 그걸 말해주고 있어요!",
    "수학의 법칙:\n만점자에게는 오답이 존재할 수 없어요.\n축하합니다!",
    "오답 그래프가 0인 건,\n당신이 문제를 전부 정복했기 때문이겠죠?",
    "천재의 증거!\n오답 그래프가 비어 있는 건\n당신의 계산이 완벽하기 때문입니다.",
    "만점이면 오답은 꿈속에서나 존재하는 법!\n환상적인 수학 실력, 대단해요!",
    "당신의 완벽한 점수 덕분에\n오답 그래프가 예술작품처럼 공허하게 빛나고 있어요.",
    "오답 데이터를 찾을 수 없다면,\n그것은 당신이 모든 문제를 정답으로 해결했기 때문입니다!",
    "완벽한 계산!\n오답 그래프가 0인 건\n당신이 수학계의 전설임을 증명합니다.",
    "오답은 사라지고, 만점만이 남았어요.\n당신의 천재성이 그 모든 것을 말해주네요!"
};


    private void Start()
    {
        if (ES3.KeyExists("courseNum"))
        {
            courseNum = ES3.Load<int>("courseNum");

            if (ES3.KeyExists("WrongProblemData"))
            {
                // 기존 데이터 로드
                existingData = ES3.Load<List<List<List<string>>>>("WrongProblemData");
                Debug.Log("기존 데이터 로드 완료." + courseNum);
                title.text = existingData[courseNum][0][1];

                MakePieChart();

            }
            else
            {
                Debug.Log("오류");
            }
        }
        else
        {
            Debug.Log("오류");
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

        // 첫 번째 리스트는 헤더 정보라고 가정(예: 날짜, 제목 등)
        // 문제 정보는 두 번째 요소(index 1)부터 시작합니다.
        for (int i = 1; i < existingData[courseNum].Count; i++)
        {
            List<string> problemInfo = existingData[courseNum][i];

            // 문제 정보 리스트가 최소 4개의 항목(인덱스 0~3)을 가지고 있는지 확인
            if (problemInfo.Count > 3)
            {
                // 인덱스 3에 단원 정보가 담겨있다고 가정 ("소인수분해" 등)
                string chapter = problemInfo[3];

                // 이미 해당 단원이 있으면 카운트를 증가시키고, 없으면 새로 추가
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
        {// chartData 리스트의 마지막 인덱스가 새로 추가된 항목임
            int newIndex = myChart.chartData.Count - 1;

            // 새 항목의 이름 설정
            myChart.chartData[newIndex].name = kv.Key;

            // 새 항목의 색상 설정
            myChart.chartData[newIndex].color = new Color32(
                (byte)UnityEngine.Random.Range(0, 256),
                (byte)UnityEngine.Random.Range(0, 256),
                (byte)UnityEngine.Random.Range(0, 256),
                255);

            // 새 항목의 값 설정
            myChart.chartData[newIndex].value = kv.Value;

            myChart.UpdateIndicators(); // 모든 레이블 지표 업데이트
            j++;
            if (j != chapterCounts.Count)
            {
                myChart.AddNewItem(); // 새로운 차트 항목 추가\
            }
            
        }



    }

    public void Btn()
    {

        GameManager.instance.LoadScene("MakingCourse");
    }
}
