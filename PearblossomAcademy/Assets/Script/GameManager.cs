using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool[] usableSkill = new bool[4];
    public bool isGameover = false;
    public int sceneCount;

    public int stage_count;

    //씬 관리
    public GameObject[] Stages;

    //스테이지 종료 UI
    public GameObject[] gameImage; //gameImage[0]: gameOver / gameImage[1]:gameClear
    public GameObject[] blackScreen;

    void Awake()
    {
        Time.timeScale=1;
        stage_count = 1;

    }


    //게임오버함수 - 다른 player script에서 OnPlayerDead 함수 호출해줘야 돼
    public void GameOver() {
        //시간멈춤
        Time.timeScale = 0;
        isGameover = true;

        /*
        GameObject myBlackScreen = GameObject.Find("BlackScreen");
        myBlackScreen.SetActive(true);
        GameObject myGameOverImage = GameObject.Find("GameOverImage");
        myGameOverImage.SetActive(true);
        */
        GameObject myBlackScreen = Instantiate(blackScreen[0], GameObject.Find("Canvas").transform);
        GameObject myGameOverImage = Instantiate(gameImage[0], GameObject.Find("Canvas").transform);

        Button gotoMenu2 = myGameOverImage.transform.Find("BackToMenuBtn_o").GetComponent<Button>();
        Button retry = myGameOverImage.transform.Find("retry").GetComponent<Button>();

        gotoMenu2.onClick.AddListener(() => GoMenu());
        retry.onClick.AddListener(() => reTry());


    }

    //게임Clear함수
    public void GameClear() {
        

        stage_count++;
        Debug.Log(stage_count);
        //시간멈춤
        Time.timeScale = 0;

        isGameover = true;

        /*
        //BlackScreen 활성화
        blackScreen[0].SetActive(true);
        //GameOverImage 활성화
        gameImage[1].SetActive(true);
        */

        /*
        GameObject myBlackScreen = GameObject.Find("BlackScreen");
        myBlackScreen.SetActive(true);
        GameObject myGameClearIamge = GameObject.Find("GameClearIamge");
        myGameClearIamge.SetActive(true);
        */

        GameObject myBlackScreen = Instantiate(blackScreen[0], GameObject.Find("Canvas").transform);
        GameObject myGameClearImage = Instantiate(gameImage[1], GameObject.Find("Canvas").transform);

        Button gotoMenu = myGameClearImage.transform.Find("BackToMenuBtn_c").GetComponent<Button>();
        Button gotoNextStage = myGameClearImage.transform.Find("nextStage").GetComponent<Button>();

        gotoMenu.onClick.AddListener(() => GoMenu());
        gotoNextStage.onClick.AddListener(() => nextStage());
    }

    void GoMenu() {
        SceneManager.LoadScene("Start");
    }
    void nextStage() {
        sceneCount = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneCount+1);
    }
    void reTry() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnEnable()
    {
          // 씬 매니저의 sceneLoaded에 체인을 건다.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 체인을 걸어서 이 함수는 매 씬마다 호출된다.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
