using UnityEngine;
using UnityEngine.UI; // UI 컴포넌트를 사용하기 위해 추가

public class StartBtnSound : MonoBehaviour
{
    AudioSource audioSource;
    
    void Awake(){
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        // 버튼 컴포넌트에 접근하고, 클릭 이벤트에 함수를 연결합니다.
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(PlayAudio);
    }

    void PlayAudio(){
        // audioSource.clip = audioStart;
        audioSource.Play();
    }

    
}
