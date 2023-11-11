using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueDragon : MonoBehaviour
{
    public float blueDragonDuration; //청룡공격 지속 시간
    private float curTime; 
    private bool isBlueDragon;   //청룡스킬 쓰는 중!!

    public GameObject blueDragonAttack; //청룡공격 prefab
    private Player myPlayer;

    void Awake()
    {
        myPlayer = GameObject.Find("Player").GetComponent<Player>();
        curTime = 0;
        blueDragonDuration = 10;
    }

    void FixedUpdate()
    {
        ActivateBlueDragon();
        if(myPlayer.isSkill)
        {
            Countdown();
        }
    }

    void ActivateBlueDragon()
    {
        if(curTime>blueDragonDuration)
        {
            //isActivate = false;
            myPlayer.isSkill = false;
            curTime = 0;
        }

        if (Input.GetButton("BlueDragon"))
        {
            //isActivate = true;
            myPlayer.isSkill = true;
            myPlayer.skillIndex = 0; //청룡 인덱스
        }
    }

    void Countdown()
    {
        curTime += Time.deltaTime;
    }

    public void GoBlueDragon()
    {
        Vector3 attackPos1 = myPlayer.transform.position + new Vector3(0, -1.5f, 0);
        GameObject myBlueDragonAttack1 = Instantiate(blueDragonAttack, attackPos1, transform.rotation);
        Rigidbody2D rigid1 = myBlueDragonAttack1.GetComponent<Rigidbody2D>();
        rigid1.AddForce(Vector2.right * 10, ForceMode2D.Impulse);

        Vector3 attackPos2 = myPlayer.transform.position;
        GameObject myBlueDragonAttack2 = Instantiate(blueDragonAttack, attackPos2, transform.rotation);
        Rigidbody2D rigid2 = myBlueDragonAttack2.GetComponent<Rigidbody2D>();
        rigid2.AddForce(Vector2.right * 10, ForceMode2D.Impulse);

        Vector3 attackPos3 = myPlayer.transform.position + new Vector3(0, 1.5f, 0);
        GameObject myBlueDragonAttack3 = Instantiate(blueDragonAttack, attackPos3, transform.rotation);
        Rigidbody2D rigid3 = myBlueDragonAttack3.GetComponent<Rigidbody2D>();
        rigid3.AddForce(Vector2.right * 10, ForceMode2D.Impulse);
            
        
    }
}
