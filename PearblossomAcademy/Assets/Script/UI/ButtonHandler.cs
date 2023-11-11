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

    GameManager gameManager;

    void Awake(){
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    void Start()
    {
        unlock_stage = gameManager.stage_count;
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

        buttonRight.onClick.AddListener(() => FlipRight());
        buttonLeft.onClick.AddListener(() => FlipLeft());

        button_start.onClick.AddListener(() => ActivateScene());
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
        SceneManager.LoadScene("Intro");
    }
    else if (activeObject == main_book[1])  // main_bdrgn와 비교
    {
        SceneManager.LoadScene("BlueDragon");
    }
    else if (activeObject == main_book[2])  // main_jj와 비교
    {
        SceneManager.LoadScene("Jujak");
    }
    else if (activeObject == main_book[3])  // main_whitetg와 비교
    {
        SceneManager.LoadScene("WhiteTiger");
    }
    else if (activeObject == main_book[4])  // main_hm와 비교
    {
        SceneManager.LoadScene("Hyunmu");
    }
    else if (activeObject == main_book[5])  // main_ydrgn와 비교
    {
        SceneManager.LoadScene("YellowDragon");
    }
}

}

