using System;
using UnityEngine;
using UnityEngine.UI; // UI 컴포넌트를 사용하기 위해 추가

public class StartBtnSound : MonoBehaviour
{
    public AudioClip audioStart;

    AudioSource audioSource;

    void PlaySound(){
        audioSource.clip = audioStart;
        audioSource.Play();
    }
    
    void Awake(){
        audioSource = GetComponent<AudioSource>();
    }

    //onclick이 일어나면 playsound 함수콜 되게 변경
    
    
}
