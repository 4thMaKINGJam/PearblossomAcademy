using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster2 : MonoBehaviour
{
    public float speed = 0; //이동속도 = 0 정도로 세팅
    public float attackSpeed;
    public float basicAttackDelay; //공격 간격 조절 - 4초 간격
    public GameObject MonsterFire;//도깨비불 prefab
    public Sprite[] sprites;

    int monsterHP;
    int playerBasicAttack, jujakAttack;

    //GameManager gameManager;

    private float curDelay = 2.0f; 

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
    Rigidbody2D fireRigid;
    PlayManager playManager;
     Animator anim;

    public GameObject fireFragmentPrefab; // 도깨비불 조각 프리팹
    public int numberOfFragments = 7; // 생성할 조각의 수
    public float explosionForce = 3f; // 발산 힘의 크기
   
    void Awake()
    {
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        monster = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        anim.SetBool("isAttacked", false);
        playManager = GameObject.Find("PlayManager").GetComponent<PlayManager>();
        monsterHP = playManager.monster2HP;
        playerBasicAttack = playManager.playerBasicAttack;
        jujakAttack = playManager.playerJujakAttack; 

        audioSource = GetComponent<AudioSource>(); // AudioSource 컴포넌트 초기화
        
    }

    
    void FixedUpdate()
    {
        if(playManager.isStartAttacking)
        {
        FireAttack();  //도깨비불 공격
        ReloadFireAttack(); //도깨비불 재장전
        Debug.Log("현재 monster damage: "+monsterHP);
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
         anim.SetBool("isAttacked", true);
          Invoke("isAttacked",0.3f);
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
            playManager.MonsterClear(1);
            //Time.timeScale = 0;

            //gameManager.GameClear();

            //게임 종료 씬으로 연결
        }

        

    }

    void isAttacked(){
        anim.SetBool("isAttacked", false);

    }

    void ReturnSprite(){
        spriteRenderer.sprite = sprites[0];
    }



    //도깨비불 공격
    void FireAttack()
    {
        if (curDelay < basicAttackDelay)
        {
            return;
        }

        //랜덤한 방향으로 도깨비에서 나오게 생성, AddForce해서 
        Vector3 attackPos = transform.position + new Vector3(0, 0, 0);
        GameObject myFireAttack = Instantiate(MonsterFire, attackPos, transform.rotation);
        fireRigid = myFireAttack.GetComponent<Rigidbody2D>();

        // 랜덤한 y 성분 생성
        float RandomY = UnityEngine.Random.Range(-0.8f, 0.8f); // -1과 1 사이의 랜덤한 값
        Vector2 RandomDirection = new Vector2(-1, RandomY).normalized; // 서쪽 방향으로 랜덤 벡터 생성
        // 랜덤한 방향으로 AddForce
        fireRigid.AddForce(RandomDirection * attackSpeed, ForceMode2D.Impulse);
        // 0.5초 후 멈춤
        Invoke("Stop", 0.5f);


        //1초 후 발산
        Invoke("FireWork", 1.0f);
        
        curDelay = 0;
    }

    void Stop()
    {
        fireRigid.velocity = Vector2.zero;
    }


    void FireWork(){//1.5초 후 발산
        float angleStep = 360f / numberOfFragments; // 각 조각의 각도 간격
        for (int i = 0; i < numberOfFragments; i++)
        {
            // 각도 계산
            float angle = i * angleStep;
            Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

            // 도깨비불 조각 생성 및 방향 설정
            GameObject fragment = Instantiate(fireFragmentPrefab, fireRigid.position, Quaternion.identity);
            Rigidbody2D fragmentRigid = fragment.GetComponent<Rigidbody2D>();
            fragmentRigid.AddForce(direction * explosionForce, ForceMode2D.Impulse);
        }

        // 원본 발사체는 제거
        Destroy(fireRigid.gameObject);

    }

    void ReloadFireAttack()
    {
        curDelay += Time.deltaTime;
    }

    
}


