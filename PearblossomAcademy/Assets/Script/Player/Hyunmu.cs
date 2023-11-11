using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hyunmu : MonoBehaviour
{
    private float curTime; 

    public GameObject HyunmuAttack; //주작공격 prefab
    private Player myPlayer;

    void Awake()
    {
        myPlayer = GameObject.Find("Player").GetComponent<Player>();
        curTime = 0;
    }

    void ActivateHyunmu()
    {

        if (Input.GetButton("Hyunmu") && !myPlayer.isSkill)
        {
            myPlayer.isSkill = true;
            myPlayer.skillIndex = 3; //현무 인덱스
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
