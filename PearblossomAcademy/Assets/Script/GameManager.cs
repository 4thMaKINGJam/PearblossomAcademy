using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool[] usableSkill = new bool[4];
    public bool isGameover = false;
    public int GameClear=0;

    public void OnPlayerDead() {
        isGameover = true;
    }

}
