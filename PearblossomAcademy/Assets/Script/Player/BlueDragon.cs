using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueDragon : MonoBehaviour
{
    public float blueDragonDuration; //청룡공격 지속 시간
    public float blueDragonDelay; //청룡공격 공격 간격
    private float curTime; 

    public GameObject blueDragonAttack; //청룡공격 prefab
    private Player myPlayer;


    void Awake()
    {
        myPlayer = GameObject.Find("Player").GetComponent<Player>();
        curTime = 0;
        blueDragonDuration = 10;
        blueDragonDelay = 0.7f;
    }

    void FixedUpdate()
    {
        ActivateBlueDragon();
        if(myPlayer.isSkill && myPlayer.skillIndex==0)
        {
            Countdown();
        }
    }

    void ActivateBlueDragon()
    {
        if(curTime > blueDragonDuration)
        {
            myPlayer.isSkill = false;
            curTime = 0;
            myPlayer.attackDelay = myPlayer.basicAttackDelay;
        }

        if (Input.GetButton("BlueDragon") && !myPlayer.isSkill && myPlayer.myPlayManager.skillCount>0)
        {
            myPlayer.isSkill = true;
            myPlayer.skillIndex = 0; //청룡 인덱스
            myPlayer.attackDelay = blueDragonDelay;
            myPlayer.myPlayManager.UltSkillActivate();
            myPlayer.myPlayManager.skillCount--;
        }
    }

    void Countdown()
    {
        curTime += Time.deltaTime;
        Debug.Log(curTime);
    }

    public void GoBlueDragon()
    {
        Vector3 attackPos1 = myPlayer.transform.position + myPlayer.FanPos;
        GameObject myBlueDragonAttack1 = Instantiate(blueDragonAttack, attackPos1, Quaternion.Euler(new Vector3(0, 0, 20)));
        Rigidbody2D rigid1 = myBlueDragonAttack1.GetComponent<Rigidbody2D>();
        rigid1.AddForce(Vector2.right * 10, ForceMode2D.Impulse);
        rigid1.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        Vector3 attackPos2 = myPlayer.transform.position + myPlayer.FanPos;
        GameObject myBlueDragonAttack2 = Instantiate(blueDragonAttack, attackPos2, transform.rotation);
        Rigidbody2D rigid2 = myBlueDragonAttack2.GetComponent<Rigidbody2D>();
        rigid2.AddForce(Vector2.right * 10, ForceMode2D.Impulse);

        Vector3 attackPos3 = myPlayer.transform.position + myPlayer.FanPos;
        GameObject myBlueDragonAttack3 = Instantiate(blueDragonAttack, attackPos3, Quaternion.Euler(new Vector3(0, 0, -20)));
        Rigidbody2D rigid3 = myBlueDragonAttack3.GetComponent<Rigidbody2D>();
        rigid3.AddForce(Vector2.right * 10, ForceMode2D.Impulse);
        rigid3.AddForce(Vector2.down * 5, ForceMode2D.Impulse);
            
        
    }
}
