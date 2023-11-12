using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingHandler : MonoBehaviour
{
    public GameObject Button;
    public GameObject MirEnding;

    void Awake()
    {
        StartCoroutine(FadeIn());
    }

    public void GoBackToMain()
    {
        SceneManager.LoadScene("Start");
    }

    IEnumerator FadeIn()
    {
        float fadeCnt = 0;
        yield return new WaitForSeconds(3f);
        while (fadeCnt < 1.0f)
        {
            fadeCnt += 0.01f;
            yield return new WaitForSeconds(0.01f);
            MirEnding.GetComponent<Image>().color = new Color(1,1,1,fadeCnt);
        }

        yield return new WaitForSeconds(3f);
        Button.SetActive(true);

    }
}
