using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject cellBodyA, cellBodyB, Food;
    protected List<Peacefull_Cell> = new List<Peacefull_Cell>();
    protected List<Attacking_Cell> = new List<Attacking_Cell>();
    protected List<Food_Exist> = new List<Food_Exist>();
    float randX, randY;
    Vector2 spawnPlace;
    public int spawnA = 5, spawnB=12;
    public float spawnRate = 2;
    public float borderY, borderX;
    float nextSpawn = 1;
    private void Spawn(GameObject thing)
    {
        randX = Random.Range(-borderX, borderX);
        randY = Random.Range(-borderY, borderY);
        spawnPlace = new Vector2(randX, randY);
        Instantiate(thing, spawnPlace, Quaternion.identity);
    }
    private void Start(){
        for(int i = 0; i <spawnA; i=i+1){
            
        }
        for(int i = 0; i <spawnA; i=i+1){
            
            
            
        }
        
    }
    private void Update()
    {
        if (Time.time > nextSpawn) {
            nextSpawn = Time.time + spawnRate;
            Spawn(Food);
        }
    }
}
