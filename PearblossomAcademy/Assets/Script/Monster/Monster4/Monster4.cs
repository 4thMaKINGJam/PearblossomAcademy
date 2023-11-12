using System;
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
    PlayManager playManager;

    public GameObject rockFragmentPrefab; // 도깨비불 조각 프리팹
    public int numberOfFragments = 10; // 생성할 조각의 수
    public int explosionForce = 30; // 발산 힘의 크기

    private bool isExplode;

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
   
    void Awake()
    {
        monster4 = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playManager = GameObject.Find("PlayManager").GetComponent<PlayManager>();
        monsterHP = playManager.monster4HP;
        playerBasicAttack = playManager.playerBasicAttack;

        audioSource = GetComponent<AudioSource>(); // AudioSource 컴포넌트 초기화
        
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
        if(monsterHP <=0 && !isExplode){
            //귀수산 죽음 sprite - 표정 바꾸기
            spriteRenderer.sprite = sprites[1];
            isExplode = true;
            StartCoroutine(DeathExplosion());
        }

    }

    void ReturnSprite(){
        spriteRenderer.sprite = sprites[0];
    }



    //귀수산 돌덩이 공격 - 유도총알X
    void BasicAttack()
    {
        if (curDelay < basicAttackDelay || isExplode)
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

    //돌멩이확산
    IEnumerator DeathExplosion()
    {
        speed = 0;
        this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0); 
        GameObject FragmentPos = GameObject.Find("FragmentPos");
        List<GameObject> rockFragments = new List<GameObject>();
        HashSet<int> usedIndexes = new HashSet<int>();

        yield return new WaitForSeconds(1f);
        for (int i = 0; i < numberOfFragments; i++)
        {
            GameObject fragment = Instantiate(rockFragmentPrefab, FragmentPos.transform.GetChild(i).gameObject.transform.position, Quaternion.identity);
            rockFragments.Add(fragment);
        }
        
        yield return new WaitForSeconds(3f);

        int[] randomRockOrder = new int[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
        int shuffle = UnityEngine.Random.Range(10, 100);
        
        for(int i=0; i<shuffle; i++)
        {
            int rand1 = UnityEngine.Random.Range(0, numberOfFragments);
            int rand2 = UnityEngine.Random.Range(0, numberOfFragments);
            int a = randomRockOrder[rand1];
            randomRockOrder[rand1] = randomRockOrder[rand2];
            randomRockOrder[rand2] = a;
        }
        for (int j=0; j<numberOfFragments; j++)
        {
            Rigidbody2D fragmentRigid = rockFragments[randomRockOrder[j]].GetComponent<Rigidbody2D>();
                fragmentRigid.AddForce(Vector2.left * explosionForce, ForceMode2D.Impulse);
                Debug.Log(randomRockOrder[j]);
                yield return new WaitForSeconds(0.5f);
        }

        /*
        int attempts = 0;
        while (usedIndexes.Count < numberOfFragments && attempts < numberOfFragments * 2)
        {
            int j = Random.Range(0, numberOfFragments);
            if (!usedIndexes.Contains(j))
            {
                Rigidbody2D fragmentRigid = rockFragments[j].GetComponent<Rigidbody2D>();
                fragmentRigid.AddForce(Vector2.left * explosionForce, ForceMode2D.Impulse);
                usedIndexes.Add(j);
                yield return new WaitForSeconds(0.5f);
            }
            attempts++;
        }
        */
        yield return new WaitForSeconds(3f);
        playManager.MonsterClear(3);
    }

}
