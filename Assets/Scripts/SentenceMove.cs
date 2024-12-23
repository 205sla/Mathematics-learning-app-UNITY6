using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class SentenceMove : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.DOMoveY(transform.position.y + 100, 1f).SetDelay(0.5f); // y��ǥ �̵�


        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        if (text != null)
        {
            text.DOFade(1f, 1f).SetDelay(0.5f).OnComplete(() => GameManager.instance.LogoShowCompleted());  // �ؽ�Ʈ ���İ� ����
        }
        else
        {
            Debug.LogError("TextMeshProUGUI ������Ʈ�� �� ������Ʈ�� �����ϴ�.");
            Debug.LogError("�ΰ� �����ֱ� ����");
            PlayerPrefs.SetInt("errorCode", 30);
            SceneManager.LoadScene("Error");
        }
    }

    // Update is called once per frame
    
}
