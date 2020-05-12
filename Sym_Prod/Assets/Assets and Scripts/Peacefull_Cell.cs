using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peacefull_Cell : Cell
{
    private void Start()
    {
        //network.GenNN();
        if (state == "generated") {GenCell();}
        hunger = (int)(((sight + jump_leanght)/10+ gene_stability/25)*hunger_modifier);
    }
    ~Peacefull_Cell(){
        //Game_World world;
        //world = GameObject.Find("GameWorld_1").GetComponent<Game_World>();
        //world.pCells.Remove(gameObject);
    }
    override public void sendToList(GameObject what){
        Game_World world;
        world = GameObject.Find("GameWorld_1").GetComponent<Game_World>();
        world.pCells.Add(what);
    }
    override public void ClearFromWorld(){
        Game_World world;
        world = GameObject.Find("GameWorld_1").GetComponent<Game_World>();
        world.pCells.Remove(gameObject);
    }
}
