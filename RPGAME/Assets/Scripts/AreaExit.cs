using UnityEngine;

public class AreaExit : MonoBehaviour
{           
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public string areaToLoad;
    public string areaTransitionName;
    public AreaEntrance entrance;   
    void Start()
    {
        entrance.transitionName = areaTransitionName;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(areaToLoad);
            PlayerController2D.instance.areaTransitionName = areaTransitionName;
        }
    }
}
