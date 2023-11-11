using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool[] usableSkill = new bool[4];
    public bool isGameover = false;

    public int stage_count;

    //씬 관리
    public GameObject[] Stages;

    //스테이지 종료 UI
    public GameObject[] gameImage; //gameImage[0]: gameOver / gameImage[1]:gameClear
    public Image[] blackScreen;

    void Awake()
    {
        stage_count = 2;

        if (instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
            return;
        }
        gameImage[0].SetActive(false);
        gameImage[1].SetActive(false);

        blackScreen[0].enabled = false;

        DontDestroyOnLoad(gameObject);
    }


    //게임오버함수 - 다른 player script에서 OnPlayerDead 함수 호출해줘야 돼
    public void GameOver() {
        //시간멈춤
        Time.timeScale = 0;
        isGameover = true;

        blackScreen[0].gameObject.SetActive(true);
        gameImage[0].gameObject.SetActive(true);

        Button gotoMenu2 = GameObject.Find("BackToMenuBtn_o").GetComponent<Button>();
        Button retry = GameObject.Find("retry").GetComponent<Button>();

        gotoMenu2.onClick.AddListener(() => GoMenu());
        retry.onClick.AddListener(() => reTry());


    }

    //게임Clear함수
    public void GameClear() {
        stage_count ++;
        //시간멈춤
        Time.timeScale = 0;

        isGameover = true;

        //BlackScreen 활성화
        blackScreen[0].gameObject.SetActive(true);
        //GameOverImage 활성화
        gameImage[1].gameObject.SetActive(true);

        Button gotoMenu = GameObject.Find("BackToMenuBtn_c").GetComponent<Button>();
        Button gotoNextStage = GameObject.Find("nextStage").GetComponent<Button>();

        gotoMenu.onClick.AddListener(() => GoMenu());
        gotoNextStage.onClick.AddListener(() => nextStage());
    }

    void GoMenu() {
        SceneManager.LoadScene("Start");
    }
    void nextStage() {
        Debug.Log("next_stage");
    }
    void reTry() {
        Time.timeScale=1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
