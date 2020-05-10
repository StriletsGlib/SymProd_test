using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food_Exist : MonoBehaviour
{
    public int number = 0;
    // Start is called before the first frame update
    public int time;
    void Start()
    {
        
    }
    void Depleeting(){
        time = time - 1;
    }
    // Update is called once per frame
    void Update()
    {
        time = time - 1;
        if (time<0){Destroy(gameObject);}
    }
}
