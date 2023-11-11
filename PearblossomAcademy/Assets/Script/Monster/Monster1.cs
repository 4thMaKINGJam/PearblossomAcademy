using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1 : MonoBehaviour
{
    public float speed; //이동속도 = 6 정도로 세팅
    public int monsterBasicAttackDamage; //기본공격의 데미지량

    public float basicAttackDelay; //기본공격 간격 조절
    private float curDelay; 

    public GameObject FoxCircle; //여우구슬 prefab
    int dir = 1;

    Rigidbody2D monster1;
   
    void Awake()
    {
        monster1 = GetComponent<Rigidbody2D>();
        
    }

    
    void FixedUpdate()
    {
        Move();
        BasicAttack();  //기본공격
        ReloadBasicAttack();    //기본공격 재장전
    }

    void Move()
    {
        
        
        Vector3 curPos = transform.position;
        Vector3 movePos = new Vector3(0, dir, 0) * speed * Time.deltaTime;

        transform.position = curPos + movePos;
    }

    //구미호 왕복 운동
    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Border")
        {
            dir *= (-1);
        }

    }

    //구미호가 player의 attack 받으면 damage 받게 하기



    //구미호 여우구슬 공격 - 유도총알X
    void BasicAttack()
    {
        if (curDelay < basicAttackDelay)
        {
            return;
        }

        Vector3 attackPos = transform.position + new Vector3(0, 0, 0);
        GameObject myBasicAttack = Instantiate(FoxCircle, attackPos, transform.rotation);
        Rigidbody2D rigid = myBasicAttack.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.left * 10, ForceMode2D.Impulse);
        
        curDelay = 0;
    }

    void ReloadBasicAttack()
    {
        curDelay += Time.deltaTime;
    }
}


