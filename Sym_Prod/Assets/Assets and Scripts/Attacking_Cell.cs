using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking_Cell : Cell
{
    // Start is called before the first frame update
    private void Start()
    {
        if (state == "generated") {GenCell();}
        hunger = (int)(((jump_leanght) + gene_stability/25)*hunger_modifier);
    }
    ~Attacking_Cell(){
        //Game_World world;
        //world = GameObject.Find("GameWorld_1").GetComponent<Game_World>();
        //world.aCells.Remove(gameObject);
    }
    override public void EatFood(int energy_eaten_cell){
        //Debug.Log("eating a cell");
        energy_count= energy_count + Random.Range(food_min, food_max) + energy_eaten_cell/10;
        if (energy_count>energy_max) {energy_count = energy_max;}
    }
    override public void sendToList(GameObject what){
        Game_World world;
        world = GameObject.Find("GameWorld_1").GetComponent<Game_World>();
        world.aCells.Add(what);
    }
    override public void ClearFromWorld(){
        Game_World world;
        world = GameObject.Find("GameWorld_1").GetComponent<Game_World>();
        world.aCells.Remove(gameObject);
    }
    override public void pregenNN(){
        float[] a = new float[24] {1f, 1f, 0f, 0f, 0f, 0f, 0f, 0f, 1f, 1f, 1f, 1f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f};
        network = new NN(a);
        network.state = " preset ";
    }
}
