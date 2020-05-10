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
    ~Food_Exist(){
        ClearFromWorld();
    }
    void Depleeting(){
        time = time - 1;
    }
    public void ClearFromWorld(){
        Game_World world;
        world = GameObject.Find("GameWorld_1").GetComponent<Game_World>();
        world.foods.Remove(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        time = time - 1;
        if (time<0){
            ClearFromWorld();
            Destroy(gameObject);
        }
    }
}
