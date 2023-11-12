using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingHandler : MonoBehaviour
{
    public GameObject Button;

    void Awake()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3f);
        Button.SetActive(true);
    }

    public void GoBackToMain()
    {
        SceneManager.LoadScene("Start");
    }
}
