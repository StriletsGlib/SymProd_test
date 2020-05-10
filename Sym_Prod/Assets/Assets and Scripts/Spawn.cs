using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject cellBody;
    float randX, randY;
    Vector2 spawnPlace;
    public float spawnRate = 2;
    public float borderY, borderX;
    float nextSpawn = 0;
    private void Update()
    {
        if (Time.time > nextSpawn) {
            nextSpawn = Time.time + spawnRate;
            randX = Random.Range(-borderX, borderX);
            randY = Random.Range(-borderY, borderY);
            spawnPlace = new Vector2(randX, randY);
            Instantiate(cellBody, spawnPlace, Quaternion.identity);
            //GameObject body = Instantiate(cellBody, spawnPlace, Quaternion.identity);
            //GameWorld a = GetComponent<GameWorld>();
            //Food_Exist food1 = body.GetComponent<Food_Exist>();
            
        }
    }
}
