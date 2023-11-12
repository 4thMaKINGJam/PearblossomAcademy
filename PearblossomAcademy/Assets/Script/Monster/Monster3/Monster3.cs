using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster3 : MonoBehaviour
{
    //공격 속도
    public float speed = 0; //이동속도 = 0 정도로 세팅
    public float basicAttackDelay = 4; //공격들 간 간격 조절 - 4초 간격
    private float basicAttackTimer = 3.5f; 

    private float curDelay; 
    public float foodAttackDelay = 0.7f; //0.2초 간격으로 공격- 5초 간
    public float trimAttackDelay = 1; //1초 간격 - 5번 발사
    
    private bool isFoodAttacking = false;
    private bool isTrimAttacking = false;

    //GameManager gameManager;


    //공격 prefab
    public GameObject Food, Trim;//음식, 트림 prefab


    //애니메이션
    public Sprite[] sprites;

    //hp & 공격 damage
    int monsterHP;
    int playerBasicAttack, jujakAttack;

    //사운드
    public AudioClip audioMonsterDie;

    AudioSource audioSource;

    void PlaySound(String action){
        switch(action){
            case "MonsterDie":
                audioSource.clip = audioMonsterDie;
                break;
        } 
        audioSource.Play();
    }

    Rigidbody2D monster;
    SpriteRenderer spriteRenderer;
    Rigidbody2D foodRigid;
    GameObject myFoodAttack;
    PlayManager playManager;

   
    void Awake()
    {
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        monster = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playManager = GameObject.Find("PlayManager").GetComponent<PlayManager>();
        monsterHP = playManager.monster3HP;
        playerBasicAttack = playManager.playerBasicAttack;
        jujakAttack = playManager.playerJujakAttack; 

        audioSource = GetComponent<AudioSource>(); // AudioSource 컴포넌트 초기화
    }

    
    //4초 간격으로 하게 변경 
    void FixedUpdate()
    {
        if(playManager.isStartAttacking)
        {
            basicAttackTimer += Time.deltaTime;
        if (basicAttackTimer >= basicAttackDelay)
        {
            // 현재 진행 중인 음식 공격을 중단
            isFoodAttacking = false;
            isTrimAttacking = false;
            curDelay = 0;

            // 새로운 공격 선택
            int i = UnityEngine.Random.Range(0, 2);
            switch (i)
            {
                case 0: // 음식 공격
                    Debug.Log("음식 공격");
                    isFoodAttacking = true;
                    curDelay = foodAttackDelay; // 첫 공격이 즉시 실행되도록 설정
                    FoodAttack();
                    break;
                case 1: // 트림 공격
                    Debug.Log("트림 공격");
                    isTrimAttacking = true;
                    curDelay = trimAttackDelay; // 첫 공격이 즉시 실행되도록 설정
                    TrimAttack();
                    break;
                default:
                    break;
            }
            basicAttackTimer = 0; // 타이머 초기화
        }

        if (isFoodAttacking)
        {
            ReloadFoodAttack(); // 음식 공격의 타이밍 관리
        }
         if (isTrimAttacking)
        {
            ReloadTrimAttack(); // 트림 공격의 타이밍 관리
        }

        Debug.Log("현재 monster damage: " + monsterHP); 
        }
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
            //소리
            PlaySound("MonsterDie");
            //몬스터 죽음 sprite - 표정 바꾸기
            spriteRenderer.sprite = sprites[1];
            playManager.MonsterClear(2);
            //Time.timeScale = 0;

            //게임 종료 씬으로 연결
            //gameManager.GameClear();
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

        curDelay = 0;
    }


    void ReloadFoodAttack()
    {
        curDelay += Time.deltaTime;
    }

    void TrimAttack(){
        if (curDelay < trimAttackDelay)
        {
            return;
        }
        //무작위 세로 위치에서 트림 생성
        float i = UnityEngine.Random.Range(-3, 5);

        Vector3 attackPos = transform.position + new Vector3(-1.0f, i, 0);
        GameObject myBasicAttack = Instantiate(Trim, attackPos, transform.rotation);
        Rigidbody2D rigid = myBasicAttack.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.left * 14, ForceMode2D.Impulse);
        
        curDelay = 0;
    }

    void ReloadTrimAttack(){
        curDelay += Time.deltaTime;
    }

    
}


