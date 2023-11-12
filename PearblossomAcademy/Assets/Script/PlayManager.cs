using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    //player 체력 설정
    public int playerLife = 3;
    public int skillCount = 3;
    //monster 체력 설정
    /*
    public int monster1HP = 300;
    public int monster2HP = 500;
    public int monster3HP = 1000;
    public int monster4HP = 1500;
    public int monster5HP = 3000;
    */

    //테스트용 체력설정
    public int monster1HP = 20;
    public int monster2HP = 20;
    public int monster3HP = 20;
    public int monster4HP = 20;
    public int monster5HP = 20;

    public int monsterAttack = 20;

    public int playerBasicAttack = 10; //player 기본 공격 데미지
    public int playerJujakAttack = 40;
    public int playerHyunmuAttack = 80;

    public bool isMultipleBoss = false;

    //public int thisMapIndex = 5;

    private Monster5 mixedMonster;
    public bool isStartAttacking = true;
    public GameObject[] UltimateCircles;
    GameManager myGameManager;

    void Awake()
    {
        bool[] usableSkill = new bool[] {true, false, false, false};
        Player myPlayer = GameObject.Find("Player").GetComponent<Player>();
        myGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if(isMultipleBoss){mixedMonster = GameObject.Find("Monster5").GetComponent<Monster5>();}
    }

    public void GameOver()
    {
        //Time.timeScale = 0;
        myGameManager.GameOver();
    }

    public void MonsterClear(int clearedMonsterIndex)
    {
        if(!isMultipleBoss)
        {
            Time.timeScale = 0;
            GameClear();
        }

        else
        {
            if(clearedMonsterIndex >= 4) //막보스까지 다 깸?
            {
                GameClearFinal();
            }
            else
            {
                mixedMonster.MoveOnToNextMonster(clearedMonsterIndex);
            }   
        }
        
    }

    public void GameClear()
    {
        myGameManager.GameClear();
    }

    void GameClearFinal()
    {

    }

    public void UltSkillActivate()
    {
        Destroy(UltimateCircles[3-skillCount]);
    }

}
