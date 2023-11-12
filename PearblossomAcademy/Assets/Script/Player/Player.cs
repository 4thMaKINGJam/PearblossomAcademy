using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed; //이동속도

    public float basicAttackDelay;
    public float attackDelay; //기본공격 간격 조절
    private float curDelay; 
    private int cnt;

    public GameObject playerBasicAttack; //기본공격 prefab
    public GameObject BlueDragon;
    public GameObject Jujak;
    public GameObject WhiteTiger;
    public GameObject Hyunmu;
    public GameObject[] LifeBlossom;

    private GameObject myBlueDragon;
    private GameObject myJujak;
    private GameObject myWhiteTiger;
    private GameObject myHyunmu;

    public bool isSkill;
    public int skillIndex;

    //오디오클립
    public AudioClip audioPlayerAttack; //플레이어공격
    // public AudioClip audioPlayerDamaged;  //플레이어데미지

    AudioSource audioSource;

    public PlayManager myPlayManager;
    //public GameManager myGameManager;

    Rigidbody2D player;
    SpriteRenderer spriteRenderer;

    public Sprite[] sprites;

    public Vector3 FanPos; //부채 위치

    void PlaySound(String action){
        switch(action){
            case "PlayerAttack":
                audioSource.clip = audioPlayerAttack;
                break;
            // case "PlayerDamaged":
            //     audioSource.clip = audioPlayerDamaged;
            //     break;
        } 
        audioSource.Play();
    }

    void Awake()
    {
        cnt = 0;
        player = GetComponent<Rigidbody2D>();  
        audioSource = GetComponent<AudioSource>();
        myPlayManager = GameObject.Find("PlayManager").GetComponent<PlayManager>(); 
        //myGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        isSkill = false;
        attackDelay = basicAttackDelay;
        spriteRenderer = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        FanPos = new Vector3(0, 1.2f, 0);
        AddSkills();
    }

    void FixedUpdate()
    {
        Move();
        Attack();  //공격
        ReloadAttack();  //공격 재장전
        ChangeSprite();
    }

    void AddSkills()
    {
        myBlueDragon = Instantiate(BlueDragon, transform);
        myJujak = Instantiate(Jujak, transform);
        myWhiteTiger = Instantiate(WhiteTiger, transform);
        myHyunmu = Instantiate(Hyunmu, transform);
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 curPos = transform.position;
        Vector3 movePos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        transform.position = curPos + movePos;
    }

    void Attack()
    {
        if (curDelay < attackDelay)
        {
            return;
        }

        if (Input.GetButton("BasicAttack"))
        {
            if(isSkill) //필살기 사용중이라면??
            {
                switch(skillIndex)
                {
                    case 0: 
                        myBlueDragon.GetComponent<BlueDragon>().GoBlueDragon(); 
                        break;
                    case 1: 
                        myJujak.GetComponent<Jujak>().GoJujak();
                        break;
                    case 2: 
                        Shoot(); 
                        break;
                    case 3: 
                        Shoot(); 
                        break;
                    default: 
                        break;
                }
            }
            else
            {
                Shoot();
        
            }

            curDelay = 0;
            
        }

        
    }

    void Shoot()
    {
        Vector3 attackPos = transform.position + FanPos;
        GameObject myBasicAttack = Instantiate(playerBasicAttack, attackPos, transform.rotation);
        Rigidbody2D rigid = myBasicAttack.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.right * 10, ForceMode2D.Impulse);
        //기본스킬사운드
        PlaySound("PlayerAttack");
    }

    void ReloadAttack()
    {
        curDelay += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "MonsterAttack")
        {
            // //PlayerDamagedSound
            // PlaySound("PlayerDamaged");
            if(!(isSkill && skillIndex==2)) //백호
            {
                myPlayManager.playerLife--;            
                Destroy(LifeBlossom[cnt]);
                cnt++;
                StartCoroutine(Flicker());
            } 
        }

        if(myPlayManager.playerLife<=0)
        {
            myPlayManager.GameOver();
            //myGameManager.GameOver();

        }
    }

    IEnumerator Flicker()
    {
        int count = 0;

        while (count < 2)
        {
            float fadeCnt = 0;
            while (fadeCnt < 1.0f)
            {
                fadeCnt += 0.1f;
                yield return new WaitForSeconds(0.01f);
                this.GetComponentInChildren<SpriteRenderer>().color = new Color(1,1,1,fadeCnt);
            }

            while (fadeCnt > 0f)
            {
                fadeCnt -= 0.1f;
                yield return new WaitForSeconds(0.01f);
                this.GetComponentInChildren<SpriteRenderer>().color = new Color(1,1,1,fadeCnt);
            }

            count++;
        }

        this.GetComponentInChildren<SpriteRenderer>().color = new Color(1,1,1,1);
    }

    public void HyunmuDamage()
    {
        myPlayManager.monster1HP -= myPlayManager.playerHyunmuAttack;
        myPlayManager.monster2HP -= myPlayManager.playerHyunmuAttack;
        myPlayManager.monster3HP -= myPlayManager.playerHyunmuAttack;
        myPlayManager.monster4HP -= myPlayManager.playerHyunmuAttack;
        myPlayManager.monster5HP -= myPlayManager.playerHyunmuAttack;
    }

    void ChangeSprite()
    {

        if(Input.GetButton("BasicAttack"))
        {
            spriteRenderer.sprite = sprites[1];
        }
        else
        {
            spriteRenderer.sprite = sprites[0];
        }
        
    }
}