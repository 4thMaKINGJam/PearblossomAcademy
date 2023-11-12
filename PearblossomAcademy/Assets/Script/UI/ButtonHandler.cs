using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ButtonHandler : MonoBehaviour
{
    public GameObject[] main_book;
    public GameObject activeObject;
    public GameObject handRight;
    public GameObject handLeft;
    public int currentIndex = 0;
    private int unlock_stage;

    //사운드
    public AudioClip audioStart;
    public AudioClip audioFlip;

    AudioSource audioSource;


    GameManager gameManager;
    StartBtnSound startBtnSound;


    public void PlaySound(String action){
        switch(action){
            case "BtnStart":
                audioSource.clip = audioStart;
                break;
            case "BtnFlip":
                audioSource.clip = audioFlip;
                break;
        } 
        audioSource.Play();
    }

    void Awake(){
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
        // startBtnSound = GameObject.Find("StartBtnSound").GetComponent<StartBtnSound>();
    }


    void Start()
    {
        //unlock_stage = gameManager.stage_count;
        unlock_stage = 6;
        Debug.Log(unlock_stage);

        for(int i=1; i<6; i++)
        {
            main_book[i].SetActive(false);
        }
        activeObject.SetActive(true);
        handRight.SetActive(false);
        handLeft.SetActive(false);

        Button buttonRight = GameObject.Find("btn_right").GetComponent<Button>();
        Button buttonLeft = GameObject.Find("btn_left").GetComponent<Button>();

        Button button_start = GameObject.Find("stage_start").GetComponent<Button>();

        // buttonRight.onClick.AddListener(() => FlipRight());
        // buttonLeft.onClick.AddListener(() => FlipLeft());

        buttonRight.onClick.AddListener(() => {
            PlaySound("BtnFlip");
            FlipRight();
        });
        buttonLeft.onClick.AddListener(() => {
            PlaySound("BtnFlip");
            FlipLeft();
        });
        
        // 버튼 클릭 시 사운드 재생과 씬 활성화를 함께 추가
        button_start.onClick.AddListener(() => 
        {
            PlaySound("BtnStart");
            ActivateScene();
        });
    }


    void ActivateObject(GameObject targetObject)
    {
        foreach (GameObject obj in main_book)
        {
            obj.SetActive(obj == targetObject);
        }
    }

    void FlipRight()
    {
        currentIndex = (currentIndex + 1) % unlock_stage;

        handRight.SetActive(true);
        Invoke("HandActivate",0.2f);

        if(currentIndex <= unlock_stage){
            ActivateObject(main_book[currentIndex]);
            activeObject = main_book[currentIndex];
        }
        
    }

    void FlipLeft()
    {
        currentIndex = (currentIndex - 1 + unlock_stage) % unlock_stage;
        handLeft.SetActive(true);
        Invoke("HandActivate",0.2f);
        if(currentIndex <= unlock_stage){
            ActivateObject(main_book[currentIndex]);
            activeObject = main_book[currentIndex];
        }
    }
    void HandActivate()
    {
        handRight.SetActive(false);
        handLeft.SetActive(false);
    }
    void ActivateScene()
{
    if (activeObject == main_book[0])  // main_intro와 비교
    {
        SceneManager.LoadScene("Main");
        DontDestroyOnLoad(gameManager);
    }
    else if (activeObject == main_book[1])  // main_bdrgn와 비교
    {
        SceneManager.LoadScene("BlueDragon");
        DontDestroyOnLoad(gameManager);
    }
    else if (activeObject == main_book[2])  // main_jj와 비교
    {
        SceneManager.LoadScene("Jujak");
        DontDestroyOnLoad(gameManager);
    }
    else if (activeObject == main_book[3])  // main_whitetg와 비교
    {
        SceneManager.LoadScene("WhiteTiger");
        DontDestroyOnLoad(gameManager);
    }
    else if (activeObject == main_book[4])  // main_hm와 비교
    {
        SceneManager.LoadScene("Hyunmu");
        DontDestroyOnLoad(gameManager);
    }
    else if (activeObject == main_book[5])  // main_ydrgn와 비교
    {
        SceneManager.LoadScene("YellowDragon");
        DontDestroyOnLoad(gameManager);
    }
}

}

