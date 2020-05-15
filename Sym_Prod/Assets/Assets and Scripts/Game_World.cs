using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Game_World : MonoBehaviour
{
    RandomChancePercentage randomChancePercentage = new RandomChancePercentage();
    float hunger_modifier = 1;
    public int gameSpeed = 1;
    public int chanceOfPreset = 50, chanceOfNNCell = 50;
    ListAnaliser list_analysis = new ListAnaliser();
    public string WhereToStoreCells = "Stored.Cells.txt";
    public bool ShouldStoreCellsInFile = false;
    public float borderX, borderY;
    public List<GameObject> borders = new List<GameObject>();
    public List<GameObject> pCells = new List<GameObject>();
    public List<GameObject> aCells = new List<GameObject>();
    public List<GameObject> pCellsAlg = new List<GameObject>();
    public List<GameObject> foods = new List<GameObject>();
    public List<CellInfo> bestPCells = new List<CellInfo>();
    public List<CellInfo> bestPCellsAlg = new List<CellInfo>();
    public List<CellInfo> bestACells = new List<CellInfo>();
    public GameObject foodBody, cellBodyP, cellBodyA, border_Body, cellBodyAlgP;
    public int cellPSpawn = 12, cellASpawn = 5;
    public int saveBestNum = 3;
    public int savedCells = 0, savedCellsP = 0, savedCellsPAlg = 0;
    public float foodSpawnRate = 0.05f;
    public int xrad=26, yrad=16;
    float nextSpawn = 0, nextSpawnCellA = 0, nextSpawnCellB = 0;
    public float cellASpawnRate = 999999, cellPSpawnRate = 999999;
    public bool spawnCells = false;
    public bool restartWhenDead = false;
    public bool doBordersKill = true;
    bool needToSaveA = true;
    bool needToSaveP = true;
    bool needToSave = true;
    bool started = false;
    public float[,] findThreeClosest(List<GameObject> fromWhere,  GameObject center){
        float[,] res = new float[3,2];
        List<GameObject> temp = new List<GameObject>();
        float maxVal = float.MaxValue;
        float nearestDistance = maxVal;
        GameObject tempNear = null;
        if (list_analysis.Leangh<GameObject>(fromWhere)<3){PanicFoodSpawn();}
        foreach(var item in fromWhere){
            if(Vector3.Distance(center.transform.position, item.transform.position)<nearestDistance){
                tempNear = item;
                nearestDistance = Vector3.Distance(center.transform.position, item.transform.position);
            }
        }
        temp.Add(tempNear);
        foreach(var item in fromWhere){
            if(item != temp[0]){
                if(Vector3.Distance(center.transform.position, item.transform.position)<nearestDistance){
                    tempNear = item;
                    nearestDistance = Vector3.Distance(center.transform.position, item.transform.position);
                }
            }
        }
        temp.Add(tempNear);
        foreach(var item in fromWhere){
            if((item != temp[0])&(item != temp[1])){
                if(Vector3.Distance(center.transform.position, item.transform.position)<nearestDistance){
                    tempNear = item;
                    nearestDistance = Vector3.Distance(center.transform.position, item.transform.position);
                }
            }
        }
        for (int i = 0; i<3; i++){
        res[i, 0] = (temp[0].transform.position.x - center.transform.position.x);
        res[i, 1]  = (temp[0].transform.position.y - center.transform.position.y);
        //res[0] = new float[2] {tempNear[0].transfor.transform.position.x -center.transform.position.x, tempNear[0].transfor.transform.position.y -center.transform.position.y};
        }
        return res;
    }
    void PanicFoodSpawn(){
        foods.Add(Instantiate(foodBody,RandomVector2Gen(), Quaternion.identity));
        foods.Add(Instantiate(foodBody,RandomVector2Gen(), Quaternion.identity));
        foods.Add(Instantiate(foodBody,RandomVector2Gen(), Quaternion.identity));
    }
    private Vector2 RandomVector2Gen()
    {
        float randX = Random.Range(-borderX, borderX);
        float randY = Random.Range(-borderY, borderY);
        Vector2 spawnPlace = new Vector2(randX, randY);
        return spawnPlace;
        //Instantiate(cellBody, spawnPlace, Quaternion.identity);
    }
    void getImportantInfo(){
        ImportantData sourceOfData = GameObject.Find("Start_Data").GetComponent<ImportantData>();
        gameSpeed = sourceOfData.gameSpeed;
        WhereToStoreCells = sourceOfData.WhereToStoreCells;
        ShouldStoreCellsInFile = sourceOfData.ShouldStoreCellsInFile;
        borderX = sourceOfData.borderX;
        borderY = sourceOfData.borderY;
        cellPSpawn = sourceOfData.cellPSpawn;
        cellASpawn = sourceOfData.cellASpawn;
        saveBestNum = sourceOfData.saveBestNum;
        foodSpawnRate = sourceOfData.foodSpawnRate;
        xrad = sourceOfData.xrad;
        yrad = sourceOfData.yrad;
        cellASpawnRate = sourceOfData.cellASpawnRate;
        cellPSpawnRate = sourceOfData.cellPSpawnRate;
        spawnCells = sourceOfData.spawnCells;
        restartWhenDead = sourceOfData.restartWhenDead;
        xrad = sourceOfData.xrad;
        xrad = sourceOfData.xrad;
        chanceOfPreset = sourceOfData.chanceOfPreset;
        doBordersKill =sourceOfData.doBordersKill;
        chanceOfNNCell =sourceOfData.chanceOfNNCell;
        hunger_modifier = sourceOfData.hunger_modifier;
        //cellPSpawn = sourceOfData.intData[0];
        //cellASpawn = sourceOfData.intData[1];
        //saveBestNum =sourceOfData.intData[2];
        
    }
    // Start is called before the first frame update
    void Start()
    {
        getImportantInfo();
        //WriteCellsDown("text.txt");
        for(int dx = 0; dx<xrad; dx++){
            Vector2[] place = new Vector2[4];
            place[0] = new Vector2(dx, yrad-1);
            place[1] = new Vector2(dx, -yrad+1);
            place[2] = new Vector2(-dx, yrad-1);
            place[3] = new Vector2(-dx, -yrad+1);
            for(int i = 0; i<4; i++){
                borders.Add(Instantiate(border_Body,place[i], Quaternion.identity));
            }
        }
        for(int dy = 0; dy<yrad; dy++){
            Vector2[] place = new Vector2[4];
            place[0] = new Vector2(xrad,dy);
            place[1] = new Vector2(-xrad,dy);
            place[2] = new Vector2(xrad,-dy);
            place[3] = new Vector2(-xrad,-dy);
            for(int i = 0; i<4; i++){
                borders.Add(Instantiate(border_Body,place[i], Quaternion.identity));
            }
        }
        foreach(var borderBlock in borders){
            borderBlock.GetComponent<BoxCollider2D>().isTrigger = doBordersKill;
        }
        SpawnBasicSells();
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
    void SavingIfNeeded(){
        if((restartWhenDead)&(saveBestNum>0)&started){
            //Debug.Log("DeadWorld");
            if ((list_analysis.Leangh<GameObject>(pCells) + list_analysis.Leangh<GameObject>(pCellsAlg) == saveBestNum)&needToSaveP){
                needToSaveP = false;
                SaveBestToList(pCells,bestPCells);
                SaveBestToList(pCellsAlg,bestPCellsAlg);
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
    }
    void ClearGame(){
        foreach(var food in foods){
            Destroy(food);
        }
        foods.Clear();
        aCells.Clear();
        foods.Clear();
    }
    void SpawnBasicSells(){
        int p = 0, alg = 0;
        for(int i = 0; i< cellPSpawn; i++){
            if(randomChancePercentage.More(chanceOfNNCell)){
                pCells.Add(Instantiate(cellBodyP,RandomVector2Gen(), Quaternion.identity));
                pCells[p].GetComponent<Cell>().gameSpeed = gameSpeed;
                pCells[p].GetComponent<Cell>().presetNNChance= chanceOfPreset;
                pCells[p].GetComponent<Cell>().state="generated";
                pCells[p].GetComponent<Cell>().hunger_modifier=hunger_modifier;
                p++;
            }
            else{
                pCellsAlg.Add(Instantiate(cellBodyAlgP,RandomVector2Gen(), Quaternion.identity));
                pCellsAlg[alg].GetComponent<Cell>().gameSpeed = gameSpeed;
                pCellsAlg[alg].GetComponent<Cell>().presetNNChance= chanceOfPreset;
                pCellsAlg[alg].GetComponent<Cell>().state="generated";
                pCellsAlg[alg].GetComponent<Cell>().hunger_modifier=hunger_modifier;
                alg++;
            }
        }
        for(int i = 0; i< cellASpawn; i++){
            aCells.Add(Instantiate(cellBodyA,RandomVector2Gen(), Quaternion.identity));
            aCells[i].GetComponent<Cell>().gameSpeed = gameSpeed;
            aCells[i].GetComponent<Cell>().presetNNChance= chanceOfPreset;
            aCells[i].GetComponent<Cell>().state="generated";
            aCells[i].GetComponent<Cell>().hunger_modifier=hunger_modifier;
        }
    }
    bool ShouldGameRestart(){
        return(restartWhenDead&(list_analysis.IsZero<GameObject>(pCells))&(list_analysis.IsZero<GameObject>(aCells))&started);
    }
    void RespawnNeededCells(){
        for(int i = 0; i< savedCells; i++){
            if(!started) break;
            if(i>list_analysis.Leangh<GameObject>(pCells)- 1) break;
            Cell respawning_cell = pCells[i].GetComponent<Cell>();
            respawning_cell.CellInfoGet(bestPCells[savedCells -1 - i]);
        }
        for(int i = 0; i< savedCells; i++){
            if(!started) break;
            if(i>list_analysis.Leangh<GameObject>(pCellsAlg)- 1) break;
            Cell respawning_cell = pCellsAlg[i].GetComponent<Cell>();
            respawning_cell.CellInfoGet(bestPCellsAlg[savedCells -1 - i]);
        }
        for(int i = 0; i< savedCells; i++){
            if(!started) break;
            if(i>list_analysis.Leangh<GameObject>(aCells) - 1) break;
            Cell respawning_cell = pCells[i].GetComponent<Cell>();
            respawning_cell.CellInfoGet(bestACells[savedCells - 1 - i]);
        }
    }
    void GameRestart(){
        ClearGame();
        SpawnBasicSells();
        RespawnNeededCells();
        needToSaveA = true;
        needToSaveP = true;
        needToSave = true;
        if (ShouldStoreCellsInFile) WriteCellsDown(WhereToStoreCells);
    }
    void WriteCellsDown(string text){
        if(File.Exists(text)) File.Delete(text);
        StreamWriter sw = File.CreateText(text);
        foreach(var PCellInfo in bestPCells){
            //System.Environment.NewLine
            sw.WriteLine("Peacefull_Cell");
            sw.WriteLine(PCellInfo.GetParameters());
            sw.WriteLine("");
        }
        foreach(var ACellInfo in bestACells){
            //System.Environment.NewLine
            sw.WriteLine("Attacking_Cell");
            sw.WriteLine(ACellInfo.GetParameters());
        }
    }
    void RateSpawn(){
        if (Time.time > nextSpawn) {
            nextSpawn = Time.time + foodSpawnRate/gameSpeed;
            foods.Add(Instantiate(foodBody,RandomVector2Gen(), Quaternion.identity));
        }
        if (spawnCells&(Time.time > cellASpawnRate)) {
            nextSpawnCellA = Time.time + cellPSpawnRate/gameSpeed;
            aCells.Add(Instantiate(cellBodyA,RandomVector2Gen(), Quaternion.identity));
        }
        if (spawnCells&(Time.time > nextSpawnCellB)) {
            nextSpawnCellB = Time.time + cellPSpawnRate/gameSpeed;
            pCells.Add(Instantiate(cellBodyP,RandomVector2Gen(), Quaternion.identity));
        }
    }
    // Update is called once per frame
    void Update()
    {
        list_analysis.clearFromNull<GameObject>(pCells);
        list_analysis.clearFromNull<GameObject>(pCellsAlg);
        list_analysis.clearFromNull<GameObject>(aCells);
        SavingIfNeeded();
        if(ShouldGameRestart()){
            GameRestart();
        }
        RateSpawn();
    }
    public float[] findClosest(GameObject watcher, List<GameObject> fromWhere, float MaxDistance){
        float[] coord = new float[2];
        float nearestDistance = MaxDistance;
        foreach(var thing in fromWhere){
            if((thing !=watcher)&(thing != null)){
                if(Vector3.Distance(watcher.transform.position, thing.transform.position)<nearestDistance){
                    coord[0] = (thing.transform.position.x - watcher.transform.position.x)/MaxDistance;
                    coord[1] = (thing.transform.position.y - watcher.transform.position.y)/MaxDistance;
                    nearestDistance =Vector3.Distance(watcher.transform.position, thing.transform.position);
                }
            }
        }
        return coord;
    }
    public float[] Searching(GameObject watcher, float MaxDistance){
        //GameObject nearestP, nearestA, nearestF;
        float[] coord = new float[8];
        //System.Array.Copy(findClosest(watcher, pCells, MaxDistance), 0, coord, 0, 2);
        //System.Array.Copy(findClosest(watcher, aCells, MaxDistance), 0, coord, 4, 2);
        float nearestDistance = MaxDistance;
        foreach(var pCell in pCells){
            if((pCell !=watcher)&(pCell !=null)){
                if(Vector3.Distance(watcher.transform.position, pCell.transform.position)<nearestDistance){
                    coord[0] = (pCell.transform.position.x - watcher.transform.position.x)/MaxDistance;
                    coord[1] = (pCell.transform.position.y - watcher.transform.position.y)/MaxDistance;
                    nearestDistance =Vector3.Distance(watcher.transform.position, pCell.transform.position);
                }
            }
        }
        foreach(var pCell in pCellsAlg){
            if((pCell !=watcher)&(pCell !=null)){
                if(Vector3.Distance(watcher.transform.position, pCell.transform.position)<nearestDistance){
                    coord[0] = (pCell.transform.position.x - watcher.transform.position.x)/MaxDistance;
                    coord[1] = (pCell.transform.position.y - watcher.transform.position.y)/MaxDistance;
                    nearestDistance =Vector3.Distance(watcher.transform.position, pCell.transform.position);
                    //Debug.Log("to Attack = " + nearestDistance * 10000);
                }
            }
        }
        nearestDistance = MaxDistance;
        foreach(var aCell in aCells){
            if((aCell !=watcher)&(aCell !=null)){
                if(Vector3.Distance(watcher.transform.position, aCell.transform.position)<nearestDistance){
                    coord[4] = (aCell.transform.position.x - watcher.transform.position.x)/MaxDistance;
                    coord[5] = (aCell.transform.position.y - watcher.transform.position.y)/MaxDistance;
                    nearestDistance =Vector3.Distance(watcher.transform.position, aCell.transform.position);
                    //Debug.Log("to Attack = " + nearestDistance * 10000);
                }
            }
        }
        nearestDistance = MaxDistance;
        foreach(var food in foods){
            if((Vector3.Distance(watcher.transform.position, food.transform.position)<nearestDistance)&(food !=null)){
                //float fdx, fdy;
                MyMathModule myMathModule = new MyMathModule();
                //fdx = (food.transform.position.x - watcher.transform.position.x)/MaxDistance;
                //fdy = (food.transform.position.y - watcher.transform.position.y)/MaxDistance;
                //if ((myMathModule.floatMod(fdx - coord[4])>0.01)&(myMathModule.floatMod(fdy - coord[5])>0.01)){
                coord[2] = (food.transform.position.x - watcher.transform.position.x)/MaxDistance;
                coord[3] = (food.transform.position.y - watcher.transform.position.y)/MaxDistance;
                    nearestDistance =Vector3.Distance(watcher.transform.position, food.transform.position);
                //}
                //if ((coord[2]-coord[4]<0.01)||(coord[2]-coord[4]<0.01))
                //coord[6] = (food.transform.position.x - coord[4]*MaxDistance)/MaxDistance;
                //coord[7] = (food.transform.position.y - coord[5]*MaxDistance)/MaxDistance;
                 //Debug.Log("to Food = " + nearestDistance * 10000);
            }
        }

        nearestDistance = MaxDistance;
        foreach(var border in borders){
            if((Vector3.Distance(watcher.transform.position, border.transform.position)<nearestDistance)&(border!=null)){
                coord[6] = (border.transform.position.x - watcher.transform.position.x)/MaxDistance;
                coord[7] = (border.transform.position.y - watcher.transform.position.y)/MaxDistance;
                nearestDistance =Vector3.Distance(watcher.transform.position, border.transform.position);
            }
        }
        return coord;
    }
}
