using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellInfo
{
    public float sight, jump_leanght;
    public int food_min=400, food_max=1000, gameSpeed = 1;
    public int energy_divided = 999999, energy_count = -1, energy_max = 999999, gene_stability = 999999, hunger = 999999;
    public int minimum_energy_divided = 12000;
    public GameObject divisionBody;
    public string state = "generated";
    public float hunger_modifier = 1;
    public NN network = new NN();
    public CellInfo(Cell copied_cell){
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
    public CellInfo(AlgPeacefull_Cell copied_cell){
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
        float[] putThis = new float[8] {(float)copied_cell.fearOfEnemies, (float)copied_cell.avoidness, 0, 0, 0, 0, 0, 0};
        network.modifier = putThis;
        network.state = "notARealNetwork";
    }
    public void RegenerateEnergy(){
        energy_count = Random.Range(50000, 75000);
    }
    public string GetParameters(){
        string res = "";
        res = res + " sight= " + sight.ToString("0.00") + " jump_leanght= "+ jump_leanght.ToString("0.00") + " food_min= " + food_min + " food_max= " + food_max + " energy_divided= " + energy_divided + " energy_max = " + energy_max + " gene_stability= " + gene_stability + " minimum_energy_divided= " + minimum_energy_divided + " hunger_modifier = " + hunger_modifier.ToString("0.00") + network.GetWeights();
        return res;
    }
}
