using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1Attack : MonoBehaviour
{
   int damage; //기본공격뎀
   SpriteRenderer spriteRenderer;
   

   void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // PlayManager playManager = GameObject.Find("PlayManager").GetComponent<PlayManager>();
        
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Border")
        {
            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "PlayerBasicAttack") //여우구슬이 playerattack 맞으면 그냥 사라지기
        {
            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "Player") //여우구슬이 player에 닿으면 밝아졌다가 사라지기 - 공격적용
        {
            spriteRenderer.color = new Color(1,1,1,0.9f);
            //0.5초 후 사라지기
            Invoke("DestroyGameObject", 0.1f);
            // Destroy(gameObject);
        }
    }
        void DestroyGameObject()
        {
        Destroy(gameObject);
        }
}
