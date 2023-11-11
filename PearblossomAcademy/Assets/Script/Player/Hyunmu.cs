using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hyunmu : MonoBehaviour
{
    public float HyunmuDuration; //현무공격 지속 시간
    private float curTime; 

    public GameObject HyunmuAttack; //현무공격 prefab
    private Player myPlayer;

    void Awake()
    {
        myPlayer = GameObject.Find("Player").GetComponent<Player>();

        curTime = 0;
        HyunmuDuration = 2;
    }

    void FixedUpdate()
    {
        ActivateHyunmu();
        if(myPlayer.isSkill && myPlayer.skillIndex==3)
        {
            Countdown();
        }
    }

    void ActivateHyunmu()
    {
        if(curTime > HyunmuDuration)
        {
            myPlayer.isSkill = false;
            curTime = 0;
        }

        if (Input.GetButton("Hyunmu") && !myPlayer.isSkill && myPlayer.myPlayManager.skillCount>0)
        {
            myPlayer.isSkill = true;
            myPlayer.skillIndex = 3; //현무 인덱스
            GoHyunmu();
            myPlayer.myPlayManager.skillCount--;
        }
    }

    void Countdown()
    {
        curTime += Time.deltaTime;
    }

    public void GoHyunmu()
    {
        Vector3 attackPos = myPlayer.transform.position;
        GameObject myHyunmuAttack = Instantiate(HyunmuAttack, attackPos, transform.rotation);
        Rigidbody2D rigid = myHyunmuAttack.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.right * 10, ForceMode2D.Impulse);
    }
}
