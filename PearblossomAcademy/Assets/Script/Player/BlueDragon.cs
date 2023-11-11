using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueDragon : MonoBehaviour
{
    public float blueDragonCoolTime; //청룡공격 간격 조절
    private float curTime; 

    public GameObject blueDragonAttack; //청룡공격 prefab
    private Player myPlayer;

    void Awake()
    {
        myPlayer = GameObject.Find("Player").GetComponent<Player>();
    }

    void FixedUpdate()
    {
        GoBlueDragon();
        ReloadBlueDragon();
        Debug.Log(curTime);
    }

    void GoBlueDragon()
    {
        if (curTime < blueDragonCoolTime)
        {
            return;
        }

        if (Input.GetButton("BlueDragon"))
        {
            Vector3 attackPos1 = myPlayer.transform.position + new Vector3(0, -1f, 0);
            GameObject myBlueDragonAttack1 = Instantiate(blueDragonAttack, attackPos1, transform.rotation);
            Rigidbody2D rigid1 = myBlueDragonAttack1.GetComponent<Rigidbody2D>();
            rigid1.AddForce(Vector2.right * 10, ForceMode2D.Impulse);

            Vector3 attackPos2 = myPlayer.transform.position;
            GameObject myBlueDragonAttack2 = Instantiate(blueDragonAttack, attackPos2, transform.rotation);
            Rigidbody2D rigid2 = myBlueDragonAttack2.GetComponent<Rigidbody2D>();
            rigid2.AddForce(Vector2.right * 10, ForceMode2D.Impulse);

            Vector3 attackPos3 = myPlayer.transform.position + new Vector3(0, 1f, 0);
            GameObject myBlueDragonAttack3 = Instantiate(blueDragonAttack, attackPos3, transform.rotation);
            Rigidbody2D rigid3 = myBlueDragonAttack3.GetComponent<Rigidbody2D>();
            rigid3.AddForce(Vector2.right * 10, ForceMode2D.Impulse);
            
        }

        curTime = 0;

    }

    void ReloadBlueDragon()
    {
        curTime += Time.deltaTime;
    }
}
