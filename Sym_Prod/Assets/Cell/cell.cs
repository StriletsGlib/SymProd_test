using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class cell : MonoBehaviour
{
    public float sight, jump_leanght;
    public int energy_divided, energy_bank;
    public string state = "pregen";
    // Start is called before the first frame update
    public cell()
    {
        sight = Random.Range(1, 30)/100;
        jump_leanght = Random.Range(1, 30) / 100;
        energy_divided = Random.Range(10, 100);
        energy_bank = Random.Range(50, 75);
    }
    public void genCell()
    {
        sight = (float)Random.Range(1, 30) / 100;
        jump_leanght = (float)Random.Range(1, 30) / 100;
        energy_divided = Random.Range(10, 100);
        energy_bank = Random.Range(50, 75);
    }
    // Update is called once per frame
    private void Start()
    {
        new cell();
    }
    void Update()
    {
        if (state == "pregen") {
            genCell();
            state = "gen";
        }
    }
}
