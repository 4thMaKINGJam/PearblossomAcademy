using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster3 : MonoBehaviour
{
    //공격 속도
    public float speed = 0; //이동속도 = 0 정도로 세팅
    public float basicAttackDelay = 4; //공격들 간 간격 조절 - 4초 간격
    private float curDelay = 2.0f; 
    public float foodAttackDelay = 0.7f; //0.2초 간격으로 공격- 5초 간
    public float trimAttackDelay = 1; //1초 간격 - 5번 발사
    

    //공격 prefab
    public GameObject Food;//음식 prefab

    //애니메이션
    public Sprite[] sprites;

    //hp & 공격 damage
    int monsterHP;
    int playerBasicAttack, jujakAttack;

    Rigidbody2D monster;
    SpriteRenderer spriteRenderer;
    Rigidbody2D foodRigid;
    GameObject myFoodAttack;

   
    void Awake()
    {
        monster = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        PlayManager playManager = GameObject.Find("PlayManager").GetComponent<PlayManager>();
        monsterHP = playManager.monster3HP;
        playerBasicAttack = playManager.playerBasicAttack;
        jujakAttack = playManager.playerJujakAttack; 
    }

    
    //4초 간격으로 하게 변경 
    void FixedUpdate()
    {
        int i = Random.Range(0,2);
        switch(i){
            case 0://음식 공격
                FoodAttack(); //음식공격
                ReloadFoodAttack(); //음식 재장전
                break;
            case 1: //트림 공격
                TrimAttack();
                ReloadTrimAttack();
                break;
            default: 
                break;
        }
        
        Debug.Log("현재 monster damage: "+monsterHP);
    }


    void OnTriggerEnter2D(Collider2D collision){
        //도깨비 player의 attack 받으면 damage 받게 하기

        switch (collision.gameObject.tag){
            case "PlayerBasicAttack":
                OnHit(playerBasicAttack);
                break;
            case "JujakAttack":
                OnHit(jujakAttack);
                break;
            default:
                // 기본 처리 코드를 여기에 작성하세요.
                break;
        }


    }

    //몬스터 damage 받기
    void OnHit(int damage){
        //도깨비 체력 감소
        monsterHP -= damage;
        Debug.Log("현재 monster HP: "+monsterHP);
        
        //몬스터 맞았을 때 표정 변화
        spriteRenderer.sprite = sprites[1];
        Invoke("ReturnSprite",0.5f);
        
        //몬스터 사망
        if(monsterHP <=0){
            //몬스터 죽음 sprite - 표정 바꾸기
            spriteRenderer.sprite = sprites[1];
            Time.timeScale = 0;

            //게임 종료 씬으로 연결
        }

    }

    void ReturnSprite(){
        spriteRenderer.sprite = sprites[0];
    }



    //음식 유도총알 공격
    void FoodAttack()
    {
        if (curDelay < foodAttackDelay)
        {
            return;
        }

        Vector3 attackPos = transform.position + new Vector3(0, 0, 0);
        myFoodAttack = Instantiate(Food, attackPos, transform.rotation);
        foodRigid = myFoodAttack.GetComponent<Rigidbody2D>();

        // Rigidbody2D rigid = myBasicAttack.GetComponent<Rigidbody2D>();
        // rigid.AddForce(Vector2.left * 10, ForceMode2D.Impulse);
        
        curDelay = 0;
    }


    void ReloadFoodAttack()
    {
        curDelay += Time.deltaTime;
    }

    void TrimAttack(){

    }

    void ReloadTrimAttack(){

    }

    
}

