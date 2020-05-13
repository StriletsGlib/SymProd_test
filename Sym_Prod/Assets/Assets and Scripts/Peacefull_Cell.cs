using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peacefull_Cell : Cell
{
    private void Start()
    {
        //network.GenNN();
        if (state == "generated") {GenCell();}
        hunger = (int)(((jump_leanght)+ gene_stability/25)*hunger_modifier);
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
    override public void pregenNN(){
        float[] a = new float[] {0f, 0f, 0.5f, 0.5f, -0.9f, -0.9f, -0.9f, -0.9f, 1f, 1f, 1f, 1f};
        network = new NN(a);
        network.state = " preset ";
    }
}
