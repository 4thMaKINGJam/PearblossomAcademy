using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed; //이동속도
    public int playerBasicAttackDamage; //기본공격의 데미지량

    public float basicAttackDelay; //기본공격 간격 조절
    private float curDelay; 

    public GameObject playerBasicAttack; //기본공격 prefab
    public GameObject BlueDragon;
    public GameObject Jujak;
    public GameObject WhiteTiger;
    public GameObject Hyunmu;

    Rigidbody2D player;

    void Awake()
    {
        player = GetComponent<Rigidbody2D>();   
    }

    void FixedUpdate()
    {
        Move();
        BasicAttack();  //기본공격
        ReloadBasicAttack();    //기본공격 재장전
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 curPos = transform.position;
        Vector3 movePos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        transform.position = curPos + movePos;
    }

    void BasicAttack()
    {
        if (curDelay < basicAttackDelay)
        {
            return;
        }

        if (Input.GetButton("BasicAttack"))
        {
            Vector3 attackPos = transform.position;// + new Vector3(0, -0.5f, 0);
            GameObject myBasicAttack = Instantiate(playerBasicAttack, attackPos, transform.rotation);
            Rigidbody2D rigid = myBasicAttack.GetComponent<Rigidbody2D>();
            rigid.AddForce(Vector2.right * 10, ForceMode2D.Impulse);
        }

        curDelay = 0;
    }

    void ReloadBasicAttack()
    {
        curDelay += Time.deltaTime;
    }
}
