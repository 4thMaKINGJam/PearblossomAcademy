using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool[] usableSkill = new bool[4];
    public bool isGameover = false;
    public int GameClear=0;

    //씬 관리
    public GameObject[] Stages;

    //스테이지 종료 UI
    public Image[] gameImage; //gameImage[0]: gameOver / gameImage[1]:gameClear
    public Image[] blackScreen;

    //게임오버함수 - 다른 player script에서 OnPlayerDead 함수 호출해줘야 돼
    public void GameOver() {
        //시간멈춤
        Time.timeScale = 0;

        isGameover = true;

        //BlackScreen 활성화
        blackScreen[0].gameObject.SetActive(true);
        
        //GameOverImage 활성화
        gameImage[0].gameObject.SetActive(true);
    }

    //게임Clear함수
    public void GameClear() {
        //시간멈춤
        Time.timeScale = 0;

        isGameover = true;

        //BlackScreen 활성화
        blackScreen[0].gameObject.SetActive(true);
        
        //GameOverImage 활성화
        gameImage[1].gameObject.SetActive(true);
    }

}
