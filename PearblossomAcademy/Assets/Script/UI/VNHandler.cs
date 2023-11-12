using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VNHandler : MonoBehaviour
{
    private int currentIndex = 0;
    public GameObject[] visual_pics;
    public GameObject BG_black;
    public GameObject academy_room;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        visual_pics[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            // 클릭하면 다음 인덱스로 넘어가기
            currentIndex++;

            // 인덱스 범위 체크
            if (currentIndex < visual_pics.Length)
            {
                // 활성화된 이미지 세팅
                SetActiveVisual(currentIndex);

                // 특정 범위에 따른 추가 동작
                if (currentIndex >= 15 && currentIndex <= 18)
                {
                    // 15~18번 인덱스의 경우
                    BG_black.SetActive(true);
                    academy_room.SetActive(false);
                }
                else if (currentIndex >= 19 && currentIndex <= 29)
                {
                    // 19~29번 인덱스의 경우
                    BG_black.SetActive(false);
                    academy_room.SetActive(true);
                }
                else
                {
                    // 그 외의 경우
                    BG_black.SetActive(false);
                    academy_room.SetActive(false);
                }
            }
        }
    }

    void SetActiveVisual(int index)
    {
        // 모든 visual_pics를 비활성화
        for (int i = 0; i < visual_pics.Length; i++)
        {
            visual_pics[i].SetActive(false);
        }

        // 현재 인덱스의 visual_pic을 활성화
        visual_pics[index].SetActive(true);
        Debug.Log(index);

        if(index==28)
        {
            // gameManager.stage_count ++;
            SceneManager.LoadScene("Start");
        }
    }
}
