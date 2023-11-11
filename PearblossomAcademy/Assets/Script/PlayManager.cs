using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    public int playerLife;
    //moster 체력 설정
    public int monster1HP = 300;
    public int monster2HP = 500;
    public int monster3HP = 1000;
    public int monster4HP = 1500;
    public int monster5HP = 3000;
    public int monsterFoxCircle = 20;

    public int playerBasicAttack = 10; //player 기본 공격 데미지
    //attack 2~ damage 설정 필요
    
    

    void Start()
    {
        bool[] usableSkill = new bool[] {true, false, false, false};

        Player myPlayer = GameObject.Find("Player").GetComponent<Player>();

        if(usableSkill[0]) {GameObject myBlueDragon = Instantiate(myPlayer.BlueDragon);}
        if(usableSkill[1]) {GameObject myJujak = Instantiate(myPlayer.Jujak);}
        if(usableSkill[2]) {GameObject myWhiteTiger = Instantiate(myPlayer.WhiteTiger);}
        if(usableSkill[3]) {GameObject myHyunmu = Instantiate(myPlayer.Hyunmu);}
        
    }

}
