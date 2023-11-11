using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    //player 체력 설정
    public int playerLife = 3;
    //monster 체력 설정
    public int monster1HP = 300;
    public int monster2HP = 500;
    public int monster3HP = 1000;
    public int monster4HP = 1500;
    public int monster5HP = 3000;
    public int monsterAttack = 20;

    public int playerBasicAttack = 10; //player 기본 공격 데미지
    //임의로 설정
    public int player2Attack = 20;
    //attack 2~ damage 설정 필요

    

    void Start()
    {
        
        bool[] usableSkill = new bool[] {true, false, false, false};

        Player myPlayer = GameObject.Find("Player").GetComponent<Player>();
        /*
        if(usableSkill[0]) {GameObject myBlueDragon = Instantiate(myPlayer.BlueDragon, myPlayer.transform);}
        if(usableSkill[1]) {GameObject myJujak = Instantiate(myPlayer.Jujak, myPlayer.transform);}
        if(usableSkill[2]) {GameObject myWhiteTiger = Instantiate(myPlayer.WhiteTiger, myPlayer.transform);}
        if(usableSkill[3]) {GameObject myHyunmu = Instantiate(myPlayer.Hyunmu, myPlayer.transform);}
        */
    }

}
