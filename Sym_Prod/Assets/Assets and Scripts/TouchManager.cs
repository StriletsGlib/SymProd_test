using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchManager : MonoBehaviour
{
    public Camera camera;
    float timeBetweenClicks = 2;
    float currentTime;
    int clicks = 0;
    // Start is called before the first frame update
    void Start()
    {
        //camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        currentTime = Time.time + timeBetweenClicks;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount>clicks){
            Debug.Log("or");
            clicks++;
            currentTime = Time.time + timeBetweenClicks;
        }
        if (Time.time > currentTime+timeBetweenClicks){
            if(Input.touchCount == 2){
                camera.orthographicSize +=0.5f;
            }
            if(Input.touchCount == 3){
                camera.orthographicSize -=0.5f;
            }
            clicks = 0;
        }
    }
}
