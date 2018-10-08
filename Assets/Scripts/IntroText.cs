using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroText : MonoBehaviour
{
    private Text introText;
    private IEnumerator coroutine;
    Scene currentScene;

    private void Start()
    {
        introText = gameObject.GetComponent<Text>();
        currentScene = SceneManager.GetActiveScene();

        coroutine = WaitAndPrint(2f);
        StartCoroutine(coroutine);
        
        //during Intro, set text
        if (!GameManager.Instance.introIsComplete)
        {
            introText.text = currentScene.name;
        }
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        introText.fontSize = 72;
        introText.text = "Ready... Get to the platform!";
    }

    private void Update()
    {
        if (GameManager.Instance.introIsComplete)
        {
            introText.gameObject.SetActive(false);
        }
    }
}
