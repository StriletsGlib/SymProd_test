using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking_Cell : Cell
{
    // Start is called before the first frame update
    private void Start()
    {
        network.GenNN();
        if (state == "generated") {GenCell();}
        hunger = (int)(((sight + jump_leanght)*7.5 + gene_stability/25)*hunger_modifier);
    }
    override public void EatFood(int energy_eaten_cell){
        Debug.Log("eating a cell");
        energy_count= energy_count + Random.Range(food_min, food_max) + energy_eaten_cell;
        if (energy_count>energy_max) {energy_count = energy_max;}
    }
}
