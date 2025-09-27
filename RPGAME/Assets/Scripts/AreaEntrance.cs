using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public string transitionName;
    void Start()
    {
        if(transitionName == PlayerController2D.instance.areaTransitionName)
        {
            PlayerController2D.instance.transform.position = transform.position;
        }
        UIFade.instance.FadeFromBlack();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
