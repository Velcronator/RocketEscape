using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroText : MonoBehaviour
{
    private Text introText;         //text variable for updating text
    private IEnumerator coroutine;  
    Scene currentScene;
    public int introCountdown = 3;  //amount of time the intro is active

    private void Start()
    {
        introText = gameObject.GetComponent<Text>();
        currentScene = SceneManager.GetActiveScene();

        coroutine = WaitforSeconds(introCountdown);
        StartCoroutine(coroutine);
        
        //during Intro, set text
        if (!GameManager.Instance.introIsComplete)
        {
            introText.fontSize = 72;
            introText.text = currentScene.name;
        }
    }

    //text can remain on screen till intro complete. at end of co-routine, set intro to complete
    private IEnumerator WaitforSeconds(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GameManager.Instance.introIsComplete = true;
    }

    private void Update()
    {
        //after half the intro time, change text
        if (Time.timeSinceLevelLoad > (introCountdown / 2))
        {
            introText.text = "Ready... Get to the platform!";
        }

        //once intro complete, turn off text
        if (GameManager.Instance.introIsComplete)
        {
            introText.gameObject.SetActive(false);
        }
    }
}
