using System.Collections.Generic;
using UnityEngine;

public class ComplimentGenerator : MonoBehaviour
{
    List<string> Combo5 = new List<string>
    {
    "와, 5문제를 연속으로 다 맞혔어요! 대단해요, 수학 천재!",
    "5문제 연속 정답! 이제 수학 마스터가 된 거 같아요!",
    "정답! 5문제 연속으로 맞췄어요! 완벽해요!",
    "정확히 맞혔어요! 5문제를 연속으로 해결하다니, 멋져요!",
    "수학 고수 등극! 5문제를 연속으로 맞췄네요. 계속 이렇게 해봐요!",
    "끝내줬어요! 5문제 연속 정답! 이제 수학의 왕이네요!",
    "너무 멋지네요! 5문제 연속으로 맞추다니, 이제 누구도 당신을 막을 수 없겠어요!",
    "5문제를 연속으로 다 맞혔어요! 수학의 비밀을 풀어버렸네요!",
    "와우, 5문제 연속 정답! 계속 이대로 가면 수학 마스터!",
    "정답이 쭉쭉 나왔어요! 5문제를 연속으로 맞히다니, 대단해요!"
    };
    List<string> Combo10 = new List<string>
{
    "10문제 연속 정답! 이제 수학의 진정한 고수가 되셨어요!",
    "와, 10문제 연속 정답! 당신은 이제 수학 마스터를 넘어서 수학의 챔피언!",
    "어머나, 10문제를 연속으로 다 맞췄다니! 수학의 신이 되어 가는 중이에요!",
    "대단해요! 10문제를 연속 정답! 이제 수학에서 두려운 게 없겠어요!",
    "10문제 연속 정답! 이게 바로 수학 천재의 힘!",
    "우와! 10문제 연속으로 다 맞히다니, 정말 멋져요! 이제 수학을 지배하는 사람이 되었어요!",
    "10문제를 연속으로 맞히다니, 완벽하게 수학을 정복했네요! 최고예요!",
    "정확하게 10문제를 맞혔어요! 수학의 왕이 된 기분이죠?",
    "10문제를 연속으로 맞힌 당신, 이제 수학의 비밀을 모두 알 것 같아요!",
    "10문제 연속 정답! 당신의 수학 실력은 이제 전설이에요!"
};
    List<string> Combo15 = new List<string>
{
    "15문제 연속 정답! 이제 수학의 절대 고수로 등극했어요!",
    "와우! 15문제 연속 정답이라니! 당신은 수학의 전설이에요!",
    "어떻게 이렇게 잘 하나요? 15문제를 연속으로 다 맞히다니, 수학의 마법사네요!",
    "정말 대단해요! 15문제 연속으로 맞혔다니, 이제 수학의 왕으로 거듭났어요!",
    "15문제 연속 정답! 이 정도면 수학을 완전히 정복한 거나 다름없어요!",
    "대단해요! 15문제 연속으로 맞혔어요! 당신이야말로 진정한 수학의 천재!",
    "15문제를 연속으로 맞히다니, 이제 수학의 고수도 아니고, 수학의 신이에요!",
    "15문제 연속 정답! 수학을 완전히 장악한 모습, 정말 멋져요!",
    "정확하게 15문제를 맞혔어요! 이제 수학에 자신감을 갖고 나갈 준비 완료!",
    "15문제 연속 정답! 당신은 이제 수학의 챔피언이자 진정한 고수!",
    "15문제 연속으로 다 맞혔어요! 수학 실력이 상상 이상이에요!",
    "대단해요! 15문제를 연속으로 맞췄다는 건 진짜 기적 같아요!",
    "15문제 연속 정답! 이제 수학의 길을 누가 막을 수 있겠어요?",
    "정말 멋져요! 15문제 연속 맞추다니, 수학에서 최강자가 된 거 같아요!",
    "15문제를 연속으로 맞혔다니, 이젠 수학을 지배하는 사람이 되었어요! 최고!"
};


    List<string> Accuracy5 = new List<string>
{
    "와, 모든 문제를 맞혔어요! 이건 정말 대단한 성취예요!",
    "모든 문제를 완벽하게 맞추다니, 이제 수학의 마스터가 되었어요!",
    "전부 맞혔다니! 이제 수학을 정복한 진정한 고수!",
    "모든 문제를 다 맞혔어요! 이 정도면 수학의 신이라 불러도 손색없어요!",
    "완벽하게 모든 문제를 맞혔어요! 수학의 챔피언, 바로 당신이에요!",
    "모든 문제를 정확히 맞혔어요! 수학의 모든 비밀을 풀어버린 거 같아요!",
    "모든 문제를 다 맞힌 당신, 정말 멋져요! 이제 수학을 완전히 지배하는 사람!",
    "와우, 모든 문제를 맞히다니! 이제 수학에서는 당신이 최고예요!",
    "모든 문제를 정확히 풀었어요! 이건 정말 대단한 성취예요!",
    "전부 맞췄다니, 이제 수학에서 더 이상 도전할 게 없겠어요! 최고예요!",
    "모든 문제를 맞히다니, 진정한 수학의 천재! 정말 멋져요!",
    "모든 문제를 완벽하게 해결했어요! 당신은 이제 수학의 고수가 확실해요!",
    "모든 문제를 다 맞혔다는 건, 이제 수학을 정복했다는 뜻이에요! 정말 최고!",
    "완벽하게 모든 문제를 맞혔어요! 수학에서 이제 막힐 일은 없겠네요!",
    "모든 문제를 정확히 맞혔다니! 이 정도 실력은 정말 대단해요!",
    "정말 멋져요! 모든 문제를 맞혔다니, 수학의 신이 되어 가는 중이에요!",
    "모든 문제를 맞힌 당신, 이제 수학에서 무적이에요! 대단해요!",
    "전부 다 맞혔어요! 이제 수학을 지배하는 당신이 최고예요!",
    "모든 문제를 정확히 맞혔어요! 이제 수학을 완전히 마스터한 거예요!",
    "모든 문제를 다 맞췄다니, 진짜 수학의 왕이네요! 대단해요!"
};
    List<string> Accuracy4 = new List<string>
{
    "대부분의 문제를 맞췄어요! 정말 잘했어요, 이제 몇 문제만 더 풀면 완벽할 거예요!",
    "와, 많은 문제를 맞혔네요! 거의 다 맞혔으니 다음엔 완벽하게 맞힐 수 있을 거예요!",
    "대단해요! 대부분의 문제를 맞혔어요! 조금만 더 하면 100% 성공이죠!",
    "거의 모든 문제를 맞혔어요! 수학 실력이 정말 뛰어나요, 조금만 더 노력해봐요!",
    "대부분 맞혔어요! 멋지네요! 이제 몇 개만 더 맞히면 완벽하게 마스터할 수 있어요!",
    "정말 잘했어요! 많은 문제를 정확히 풀었어요, 이제 마지막 몇 개만 더 도전해보세요!",
    "대부분 맞히다니, 정말 멋져요! 이제 조금만 더 힘내면 모든 문제를 맞힐 수 있어요!",
    "대부분 문제를 맞혔네요! 다음번에는 모든 문제를 완벽하게 해결할 수 있을 거예요!",
    "정말 대단해요! 대부분 맞혔으니, 이제 조금만 더 집중하면 완벽해질 거예요!",
    "거의 다 맞혔어요! 훌륭해요! 이제 마지막 단계만 더 넘으면 수학을 완전히 정복할 거예요!",
    "대부분 맞혔다는 건 이미 훌륭한 실력이에요! 이제 마무리만 잘 하면 완벽하게 해결할 수 있어요!",
    "많은 문제를 정확히 맞혔어요! 이제 조금만 더 풀어보면 수학 마스터가 될 거예요!",
    "대부분 맞힌 거 정말 멋져요! 이제 마지막 한두 개만 더 맞히면 완벽해요!",
    "대부분의 문제를 맞혔어요! 이제 수학에서 최고가 될 준비가 되어가고 있어요!",
    "대부분을 맞혔다니, 이미 훌륭한 실력을 가진 거예요! 조금만 더 힘내면 완벽한 결과가 있을 거예요!",
    "대부분의 문제를 맞혔어요! 조금만 더 집중하면 다음엔 모든 문제를 맞힐 수 있을 거예요!",
    "대부분 맞혔네요! 정말 잘했어요! 이제 몇 문제만 더 풀어보면 완벽하게 수학을 정복할 수 있어요!",
    "정말 멋져요! 대부분 문제를 맞혔어요! 이제 마지막 한 걸음만 더 뗀다면 완벽한 결과가 기다리고 있어요!",
    "대부분 맞혔다니 정말 잘했어요! 이제 자신감을 가지고 마지막 몇 개의 문제도 풀어보세요!",
    "대부분 문제를 맞혔어요! 정말 멋져요! 조금만 더 집중하면 완벽한 수학 마스터가 될 수 있을 거예요!"
};
    List<string> Accuracy3 = new List<string>
{
    "잘했어요! 어느 정도 맞혔어요! 이제 조금만 더 집중하면 더 많은 문제를 맞힐 수 있을 거예요!",
    "어느 정도 맞혔네요! 이제 자신감을 가지고 나머지 문제들도 해결해볼 수 있어요!",
    "훌륭해요! 어느 정도 맞혔어요! 조금만 더 노력하면 완벽하게 맞힐 수 있어요!",
    "많은 문제를 맞혔어요! 이제 남은 문제들만 풀면 완벽하게 정답을 맞출 수 있을 거예요!",
    "잘 하고 있어요! 어느 정도 맞혔으니, 이제 남은 문제들은 확실히 해결할 수 있을 거예요!",
    "어느 정도 맞혔어요! 이미 좋은 시작을 했어요. 남은 문제는 쉽게 풀 수 있을 거예요!",
    "정말 잘했어요! 어느 정도 맞혔으니 이제 조금만 더 집중하면 전부 맞힐 수 있을 거예요!",
    "어느 정도 맞혔다니 정말 멋져요! 이제 조금만 더 노력해서 완벽하게 맞혀봐요!",
    "잘 하고 있어요! 어느 정도 맞혔으니까, 남은 문제들만 해결하면 완벽한 결과가 있을 거예요!",
    "어느 정도 맞혔네요! 이미 좋은 성과를 거두었어요. 이제 마지막 몇 개만 더 풀어보세요!"
};
    List<string> Accuracy2 = new List<string>
{
    "조금 맞혔어요! 잘 하고 있어요! 조금만 더 집중하면 더 많은 문제를 풀 수 있을 거예요!",
    "정확한 답은 아니지만 잘 시도했어요! 조금만 더 노력하면 완벽한 정답이 나올 거예요!",
    "조금 맞혔어요! 아주 좋은 시작이에요, 이제 더 잘 풀 수 있을 거예요!",
    "좋아요! 조금 맞혔어요! 이제 남은 문제들에 대해 더 생각해보면 정답을 맞힐 수 있을 거예요!",
    "조금 맞혔네요! 이제 문제를 잘 풀 수 있다는 자신감을 얻었으니, 나머지도 잘 해결할 수 있을 거예요!",
    "아주 잘했어요! 조금 맞혔어요! 이제 조금만 더 연습하면 완벽하게 맞힐 수 있을 거예요!",
    "잘 했어요! 조금 맞혔으니 이제 다음 단계로 나아갈 준비가 된 거예요!",
    "조금 맞혔어요! 이제 문제의 패턴을 알았으니, 더 많이 맞힐 수 있을 거예요!",
    "정확히 맞히진 않았지만, 첫걸음은 잘 뗐어요! 이제 남은 문제도 잘 풀어볼 수 있을 거예요!",
    "조금 맞혔어요! 아주 훌륭해요! 조금만 더 연습하면 다음에는 전부 맞힐 수 있을 거예요!"
};
    List<string> Accuracy0 = new List<string>
{
    "모두 틀렸지만, 괜찮아요! 실수는 배움의 중요한 부분이에요. 이번 기회에 더 많이 배울 수 있을 거예요!",
    "아직 틀렸지만, 중요한 건 계속 도전하는 거예요. 조금만 더 노력하면 다음엔 더 잘할 수 있어요!",
    "이번엔 모두 틀렸지만, 실수를 통해 배우는 게 진짜 중요해요. 계속해서 연습해봐요!",
    "모든 문제를 틀렸지만, 그만큼 배울 기회가 생긴 거예요. 다음엔 더 잘할 수 있을 거예요!",
    "이번엔 모두 틀렸지만, 괜찮아요! 실수에서 배우는 것이 더 중요한 걸요. 다시 시도해 봅시다!",
    "모두 틀렸지만, 이번 경험이 앞으로 더 잘할 수 있는 발판이 될 거예요. 포기하지 말고 다시 도전해봐요!",
    "모두 틀렸다고 해도, 실수에서 얻는 것이 더 많아요. 계속 도전하면 분명히 더 잘할 수 있을 거예요!",
    "이번엔 틀렸지만, 중요한 건 끝까지 포기하지 않는 거예요. 계속해서 시도하고 배우는 게 가장 중요해요!",
    "모두 틀렸지만, 실패도 중요한 학습이에요. 다시 시도하면 확실히 나아질 거예요!",
    "모두 틀렸지만, 실수는 발전의 시작이에요. 다음에는 더 잘할 수 있도록 같이 힘내봐요!"
};



    public string GetRandomCombo(int comboType)
    {
        switch (comboType)
        {
            case 5:
                return Combo5[Random.Range(0, Combo5.Count)];
            case 10:
                return Combo10[Random.Range(0, Combo10.Count)];
            case 15:
                return Combo15[Random.Range(0, Combo15.Count)];
            default:
                return "Invalid combo type";
        }
    }

    string GetRandomAccuracyMessage(int accuracyLevel)
    {
        switch (accuracyLevel)
        {
            case 5:
                return Accuracy5[Random.Range(0, Accuracy5.Count)];
            case 4:
                return Accuracy4[Random.Range(0, Accuracy4.Count)];
            case 3:
                return Accuracy3[Random.Range(0, Accuracy3.Count)];
            case 2:
                return Accuracy2[Random.Range(0, Accuracy2.Count)];
            case 0:
                return Accuracy0[Random.Range(0, Accuracy0.Count)];
            default:
                return "잘못된 정확도 수준입니다.";
        }
    }

    public  string GetAccuracyMessage(int totalQuestions, int incorrectQuestions)
    {
        // 정확도 계산
        float accuracy = (totalQuestions - incorrectQuestions) / (float)totalQuestions * 100;

        // 정확도 수준 계산
        int accuracyLevel = 0;

        if (accuracy == 100)
        {
            accuracyLevel = 5;  // 다 맞은 경우
        }
        else if (accuracy >= 80)
        {
            accuracyLevel = 4;  // 80% 이상 맞은 경우
        }
        else if (accuracy >= 60)
        {
            accuracyLevel = 3;  // 60% 이상 맞은 경우
        }
        else if (accuracy >= 40)
        {
            accuracyLevel = 2;  // 40% 이상 맞은 경우
        }
        else
        {
            accuracyLevel = 0;  // 다 틀린 경우
        }

        // 정확도 메시지 반환
        return GetRandomAccuracyMessage(accuracyLevel);
    }

}
