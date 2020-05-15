using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class keyBoardManager : MonoBehaviour
{
    Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene("Menu");
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            camera.orthographicSize +=0.5f;
        }
        if(Input.GetKeyDown(KeyCode.Minus)){
            camera.orthographicSize -=0.5f;
        }
    }
}
