using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jujak : MonoBehaviour
{
    public float jujakDuration; //주작공격 지속 시간
    public float jujakDelay; //주작공격 공격 간격
    private float curTime; 

    public GameObject jujakAttack; //주작공격 prefab
    private Player myPlayer;

    void Awake()
    {
        myPlayer = GameObject.Find("Player").GetComponent<Player>();
        curTime = 0;
        jujakDuration = 10;
        jujakDelay = 0.7f;
    }

    void FixedUpdate()
    {
        ActivateJujak();
        if(myPlayer.isSkill && myPlayer.skillIndex==1)
        {
            Countdown();
        }
    }

    void ActivateJujak()
    {
        if(curTime > jujakDuration)
        {
            myPlayer.isSkill = false;
            curTime = 0;
            myPlayer.attackDelay = myPlayer.basicAttackDelay;
        }

        if (Input.GetButton("Jujak") && !myPlayer.isSkill && myPlayer.myPlayManager.skillCount>0)
        {
            myPlayer.isSkill = true;
            myPlayer.skillIndex = 1; //주작 인덱스
            myPlayer.attackDelay = jujakDelay;
            myPlayer.myPlayManager.skillCount--;
        }
    }

    void Countdown()
    {
        curTime += Time.deltaTime;
    }

    public void GoJujak()
    {
        Vector3 attackPos = myPlayer.transform.position;
        GameObject myJujakAttack = Instantiate(jujakAttack, attackPos, transform.rotation);
        Rigidbody2D rigid = myJujakAttack.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.right * 10, ForceMode2D.Impulse);
    }
}
