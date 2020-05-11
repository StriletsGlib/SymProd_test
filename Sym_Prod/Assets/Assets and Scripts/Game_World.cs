using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_World : MonoBehaviour
{
    ListAnaliser list_analysis = new ListAnaliser();
    public float borderX, borderY;
    public List<GameObject> black_Holes = new List<GameObject>();
    public List<GameObject> pCells = new List<GameObject>();
    public List<GameObject> aCells = new List<GameObject>();
    public List<GameObject> foods = new List<GameObject>();
    public List<CellInfo> bestPCells = new List<CellInfo>();
    public List<CellInfo> bestACells = new List<CellInfo>();
    public GameObject foodBody, cellBodyP, cellBodyA, black_holeBody;
    public int cellPSpawn = 12, cellASpawn = 5;
    public int saveBestNum = 3;
    public int savedCells = 0;
    public float foodSpawnRate = 0.05f;
    public int xrad=26, yrad=16;
    float nextSpawn = 0, nextSpawnCellA = 0, nextSpawnCellB = 0;
    public float cellASpawnRate = 999999, cellPSpawnRate = 999999;
    public bool spawnCells = false;
    public bool restartWhenDead = false;
    bool needToSaveA = true;
    bool needToSaveP = true;
    bool needToSave = true;
    bool started = false;
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
        for(int dx = 0; dx<xrad; dx++){
            Vector2[] place = new Vector2[4];
            place[0] = new Vector2(dx, yrad-1);
            place[1] = new Vector2(dx, -yrad+1);
            place[2] = new Vector2(-dx, yrad-1);
            place[3] = new Vector2(-dx, -yrad+1);
            for(int i = 0; i<4; i++){black_Holes.Add(Instantiate(black_holeBody,place[i], Quaternion.identity));}
        }
        for(int dy = 0; dy<yrad; dy++){
            Vector2[] place = new Vector2[4];
            place[0] = new Vector2(xrad,dy);
            place[1] = new Vector2(-xrad,dy);
            place[2] = new Vector2(xrad,-dy);
            place[3] = new Vector2(-xrad,-dy);
            for(int i = 0; i<4; i++){black_Holes.Add(Instantiate(black_holeBody,place[i], Quaternion.identity));}
        }
        for(int i = 0; i< cellPSpawn; i++){
            pCells.Add(Instantiate(cellBodyP,RandomVector2Gen(), Quaternion.identity));
        }
        for(int i = 0; i< cellASpawn; i++){
            aCells.Add(Instantiate(cellBodyA,RandomVector2Gen(), Quaternion.identity));
        }
        started = true;
    }
    void SaveBestToList(List<GameObject> what, List<CellInfo> where){
        foreach (var objectCell in what){
            CellInfo regeneratingOne = new CellInfo(objectCell.GetComponent<Cell>());
            regeneratingOne.RegenerateEnergy();
            regeneratingOne.state = "respawned";
            where.Add(regeneratingOne);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if((restartWhenDead)&(saveBestNum>0)&started){
            //Debug.Log("DeadWorld");
            if ((list_analysis.Leangh<GameObject>(pCells) == saveBestNum)&needToSaveP){
                needToSaveP = false;
                SaveBestToList(pCells,bestPCells);
            }
            if ((list_analysis.Leangh<GameObject>(aCells) == saveBestNum)&needToSaveA){
                needToSaveA = false;
                SaveBestToList(aCells,bestACells);
            }
            if (!needToSaveA&!needToSaveP&needToSave){
                savedCells = savedCells + saveBestNum;
                needToSave = false;
                //Debug.Log("Respawn");
            }
        }
        if(restartWhenDead&(list_analysis.IsZero<GameObject>(pCells))&(list_analysis.IsZero<GameObject>(aCells))&started){
            foreach(var food in foods){
                Destroy(food);
            }
            foods.Clear();
            aCells.Clear();
            foods.Clear();
            for(int i = 0; i< cellPSpawn; i++){
                pCells.Add(Instantiate(cellBodyP,RandomVector2Gen(), Quaternion.identity));
            }
            for(int i = 0; i< cellASpawn; i++){
                aCells.Add(Instantiate(cellBodyA,RandomVector2Gen(), Quaternion.identity));
            }
            for(int i = 0; i< savedCells; i++){
                if(!started) break;
                if(i>list_analysis.Leangh<GameObject>(pCells)- 1) break;
                Cell respawning_cell = pCells[i].GetComponent<Cell>();
                //Debug.Log("Copy1");
                respawning_cell.CellInfoGet(bestPCells[savedCells -1 - i]);
            }
            for(int i = 0; i< savedCells; i++){
                if(!started) break;
                if(i>list_analysis.Leangh<GameObject>(aCells) - 1) break;
                Cell respawning_cell = pCells[i].GetComponent<Cell>();
                //Debug.Log("Copy2");
                respawning_cell.CellInfoGet(bestACells[savedCells - 1 - i]);
            }
            needToSaveA = true;
            needToSaveP = true;
            needToSave = true;
        }
        if (Time.time > nextSpawn) {
            nextSpawn = Time.time + foodSpawnRate;
            foods.Add(Instantiate(foodBody,RandomVector2Gen(), Quaternion.identity));
        }
        if (spawnCells&(Time.time > cellASpawnRate)) {
            nextSpawnCellA = Time.time + cellPSpawnRate;
            aCells.Add(Instantiate(cellBodyA,RandomVector2Gen(), Quaternion.identity));
        }
        if (spawnCells&(Time.time > nextSpawnCellB)) {
            nextSpawnCellB = Time.time + cellPSpawnRate;
            pCells.Add(Instantiate(cellBodyP,RandomVector2Gen(), Quaternion.identity));
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
        nearestDistance = float.MaxValue;
        foreach(var black_jole in black_Holes){
            if(Vector3.Distance(watcher.transform.position, black_jole.transform.position)<nearestDistance){
                coord[6] = watcher.transform.position.x - black_jole.transform.position.x;
                coord[7] = watcher.transform.position.y - black_jole.transform.position.y;
                nearestDistance =Vector3.Distance(watcher.transform.position, black_jole.transform.position);
            }
        }
        return coord;
    }
}
