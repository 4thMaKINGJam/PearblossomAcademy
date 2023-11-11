using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    public int playerLife;
    public int monsterHP;

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
