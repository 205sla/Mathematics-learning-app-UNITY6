using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class SentenceMove : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.DOMoveY(transform.position.y + 100, 1f).SetDelay(0.5f); // y좌표 이동


        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        if (text != null)
        {
            text.DOFade(1f, 1f).SetDelay(0.5f).OnComplete(() => GameManager.instance.LogoShowCompleted());  // 텍스트 알파값 변경
        }
        else
        {
            Debug.LogError("TextMeshProUGUI 컴포넌트가 이 오브젝트에 없습니다.");
            Debug.LogError("로고 보여주기 오류");
            PlayerPrefs.SetInt("errorCode", 30);
            SceneManager.LoadScene("Error");
        }
    }

    // Update is called once per frame
    
}
