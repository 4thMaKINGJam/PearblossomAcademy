using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster5 : MonoBehaviour
{
    public GameObject Monster1;
    public GameObject Monster2;
    public GameObject Monster3;
    public GameObject Monster4;

    private Vector3 StartPos;
    private Vector3 MainPos;

    private bool[] isMonsterMoving = new bool[] {false, false, false, false};
    private float Speed = 5f;

    private List<GameObject> myMonsterList = new List<GameObject>();
    
    PlayManager playManager;

    void Awake()
    {
        StartPos = GameObject.Find("StartPos").transform.position;
        MainPos = GameObject.Find("MainPos").transform.position;
        playManager = GameObject.Find("PlayManager").GetComponent<PlayManager>();
        GoMonster1();
    }

    void FixedUpdate()
    {
        ShowUp();
    }

    void ShowUp()
    {
        for(int i=0; i<4; i++)
        {
            if(isMonsterMoving[i])
            {
                myMonsterList[i].transform.position = Vector3.MoveTowards(myMonsterList[i].transform.position, MainPos, Speed * Time.deltaTime);
                if(myMonsterList[i].transform.position==MainPos){ isMonsterMoving[i]=false; playManager.isStartAttacking = true; }
            }
        }
    }

    void GoMonster1()
    {
        GameObject myMonster1 = Instantiate(Monster1, StartPos, transform.rotation);
        myMonsterList.Add(myMonster1);
        isMonsterMoving[0] = true;
    }

    void GoMonster2()
    {

    }

    void GoMonster3()
    {

    }

    void GoMonster4()
    {

    }

    public void MoveOnToNextMonster(int killedMonsterIndex)
    {

    }
}
