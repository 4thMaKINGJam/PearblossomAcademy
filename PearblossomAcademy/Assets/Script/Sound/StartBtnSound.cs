using System;
using UnityEngine;
using UnityEngine.UI; // UI 컴포넌트를 사용하기 위해 추가

public class StartBtnSound : MonoBehaviour
{
    public AudioClip audioStart;

    AudioSource audioSource;

    void PlaySound(String action){
        audioSource.clip = audioStart;
        audioSource.Play();
    }
    
    void Awake(){
        audioSource = GetComponent<AudioSource>();
    }
    
    
}
