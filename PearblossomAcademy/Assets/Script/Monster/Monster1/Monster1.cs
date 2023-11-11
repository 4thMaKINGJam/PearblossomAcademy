using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1 : MonoBehaviour
{
    public float speed; //이동속도 = 6 정도로 세팅
    
    public float basicAttackDelay; //기본공격 간격 조절
    private float curDelay; 

    public GameObject FoxCircle; //여우구슬 prefab
    public Sprite[] sprites;
    int dir = 1;
    int monsterHP;
    int playerBasicAttack;

    Rigidbody2D monster1;
    SpriteRenderer spriteRenderer;
    PlayManager playManager;
   
    void Awake()
    {
        monster1 = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playManager = GameObject.Find("PlayManager").GetComponent<PlayManager>();
        monsterHP = playManager.monster1HP;
        playerBasicAttack = playManager.playerBasicAttack;
        
    }

    
    void FixedUpdate()
    {
        if(playManager.isStartAttacking)
        {
            Move();
            BasicAttack();  //기본공격
            ReloadBasicAttack(); //기본공격 재장전
            //Debug.Log("현재 monster damage: "+monsterHP);
        }
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

        //구미호가 player의 attack 받으면 damage 받게 하기
        if(collision.gameObject.tag == "PlayerBasicAttack"){
            OnHit(playerBasicAttack);
        }

    }

    //구미호가 player의 attack 받으면 damage 받게 하기
    void OnHit(int damage){
        //구미호 체력 감소
        monsterHP -= damage;
        //Debug.Log("현재 monster damage: "+monsterHP);
        
        //구미호 맞았을 때 표정 변화
        spriteRenderer.sprite = sprites[1];
        Invoke("ReturnSprite",0.5f);
        
        //구미호 사망
        if(monsterHP <=0){
            //구미호 죽음 sprite - 표정 바꾸기
            spriteRenderer.sprite = sprites[1];
            playManager.MonsterClear(0);
            //Time.timeScale = 0;

            //게임 종료 씬으로 연결
        }

    }

    void ReturnSprite(){
        spriteRenderer.sprite = sprites[0];
    }



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


