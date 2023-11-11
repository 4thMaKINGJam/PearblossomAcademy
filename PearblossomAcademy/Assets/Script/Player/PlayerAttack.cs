using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Border" || collision.gameObject.tag == "Monster")
        {
            Destroy(gameObject);
        }
    }
}
