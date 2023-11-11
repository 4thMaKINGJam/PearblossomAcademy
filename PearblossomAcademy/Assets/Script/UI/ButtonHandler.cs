using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ButtonHandler : MonoBehaviour
{
    public GameObject main_intro;
    public GameObject main_bdrgn;
    public GameObject main_jj;
    public GameObject main_whitetg;
    public GameObject main_hm;
    public GameObject main_ydrgn;

    void Start()
    {
        // 초기 상태에서는 main_bdrgn 만 활성화되어 있도록 설정
        ActivateObject(main_intro);

        // 각 버튼에 클릭 이벤트 핸들러를 연결
        Button button1 = GameObject.Find("btn_intro").GetComponent<Button>();
        Button button2 = GameObject.Find("btn_bd").GetComponent<Button>();
        Button button3 = GameObject.Find("btn_jj").GetComponent<Button>();
        Button button4 = GameObject.Find("btn_wt").GetComponent<Button>();
        Button button5 = GameObject.Find("btn_hm").GetComponent<Button>();
        Button button6 = GameObject.Find("btn_yd").GetComponent<Button>();
        Button button_start = GameObject.Find("stage_start").GetComponent<Button>();

        button2.onClick.AddListener(() => ActivateObject(main_bdrgn));
        button3.onClick.AddListener(() => ActivateObject(main_jj));
        button4.onClick.AddListener(() => ActivateObject(main_whitetg));
        button5.onClick.AddListener(() => ActivateObject(main_hm));
        button6.onClick.AddListener(() => ActivateObject(main_ydrgn));
        button1.onClick.AddListener(() => ActivateObject(main_intro));
        button_start.onClick.AddListener(() => ActivateScene());
    }

    // 모든 오브젝트를 비활성화하고 주어진 오브젝트만 활성화하는 함수
    void ActivateObject(GameObject targetObject)
    {
        main_intro.SetActive(false);
        main_bdrgn.SetActive(false);
        main_jj.SetActive(false);
        main_whitetg.SetActive(false);
        main_hm.SetActive(false);
        main_ydrgn.SetActive(false);
        targetObject.SetActive(true);
    }
    void ActivateScene()
    {
        if(main_intro.activeSelf)
        {
            SceneManager.LoadScene("Intro");
        }
        else if(main_bdrgn.activeSelf)
        {
            SceneManager.LoadScene("BlueDragon");
        }
        else if(main_jj.activeSelf)
        {
            SceneManager.LoadScene("Jujak");
        }
        else if(main_whitetg.activeSelf)
        {
            SceneManager.LoadScene("WhiteTiger");
        }
        else if(main_hm.activeSelf)
        {
            SceneManager.LoadScene("Hyunmu");
        }
        else if(main_ydrgn.activeSelf)
        {
            SceneManager.LoadScene("YellowDragon");
        }
    }
}

