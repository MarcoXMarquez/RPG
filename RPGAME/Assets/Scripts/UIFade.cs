using UnityEngine;
using UnityEngine.UI;   

public class UIFade : MonoBehaviour
{
    public static UIFade instance;
    public float fadeSpeed;    
    public Image fadeScreen;
    public bool shouldFadeToBlack;
    public bool shouldFadeFromBlack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed*Time.deltaTime));
            
            if(fadeScreen.color.a == 1f)
            {
                shouldFadeToBlack = false;
            }
        }
        if (shouldFadeFromBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            if(fadeScreen.color.a == 0f)
            {
                shouldFadeFromBlack = false;
            }   
        }
    }
    public void FadeToBlack()
    {
        Debug.Log("Fading to black");
        shouldFadeToBlack = true;
        shouldFadeFromBlack = false;
    }
    public void FadeFromBlack()
    {
        Debug.Log("Fading from black");
        shouldFadeFromBlack = true;
        shouldFadeToBlack = false;
    }
}
