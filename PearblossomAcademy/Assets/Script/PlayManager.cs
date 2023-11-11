using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    //player 체력 설정
    public int playerLife = 3;
    public int skillCount = 3;
    //monster 체력 설정
    public int monster1HP = 300;
    public int monster2HP = 500;
    public int monster3HP = 1000;
    public int monster4HP = 1500;
    public int monster5HP = 3000;
    public int monsterAttack = 20;

    public int playerBasicAttack = 10; //player 기본 공격 데미지
    public int playerJujakAttack = 40;
    public int playerHyunmuAttack = 80;

    public GameObject[] UltimateCircles;

    void Start()
    {
        bool[] usableSkill = new bool[] {true, false, false, false};
        Player myPlayer = GameObject.Find("Player").GetComponent<Player>();
    }

    public void GameOver()
    {
        Time.timeScale = 0;
    }

    public void GameClear()
    {
        Time.timeScale = 0;
    }

    public void UltSkillActivate()
    {
        Destroy(UltimateCircles[3-skillCount]);
    }

}
