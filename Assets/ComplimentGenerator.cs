using System.Collections.Generic;
using UnityEngine;

public class ComplimentGenerator : MonoBehaviour
{
    List<string> Combo5 = new List<string>
    {
    "��, 5������ �������� �� �������! ����ؿ�, ���� õ��!",
    "5���� ���� ����! ���� ���� �����Ͱ� �� �� ���ƿ�!",
    "����! 5���� �������� ������! �Ϻ��ؿ�!",
    "��Ȯ�� �������! 5������ �������� �ذ��ϴٴ�, ������!",
    "���� ���� ���! 5������ �������� ����׿�. ��� �̷��� �غ���!",
    "��������! 5���� ���� ����! ���� ������ ���̳׿�!",
    "�ʹ� �����׿�! 5���� �������� ���ߴٴ�, ���� ������ ����� ���� �� ���ھ��!",
    "5������ �������� �� �������! ������ ����� Ǯ����ȳ׿�!",
    "�Ϳ�, 5���� ���� ����! ��� �̴�� ���� ���� ������!",
    "������ ���� ���Ծ��! 5������ �������� �����ٴ�, ����ؿ�!"
    };
    List<string> Combo10 = new List<string>
{
    "10���� ���� ����! ���� ������ ������ ������ �Ǽ̾��!",
    "��, 10���� ���� ����! ����� ���� ���� �����͸� �Ѿ ������ è�Ǿ�!",
    "��ӳ�, 10������ �������� �� ����ٴ�! ������ ���� �Ǿ� ���� ���̿���!",
    "����ؿ�! 10������ ���� ����! ���� ���п��� �η��� �� ���ھ��!",
    "10���� ���� ����! �̰� �ٷ� ���� õ���� ��!",
    "���! 10���� �������� �� �����ٴ�, ���� ������! ���� ������ �����ϴ� ����� �Ǿ����!",
    "10������ �������� �����ٴ�, �Ϻ��ϰ� ������ �����߳׿�! �ְ�����!",
    "��Ȯ�ϰ� 10������ �������! ������ ���� �� �������?",
    "10������ �������� ���� ���, ���� ������ ����� ��� �� �� ���ƿ�!",
    "10���� ���� ����! ����� ���� �Ƿ��� ���� �����̿���!"
};
    List<string> Combo15 = new List<string>
{
    "15���� ���� ����! ���� ������ ���� ������ ����߾��!",
    "�Ϳ�! 15���� ���� �����̶��! ����� ������ �����̿���!",
    "��� �̷��� �� �ϳ���? 15������ �������� �� �����ٴ�, ������ ������׿�!",
    "���� ����ؿ�! 15���� �������� �����ٴ�, ���� ������ ������ �ŵ쳵���!",
    "15���� ���� ����! �� ������ ������ ������ ������ �ų� �ٸ������!",
    "����ؿ�! 15���� �������� �������! ����̾߸��� ������ ������ õ��!",
    "15������ �������� �����ٴ�, ���� ������ ������ �ƴϰ�, ������ ���̿���!",
    "15���� ���� ����! ������ ������ ����� ���, ���� ������!",
    "��Ȯ�ϰ� 15������ �������! ���� ���п� �ڽŰ��� ���� ���� �غ� �Ϸ�!",
    "15���� ���� ����! ����� ���� ������ è�Ǿ����� ������ ����!",
    "15���� �������� �� �������! ���� �Ƿ��� ��� �̻��̿���!",
    "����ؿ�! 15������ �������� ����ٴ� �� ��¥ ���� ���ƿ�!",
    "15���� ���� ����! ���� ������ ���� ���� ���� �� �ְھ��?",
    "���� ������! 15���� ���� ���ߴٴ�, ���п��� �ְ��ڰ� �� �� ���ƿ�!",
    "15������ �������� �����ٴ�, ���� ������ �����ϴ� ����� �Ǿ����! �ְ�!"
};


    List<string> Accuracy5 = new List<string>
{
    "��, ��� ������ �������! �̰� ���� ����� ���뿹��!",
    "��� ������ �Ϻ��ϰ� ���ߴٴ�, ���� ������ �����Ͱ� �Ǿ����!",
    "���� �����ٴ�! ���� ������ ������ ������ ����!",
    "��� ������ �� �������! �� ������ ������ ���̶� �ҷ��� �ջ������!",
    "�Ϻ��ϰ� ��� ������ �������! ������ è�Ǿ�, �ٷ� ����̿���!",
    "��� ������ ��Ȯ�� �������! ������ ��� ����� Ǯ����� �� ���ƿ�!",
    "��� ������ �� ���� ���, ���� ������! ���� ������ ������ �����ϴ� ���!",
    "�Ϳ�, ��� ������ �����ٴ�! ���� ���п����� ����� �ְ�����!",
    "��� ������ ��Ȯ�� Ǯ�����! �̰� ���� ����� ���뿹��!",
    "���� ����ٴ�, ���� ���п��� �� �̻� ������ �� ���ھ��! �ְ�����!",
    "��� ������ �����ٴ�, ������ ������ õ��! ���� ������!",
    "��� ������ �Ϻ��ϰ� �ذ��߾��! ����� ���� ������ ������ Ȯ���ؿ�!",
    "��� ������ �� �����ٴ� ��, ���� ������ �����ߴٴ� ���̿���! ���� �ְ�!",
    "�Ϻ��ϰ� ��� ������ �������! ���п��� ���� ���� ���� ���ڳ׿�!",
    "��� ������ ��Ȯ�� �����ٴ�! �� ���� �Ƿ��� ���� ����ؿ�!",
    "���� ������! ��� ������ �����ٴ�, ������ ���� �Ǿ� ���� ���̿���!",
    "��� ������ ���� ���, ���� ���п��� �����̿���! ����ؿ�!",
    "���� �� �������! ���� ������ �����ϴ� ����� �ְ�����!",
    "��� ������ ��Ȯ�� �������! ���� ������ ������ �������� �ſ���!",
    "��� ������ �� ����ٴ�, ��¥ ������ ���̳׿�! ����ؿ�!"
};
    List<string> Accuracy4 = new List<string>
{
    "��κ��� ������ ������! ���� ���߾��, ���� �� ������ �� Ǯ�� �Ϻ��� �ſ���!",
    "��, ���� ������ �����׿�! ���� �� �������� ������ �Ϻ��ϰ� ���� �� ���� �ſ���!",
    "����ؿ�! ��κ��� ������ �������! ���ݸ� �� �ϸ� 100% ��������!",
    "���� ��� ������ �������! ���� �Ƿ��� ���� �پ��, ���ݸ� �� ����غ���!",
    "��κ� �������! �����׿�! ���� �� ���� �� ������ �Ϻ��ϰ� �������� �� �־��!",
    "���� ���߾��! ���� ������ ��Ȯ�� Ǯ�����, ���� ������ �� ���� �� �����غ�����!",
    "��κ� �����ٴ�, ���� ������! ���� ���ݸ� �� ������ ��� ������ ���� �� �־��!",
    "��κ� ������ �����׿�! ���������� ��� ������ �Ϻ��ϰ� �ذ��� �� ���� �ſ���!",
    "���� ����ؿ�! ��κ� ��������, ���� ���ݸ� �� �����ϸ� �Ϻ����� �ſ���!",
    "���� �� �������! �Ǹ��ؿ�! ���� ������ �ܰ踸 �� ������ ������ ������ ������ �ſ���!",
    "��κ� �����ٴ� �� �̹� �Ǹ��� �Ƿ��̿���! ���� �������� �� �ϸ� �Ϻ��ϰ� �ذ��� �� �־��!",
    "���� ������ ��Ȯ�� �������! ���� ���ݸ� �� Ǯ��� ���� �����Ͱ� �� �ſ���!",
    "��κ� ���� �� ���� ������! ���� ������ �ѵ� ���� �� ������ �Ϻ��ؿ�!",
    "��κ��� ������ �������! ���� ���п��� �ְ��� �� �غ� �Ǿ�� �־��!",
    "��κ��� �����ٴ�, �̹� �Ǹ��� �Ƿ��� ���� �ſ���! ���ݸ� �� ������ �Ϻ��� ����� ���� �ſ���!",
    "��κ��� ������ �������! ���ݸ� �� �����ϸ� ������ ��� ������ ���� �� ���� �ſ���!",
    "��κ� �����׿�! ���� ���߾��! ���� �� ������ �� Ǯ��� �Ϻ��ϰ� ������ ������ �� �־��!",
    "���� ������! ��κ� ������ �������! ���� ������ �� ������ �� ���ٸ� �Ϻ��� ����� ��ٸ��� �־��!",
    "��κ� �����ٴ� ���� ���߾��! ���� �ڽŰ��� ������ ������ �� ���� ������ Ǯ�����!",
    "��κ� ������ �������! ���� ������! ���ݸ� �� �����ϸ� �Ϻ��� ���� �����Ͱ� �� �� ���� �ſ���!"
};
    List<string> Accuracy3 = new List<string>
{
    "���߾��! ��� ���� �������! ���� ���ݸ� �� �����ϸ� �� ���� ������ ���� �� ���� �ſ���!",
    "��� ���� �����׿�! ���� �ڽŰ��� ������ ������ �����鵵 �ذ��غ� �� �־��!",
    "�Ǹ��ؿ�! ��� ���� �������! ���ݸ� �� ����ϸ� �Ϻ��ϰ� ���� �� �־��!",
    "���� ������ �������! ���� ���� �����鸸 Ǯ�� �Ϻ��ϰ� ������ ���� �� ���� �ſ���!",
    "�� �ϰ� �־��! ��� ���� ��������, ���� ���� �������� Ȯ���� �ذ��� �� ���� �ſ���!",
    "��� ���� �������! �̹� ���� ������ �߾��. ���� ������ ���� Ǯ �� ���� �ſ���!",
    "���� ���߾��! ��� ���� �������� ���� ���ݸ� �� �����ϸ� ���� ���� �� ���� �ſ���!",
    "��� ���� �����ٴ� ���� ������! ���� ���ݸ� �� ����ؼ� �Ϻ��ϰ� ��������!",
    "�� �ϰ� �־��! ��� ���� �������ϱ�, ���� �����鸸 �ذ��ϸ� �Ϻ��� ����� ���� �ſ���!",
    "��� ���� �����׿�! �̹� ���� ������ �ŵξ����. ���� ������ �� ���� �� Ǯ�����!"
};
    List<string> Accuracy2 = new List<string>
{
    "���� �������! �� �ϰ� �־��! ���ݸ� �� �����ϸ� �� ���� ������ Ǯ �� ���� �ſ���!",
    "��Ȯ�� ���� �ƴ����� �� �õ��߾��! ���ݸ� �� ����ϸ� �Ϻ��� ������ ���� �ſ���!",
    "���� �������! ���� ���� �����̿���, ���� �� �� Ǯ �� ���� �ſ���!",
    "���ƿ�! ���� �������! ���� ���� �����鿡 ���� �� �����غ��� ������ ���� �� ���� �ſ���!",
    "���� �����׿�! ���� ������ �� Ǯ �� �ִٴ� �ڽŰ��� �������, �������� �� �ذ��� �� ���� �ſ���!",
    "���� ���߾��! ���� �������! ���� ���ݸ� �� �����ϸ� �Ϻ��ϰ� ���� �� ���� �ſ���!",
    "�� �߾��! ���� �������� ���� ���� �ܰ�� ���ư� �غ� �� �ſ���!",
    "���� �������! ���� ������ ������ �˾�����, �� ���� ���� �� ���� �ſ���!",
    "��Ȯ�� ������ �ʾ�����, ù������ �� �þ��! ���� ���� ������ �� Ǯ� �� ���� �ſ���!",
    "���� �������! ���� �Ǹ��ؿ�! ���ݸ� �� �����ϸ� �������� ���� ���� �� ���� �ſ���!"
};
    List<string> Accuracy0 = new List<string>
{
    "��� Ʋ������, �����ƿ�! �Ǽ��� ����� �߿��� �κ��̿���. �̹� ��ȸ�� �� ���� ��� �� ���� �ſ���!",
    "���� Ʋ������, �߿��� �� ��� �����ϴ� �ſ���. ���ݸ� �� ����ϸ� ������ �� ���� �� �־��!",
    "�̹��� ��� Ʋ������, �Ǽ��� ���� ���� �� ��¥ �߿��ؿ�. ����ؼ� �����غ���!",
    "��� ������ Ʋ������, �׸�ŭ ��� ��ȸ�� ���� �ſ���. ������ �� ���� �� ���� �ſ���!",
    "�̹��� ��� Ʋ������, �����ƿ�! �Ǽ����� ���� ���� �� �߿��� �ɿ�. �ٽ� �õ��� ���ô�!",
    "��� Ʋ������, �̹� ������ ������ �� ���� �� �ִ� ������ �� �ſ���. �������� ���� �ٽ� �����غ���!",
    "��� Ʋ�ȴٰ� �ص�, �Ǽ����� ��� ���� �� ���ƿ�. ��� �����ϸ� �и��� �� ���� �� ���� �ſ���!",
    "�̹��� Ʋ������, �߿��� �� ������ �������� �ʴ� �ſ���. ����ؼ� �õ��ϰ� ���� �� ���� �߿��ؿ�!",
    "��� Ʋ������, ���е� �߿��� �н��̿���. �ٽ� �õ��ϸ� Ȯ���� ������ �ſ���!",
    "��� Ʋ������, �Ǽ��� ������ �����̿���. �������� �� ���� �� �ֵ��� ���� ��������!"
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

    public string GetRandomAccuracyMessage(int accuracyLevel)
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
                return "�߸��� ��Ȯ�� �����Դϴ�.";
        }
    }


}