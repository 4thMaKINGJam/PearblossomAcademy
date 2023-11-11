using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1Attack : MonoBehaviour
{
   int damage; //기본공격뎀

   void Awake()
    {
    
        PlayManager playManager = GameObject.Find("PlayManager").GetComponent<PlayManager>();
        damage = playManager.monsterFoxCircle;
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Border")
        {
            Destroy(gameObject);
        }

        // if(collision.gameObject.tag == "PlayerBasicAttack")
        // {
        //     Destroy(gameObject);
        // }
    }
}
