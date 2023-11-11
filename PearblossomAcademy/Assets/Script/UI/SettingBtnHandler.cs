using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;



public class SettingBtnHandler : MonoBehaviour
{
    public GameObject setting_window;
    public GameObject setting_btns;
    public int game_clear = 0;
    public bool isSound=true;
    public TMP_Text soundText;

    void Start()
    {
        game_clear = 0;
        Button setting_btn = GameObject.Find("btn_set").GetComponent<Button>();
        setting_btn.onClick.AddListener(() => ActivateSetting());
    }
    
    void ActivateSetting()
    {
        setting_window.SetActive(true);
        setting_btns.SetActive(true);

        Button sound_btn = GameObject.Find("sound").GetComponent<Button>();
        Button out_btn = GameObject.Find("exit").GetComponent<Button>();
        Button reset_btn = GameObject.Find("reset").GetComponent<Button>();
        
        sound_btn.onClick.AddListener(() => SoundSetting());
        out_btn.onClick.AddListener(() => ExitSetting());
        reset_btn.onClick.AddListener(() => ResetSetting());

    }

    void SoundSetting()
    {
        if(!isSound)
        {
            soundText.text = "소리 켜기";
            isSound = true;
        }
        else
        {
            soundText.text = "소리 끄기";
            isSound = false;
        }
        Debug.Log("소리변경");
    }

    void ExitSetting()
    {
        setting_window.SetActive(false);
        setting_btns.SetActive(false);
    }

    void ResetSetting()
    {
        SceneManager.LoadScene("Start");
    }
}
