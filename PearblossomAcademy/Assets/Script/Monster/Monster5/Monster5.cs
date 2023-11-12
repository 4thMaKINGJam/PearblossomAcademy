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
        GameObject myMonster2 = Instantiate(Monster2, StartPos, transform.rotation);
        myMonsterList.Add(myMonster2);
        isMonsterMoving[1] = true;
    }

    void GoMonster3()
    {
        GameObject myMonster3 = Instantiate(Monster3, StartPos, transform.rotation);
        myMonsterList.Add(myMonster3);
        isMonsterMoving[2] = true;
    }

    void GoMonster4()
    {
        GameObject myMonster4 = Instantiate(Monster4, StartPos, transform.rotation);
        myMonsterList.Add(myMonster4);
        isMonsterMoving[3] = true;
    }

    public void MoveOnToNextMonster(int killedMonsterIndex)
    {
        playManager.isStartAttacking = false;

        switch(killedMonsterIndex)
        {
            case 0: isMonsterMoving[0] = false; myMonsterList[0].SetActive(false); GoMonster2(); Debug.Log("일단 여기까진 진입"); break;
            case 1: isMonsterMoving[1] = false; myMonsterList[1].SetActive(false); GoMonster3(); break;
            case 2: isMonsterMoving[2] = false; myMonsterList[2].SetActive(false); GoMonster4(); break;
            case 3: playManager.GameClearFinal(); break;
            default: break;
        }
    }
}
