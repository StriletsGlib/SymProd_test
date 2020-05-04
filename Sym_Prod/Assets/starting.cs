using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starting : MonoBehaviour
{
    int a = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        while (a < 1000) { 
            a = a+1;
            if (a > 999) { Debug.Log("AAAAA"); }
        }
    }
}
