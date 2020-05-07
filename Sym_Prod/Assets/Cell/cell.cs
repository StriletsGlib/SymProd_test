using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Cell : MonoBehaviour
{
    public float sight, jump_leanght;
    public int energy_divided, energy_count, energy_max, gene_stability, hunger;
    public GameObject cellBody;
    public string state = "generated";
    
    public void genCell()
    {
        sight = (float)Random.Range(10, 30) / 100;
        jump_leanght = (float)Random.Range(1, 30) / 100;
        energy_divided = Random.Range(60000, 100000);
        energy_count = Random.Range(50000, 75000);
        energy_max = 100000;
        gene_stability = Random.Range(10, 50);
    }
    void mutate(){
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
        if((sight*jump_leanght*energy_divided)<=0){
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    private void Start()
    {
        if (state == "generated") {genCell();}
        hunger = (int)((sight + jump_leanght)*4 + gene_stability/30);
    }
    private void Inherit(float inhereted_sight, float inhereted_jump_leanght, int inhereted_energy_divided, int inhereted_energy_count, int inhereted_gene_stability){
        sight = inhereted_sight;
        jump_leanght = inhereted_jump_leanght;
        energy_divided = inhereted_energy_divided;
        energy_count = inhereted_energy_count;
        gene_stability = inhereted_gene_stability;
        //Debug.Log("hey!");
    }
    private void SetState(string newState){
        state = newState;
    }
    private void Division(){
        GameObject newObject = Instantiate(cellBody);
        energy_count = energy_count/2;
        Cell dividedCell = newObject.GetComponent<Cell>();
        dividedCell.Inherit(sight, jump_leanght, energy_divided, energy_count, gene_stability);
        dividedCell.SetState("divided");
        dividedCell.mutate();
        //Debug.Log(dividedCell.sight);
        //a.GetComponent<Position>();
    }
    public void EatFood(){
        energy_count= energy_count + Random.Range(400, 1000);
        if (energy_count>energy_max) {energy_count = energy_max;}
        //Debug.Log("what?");
    }
    void Update()
    {
        if(energy_count>=energy_divided){Division();}
        Hunger();
    }
    bool RandomChance(int max){
        int r = Random.Range(0, 100);
        return (max>=r);
    }
    void Hunger(){
        energy_count = energy_count - hunger;
        if (energy_count <0){
            Destroy(gameObject);
        }
    }
}
