using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster2 : MonoBehaviour
{
    public float speed = 0; //이동속도 = 0 정도로 세팅
    public float attackSpeed;
    
    public float basicAttackDelay; //공격 간격 조절 - 4초 간격
    private float curDelay; 

    public GameObject MonsterFire;//도깨비불 prefab
    public Sprite[] sprites;
    int monsterHP;
    int playerBasicAttack, player2Attack;

    Rigidbody2D monster;
    SpriteRenderer spriteRenderer;
    Rigidbody2D fireRigid;
   
    void Awake()
    {
        monster = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        PlayManager playManager = GameObject.Find("PlayManager").GetComponent<PlayManager>();
        monsterHP = playManager.monster2HP;
        playerBasicAttack = playManager.playerBasicAttack;
        player2Attack = playManager.player2Attack; 
        
    }

    
    void FixedUpdate()
    {
        FireAttack();  //도깨비불 공격
        ReloadFireAttack(); //도깨비불 재장전
        Debug.Log("현재 monster damage: "+monsterHP);
    }


    void OnTriggerEnter2D(Collider2D collision){
        //도깨비 player의 attack 받으면 damage 받게 하기

        switch (collision.gameObject.tag){
            case "PlayerBasicAttack":
                OnHit(playerBasicAttack);
                break;
            case "SomeOtherTag":
                // SomeOtherTag에 대한 처리 코드를 여기에 작성하세요.
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
        Debug.Log("현재 monster damage: "+monsterHP);
        
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
        float randomY = Random.Range(-0.8f, 0.8f); // -1과 1 사이의 랜덤한 값
        Vector2 randomDirection = new Vector2(-1, randomY).normalized; // 서쪽 방향으로 랜덤 벡터 생성
        // 랜덤한 방향으로 AddForce
        fireRigid.AddForce(randomDirection * attackSpeed, ForceMode2D.Impulse);
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
        foreach (Rigidbody2D projectile in firedProjectiles)
        {
            // 랜덤한 방향으로 힘을 가함
            float randomX = Random.Range(-1f, 1f);
            float randomY = Random.Range(-1f, 1f);
            Vector2 randomDirection = new Vector2(randomX, randomY).normalized;
            projectile.AddForce(randomDirection * explosionForce, ForceMode2D.Impulse);
        }

        firedProjectiles.Clear(); // 발사체 리스트를 비움

    }

    void ReloadFireAttack()
    {
        curDelay += Time.deltaTime;
    }

    
}


