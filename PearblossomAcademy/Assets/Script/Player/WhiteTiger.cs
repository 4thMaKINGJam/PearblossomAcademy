using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteTiger : MonoBehaviour
{
    public float WhiteTigerDuration; //백호공격 지속 시간
    private float curTime; 

    public GameObject WhiteTigerShield; //백호공격 prefab
    private Player myPlayer;

    void Awake()
    {
        myPlayer = GameObject.Find("Player").GetComponent<Player>();
        curTime = 0;
        WhiteTigerDuration = 10;
    }

    void FixedUpdate()
    {
        ActivateWhiteTiger();
        if(myPlayer.isSkill)
        {
            Countdown();
        }
    }

    void ActivateWhiteTiger()
    {
        if(curTime > WhiteTigerDuration)
        {
            myPlayer.isSkill = false;
            curTime = 0;
        }

        if (Input.GetButton("WhiteTiger") && !myPlayer.isSkill)
        {
            myPlayer.isSkill = true;
            myPlayer.skillIndex = 2; //백호 인덱스
        }
    }

    void Countdown()
    {
        curTime += Time.deltaTime;
    }

    public void GoWhiteTiger()
    {
        Vector3 shieldPos = myPlayer.transform.position;
        GameObject myWhiteTigerShield = Instantiate(WhiteTigerShield, shieldPos, transform.rotation, myPlayer.transform);
    }
}
