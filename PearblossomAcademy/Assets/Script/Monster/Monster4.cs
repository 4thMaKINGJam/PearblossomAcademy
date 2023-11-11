using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster4 : MonoBehaviour
{
    public float speed; //이동속도 = 6 정도로 세팅
    
    public float basicAttackDelay; //기본공격 간격 조절
    private float curDelay; 

    public GameObject Rock; //돌 prefab
    public Sprite[] sprites;
    int dir = 1;
    int monsterHP;
    int playerBasicAttack;

    Rigidbody2D monster4;
    SpriteRenderer spriteRenderer;

    public GameObject rockFragmentPrefab; // 도깨비불 조각 프리팹
    public int numberOfFragments = 7; // 생성할 조각의 수
    public float explosionForce = 3f; // 발산 힘의 크기
   
    void Awake()
    {
        monster4 = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        PlayManager playManager = GameObject.Find("PlayManager").GetComponent<PlayManager>();
        monsterHP = playManager.monster4HP;
        playerBasicAttack = playManager.playerBasicAttack;
        
    }

    
    void FixedUpdate()
    {
        Move();
        BasicAttack();  //기본공격
        ReloadBasicAttack(); //기본공격 재장전
        //Debug.Log("현재 monster damage: "+monsterHP);
    }

    void Move()
    {
        Vector3 curPos = transform.position;
        Vector3 movePos = new Vector3(0, dir, 0) * speed * Time.deltaTime;

        transform.position = curPos + movePos;
    }

    //귀수산 왕복 운동
    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Border")
        {
            dir *= (-1);
        }

        //귀수산이 player의 attack 받으면 damage 받게 하기
        if(collision.gameObject.tag == "PlayerBasicAttack"){
            OnHit(playerBasicAttack);
        }

    }

    //귀수산이 player의 attack 받으면 damage 받게 하기
    void OnHit(int damage){
        //귀수산 체력 감소
        monsterHP -= damage;
        
        //귀수산 맞았을 때 표정 변화
        spriteRenderer.sprite = sprites[1];
        Invoke("ReturnSprite",0.5f);
        
        //귀수산 사망
        if(monsterHP <=0){
            //귀수산 죽음 sprite - 표정 바꾸기
            spriteRenderer.sprite = sprites[1];
            DeathExplosion();
            //Time.timeScale = 0;

            //게임 종료 씬으로 연결
        }

    }

    void ReturnSprite(){
        spriteRenderer.sprite = sprites[0];
    }



    //귀수산 돌덩이 공격 - 유도총알X
    void BasicAttack()
    {
        if (curDelay < basicAttackDelay)
        {
            return;
        }

        Vector3 attackPos = transform.position + new Vector3(0, 0, 0);
        GameObject myBasicAttack = Instantiate(Rock, attackPos, transform.rotation);
        Rigidbody2D rigid = myBasicAttack.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.left * 10, ForceMode2D.Impulse);
        
        curDelay = 0;
    }

    void ReloadBasicAttack()
    {
        curDelay += Time.deltaTime;
    }

    void DeathExplosion()
    {
        float angleStep = 360f / numberOfFragments; // 각 조각의 각도 간격
        for (int i = 0; i < numberOfFragments; i++)
        {
            // 각도 계산
            float angle = i * angleStep;
            Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

            // 도깨비불 조각 생성 및 방향 설정
            GameObject fragment = Instantiate(rockFragmentPrefab, transform.position, Quaternion.identity);
            Rigidbody2D fragmentRigid = fragment.GetComponent<Rigidbody2D>();
            fragmentRigid.AddForce(direction * explosionForce, ForceMode2D.Impulse);
        }

        StartCoroutine(Delay());
    }


    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3f);
        Time.timeScale = 0;
    }
}