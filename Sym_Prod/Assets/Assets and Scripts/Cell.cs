using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Cell : MonoBehaviour
{
    //int number = 0;
    public int presetNNChance = 0;
    public int gameSpeed = 1;
    public float sight, jump_leanght;
    public int food_min=400, food_max=1000;
    public int energy_divided = 999999, energy_count = -1, energy_max = 999999, gene_stability = 999999, hunger = 999999;
    public int minimum_energy_divided = 2500;
    public GameObject divisionBody;
    public string state = "generated";
    public float hunger_modifier = 1;
    public NN network = new NN();
    Vector2 prev_Move = new Vector2(0,1);
    public void GenCell()
    {
        sight = (float)Random.Range(900, 1200) / 100;
        jump_leanght = (float)Random.Range(100, 300) / 100;
        energy_divided = Random.Range(10000, 25000);
        //energy_count = Random.Range(50000, 75000);
        energy_count = 50000;
        energy_max = 1000000;
        gene_stability = Random.Range(10, 50);
        divState();
        network = new NN();
        network.GenNN();
        if (RandomChance(presetNNChance)){
            pregenNN();
            state = "improvedAI?";
        }
        hunger = (int)(((jump_leanght)/10 + gene_stability/25)*hunger_modifier);
    }
    public virtual void divState(){
        state = "gen";
    }
    public virtual void pregenNN(){
        Debug.Log("hey hey, people!");
    }
    public void CellInfoGet(CellInfo copied_cell){
        sight = copied_cell.sight;
        jump_leanght = copied_cell.jump_leanght;
        food_min = copied_cell.food_min;
        food_max = copied_cell.food_max;
        energy_divided = copied_cell.energy_divided;
        energy_count = copied_cell.energy_count;
        energy_max = copied_cell.energy_max;
        gene_stability = copied_cell.gene_stability;
        hunger = copied_cell.hunger;
        minimum_energy_divided = copied_cell.minimum_energy_divided;
        divisionBody = copied_cell.divisionBody;
        state = copied_cell.state;
        hunger_modifier = copied_cell.hunger_modifier;
        network = new NN(copied_cell.network);
        gameSpeed = copied_cell.gameSpeed;
    }
    
    void Mutate(){
        if (RandomChance(gene_stability)){
            sight = sight + ((float)(Random.Range(10, 15) - Random.Range(10, 20))/ 100);
        }
        if (RandomChance(gene_stability)){
            jump_leanght = jump_leanght + ((float)(Random.Range(10, 15) - Random.Range(10, 20))/ 100);
        }
        if (RandomChance(gene_stability)){
            energy_divided = energy_divided + (Random.Range(10, 15) - Random.Range(10, 15));
        }
        if (RandomChance(gene_stability)){
            gene_stability = gene_stability + (Random.Range(10, 15) - Random.Range(10, 15));
        }
        network.mutate(gene_stability);
        if((sight*jump_leanght*(energy_divided-minimum_energy_divided+1))<=0){
            ClearFromWorld();
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        if (state == "generated") {GenCell();}
        hunger = (int)(((sight + jump_leanght) + gene_stability/25)*hunger_modifier);
        //Debug.Log("xd");
    }
    public void RegenerateEnergy(){
        //energy_count = Random.Range(50000, 75000);
        energy_count = 50000;
    }
    private void Inherit(float inhereted_sight, float inhereted_jump_leanght, int inhereted_energy_divided, int inhereted_energy_count, int inhereted_gene_stability, NN net){
        sight = inhereted_sight;
        jump_leanght = inhereted_jump_leanght;
        energy_divided = inhereted_energy_divided;
        energy_count = inhereted_energy_count;
        gene_stability = inhereted_gene_stability;
        
        network = new NN(net);
        //Debug.Log("hey!");
    }
    private void SetState(string newState){
        state = newState;
    }
    public virtual void sendToList(GameObject what){
        Debug.Log("hey hey, people!");
    }
     private void Division(){
        GameObject newObject = Instantiate(divisionBody);
        energy_count = energy_count/2;
        Cell dividedCell = newObject.GetComponent<Cell>();
        dividedCell.Inherit(sight, jump_leanght, energy_divided, energy_count, gene_stability, network);
        dividedCell.SetState("divided");
        dividedCell.Mutate();
        sendToList(newObject);
        //Debug.Log(dividedCell.sight);
        //a.GetComponent<Position>();
    }
    public virtual void EatFood(){
        energy_count= energy_count + Random.Range(food_min, food_max);
        if (energy_count>energy_max) {energy_count = energy_max;}
        //Debug.Log("what?");
    }
    public virtual void EatFood(int energy_eaten_cell){
    }
    void Update()
    {
        if (state == "generated") {GenCell();}
        ThinkAndMove();
        if(energy_count>=energy_divided){Division();}
        Hunger();
    }
    bool RandomChance(int max){
        int r = Random.Range(0, 100);
        return (max>=r);
    }
    virtual public void ClearFromWorld(){}
    void Hunger(){
        energy_count = energy_count - hunger* gameSpeed;
        if (energy_count <0){
            ClearFromWorld();
            Destroy(gameObject);
        }
    }
    virtual public Vector2 thinkDirection(){
        float[] k = new float[8];
        Game_World world;
        world = GameObject.Find("GameWorld_1").GetComponent<Game_World>();
        k = world.Searching(gameObject, sight);
        float[] res = network.think(k);
        Vector2 movement_vector = new Vector2(res[0], res[1]);
        //Debug.Log("resx = " + res[0]*100 + "resy = " + res[1]);
        //Debug.Log("resx1 = " + movement_vector.x + "resy1 = " + movement_vector.y);
        MyMathModule mod = new MyMathModule();
        movement_vector.Normalize();
        movement_vector = movement_vector * 10;
        return movement_vector;
    }
    virtual public void ThinkAndMove(){
        Vector2 movement_vector = thinkDirection();
        Vector2 place = new Vector2(movement_vector.x + transform.position.x, movement_vector.y +transform.position.y);
        float angle = Vector2.SignedAngle(prev_Move, movement_vector);
        transform.position = Vector2.MoveTowards(transform.position, place, jump_leanght* Time.deltaTime * gameSpeed);
        //Quaternion stand_in = transform.rotation;
        //stand_in.RotateAround(new Vector2(transform.position.x, transform.position.y), Vector3.forward, angle);
        transform.RotateAround(new Vector2(transform.position.x, transform.position.y), Vector3.forward, angle);
        prev_Move = movement_vector;
    }
}
