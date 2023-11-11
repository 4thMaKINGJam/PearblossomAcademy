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

    public GameObject playerBasicAttack; //기본공격 prefab
    public GameObject BlueDragon;
    public GameObject Jujak;
    public GameObject WhiteTiger;
    public GameObject Hyunmu;

    private GameObject myBlueDragon;

    public bool isSkill;
    public int skillIndex;

    private PlayManager myPlayManager;

    Rigidbody2D player;

    void Awake()
    {
        player = GetComponent<Rigidbody2D>();  
        myPlayManager = GameObject.Find("PlayManager").GetComponent<PlayManager>(); 
        isSkill = false;
        attackDelay = basicAttackDelay;
        AddSkills();
    }

    void FixedUpdate()
    {
        Move();
        Attack();  //공격
        ReloadAttack();  //공격 재장전
    }

    void AddSkills()
    {
        myBlueDragon = Instantiate(BlueDragon, transform);
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
                    case 0: myBlueDragon.GetComponent<BlueDragon>().GoBlueDragon(); break;
                    case 1: break;
                    case 2: break;
                    case 3: break;
                    default: break;
                }
            }
            else
            {
                Vector3 attackPos = transform.position;// + new Vector3(0, -0.5f, 0);
                GameObject myBasicAttack = Instantiate(playerBasicAttack, attackPos, transform.rotation);
                Rigidbody2D rigid = myBasicAttack.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.right * 10, ForceMode2D.Impulse);
            }
            
        }

        curDelay = 0;
    }

    void ReloadAttack()
    {
        curDelay += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Monster1Attack")
        {
            myPlayManager.playerLife--;
            Debug.Log("플레이어 목숨: "+ myPlayManager.playerLife);
        }
    }
}
