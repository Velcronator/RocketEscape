using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region singleton setup
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    #endregion

    #region 
    public bool introIsComplete = false;

    #endregion

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void Update()
    {
        Debug.Log(introIsComplete);
    }
}
