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
        /*
        if(usableSkill[0]) {GameObject myBlueDragon = Instantiate(myPlayer.BlueDragon, myPlayer.transform);}
        if(usableSkill[1]) {GameObject myJujak = Instantiate(myPlayer.Jujak, myPlayer.transform);}
        if(usableSkill[2]) {GameObject myWhiteTiger = Instantiate(myPlayer.WhiteTiger, myPlayer.transform);}
        if(usableSkill[3]) {GameObject myHyunmu = Instantiate(myPlayer.Hyunmu, myPlayer.transform);}
        */
    }

}
