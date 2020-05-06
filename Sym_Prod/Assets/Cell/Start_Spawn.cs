using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_Spawn : MonoBehaviour
{
    // Start is called before the first frame update
    public int num = 5;
    void Start()
    {
        for (int i = 0; i < num; i++) {
            spawn();
            //cell a();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject cellBody;
    float randX, randY;
    Vector2 spawnPlace;
    public float borderY, borderX;
    float nextSpawn = 0;
    private void spawn()
    {
        randX = Random.Range(-borderX, borderX);
        randY = Random.Range(-borderY, borderY);
        spawnPlace = new Vector2(randX, randY);
        Instantiate(cellBody, spawnPlace, Quaternion.identity);
    }
}
