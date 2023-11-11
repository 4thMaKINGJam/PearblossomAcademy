using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster3Attack : MonoBehaviour
{
    public float speed = 4f;
    public float rotateSpeed = 150f; //200f

    GameObject target;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // 5초 후 DestroyGameObject 호출
        Invoke("DestroyGameObject", 3f);
        
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player");
    }

    void FixedUpdate()
    {
        if (target == null) return;

        Vector2 direction = (Vector2)target.transform.position - rb.position;
        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = -rotateAmount * rotateSpeed;
        rb.velocity = transform.up * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // if(collision.gameObject.tag == "Border")
        // {
        //     Destroy(gameObject);
        // }


        if(collision.gameObject.tag == "Player") //공격이 player에 닿으면 밝아졌다가 사라지기 - 공격적용
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
