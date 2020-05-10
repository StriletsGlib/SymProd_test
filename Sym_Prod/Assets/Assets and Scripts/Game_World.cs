using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_World : MonoBehaviour
{
    public float borderX, borderY;
    public List<GameObject> pCells = new List<GameObject>();
    public List<GameObject> aCells = new List<GameObject>();
    public List<GameObject> foods = new List<GameObject>();
    public GameObject foodBody, cellBodyP, cellBodyA;
    public int cellPSpawn = 12, cellASpawn = 5;
    public float foodSpawnRate = 0.05f;
    float nextSpawn = 0;
    private Vector2 RandomVector2Gen()
    {
        float randX = Random.Range(-borderX, borderX);
        float randY = Random.Range(-borderY, borderY);
        Vector2 spawnPlace = new Vector2(randY, randX);
        return spawnPlace;
        //Instantiate(cellBody, spawnPlace, Quaternion.identity);
    }
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i< cellPSpawn; i++){
            pCells.Add(Instantiate(cellBodyP,RandomVector2Gen(), Quaternion.identity));
        }
        for(int i = 0; i< cellASpawn; i++){
            aCells.Add(Instantiate(cellBodyA,RandomVector2Gen(), Quaternion.identity));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawn) {
            nextSpawn = Time.time + foodSpawnRate;
            foods.Add(Instantiate(foodBody,RandomVector2Gen(), Quaternion.identity));
        }
    }
    public float[] searching(GameObject watcher){
        //GameObject nearestP, nearestA, nearestF;
        float[] coord = new float[8];
        float nearestDistance = float.MaxValue;
        foreach(var pCell in pCells){
            if(pCell !=watcher){
                if(Vector3.Distance(watcher.transform.position, pCell.transform.position)<nearestDistance){
                    coord[0] = watcher.transform.position.x - pCell.transform.position.x;
                    coord[1] = watcher.transform.position.y - pCell.transform.position.y;
                    nearestDistance =Vector3.Distance(watcher.transform.position, pCell.transform.position);
                }
            }
        }
        nearestDistance = float.MaxValue;
        foreach(var aCell in aCells){
            if(aCell !=watcher){
                if(Vector3.Distance(watcher.transform.position, aCell.transform.position)<nearestDistance){
                    coord[2] = watcher.transform.position.x - aCell.transform.position.x;
                    coord[3] = watcher.transform.position.y - aCell.transform.position.y;
                    nearestDistance =Vector3.Distance(watcher.transform.position, aCell.transform.position);
                }
                
            }
        }
        nearestDistance = float.MaxValue;
        foreach(var food in foods){
            if(Vector3.Distance(watcher.transform.position, food.transform.position)<nearestDistance){
                coord[4] = watcher.transform.position.x - food.transform.position.x;
                coord[5] = watcher.transform.position.y - food.transform.position.y;
                nearestDistance =Vector3.Distance(watcher.transform.position, food.transform.position);
            }
        }
        return coord;
    }
}
