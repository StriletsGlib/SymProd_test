using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Cell : MonoBehaviour
{
    public float sight, jump_leanght;
    public int energy_divided, energy_count, energy_max;
    public string state = "pregen";
    // Start is called before the first frame update
    public void genCell()
    {
        sight = (float)Random.Range(1, 30) / 100;
        jump_leanght = (float)Random.Range(1, 30) / 100;
        energy_divided = Random.Range(10, 100);
        energy_count = Random.Range(50, 75);
        energy_max = 250;
    }
    // Update is called once per frame
    private void Start()
    {
        genCell();
    }
    void EatFood(){
        energy_count= energy_count + 1;
        if (energy_count>energy_max) {energy_count = energy_max;}
    }
    void Update()
    {
        
        
    }
}
