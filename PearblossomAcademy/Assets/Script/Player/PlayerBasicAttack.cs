using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicAttack : MonoBehaviour
{
    public int damage; //기본공격뎀

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Border" || collision.gameObject.tag == "Monster")
        {
            Destroy(gameObject);
        }
    }
}
