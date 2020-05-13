using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImportantData : MonoBehaviour
{
    public int gameSpeed = 1;
    public int chanceOfPreset = 50, chanceOfNNCell = 50;
    public string WhereToStoreCells = "Stored.Cells.txt";
    public bool ShouldStoreCellsInFile = false;
    public float borderX = 30, borderY=15, hunger_modifier = 1;
    public int cellPSpawn = 12, cellASpawn = 5;
    public int saveBestNum = 10;
    public float foodSpawnRate = 0.025f;
    public int xrad=32, yrad=18;
    public float cellASpawnRate = 10, cellPSpawnRate = 10;
    public bool spawnCells = false;
    public bool restartWhenDead = true;
    public bool doBordersKill = false;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.DontDestroyOnLoad(gameObject);
    }
}
