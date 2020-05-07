using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunger_Modifier : MonoBehaviour
{
    private Cell modified;
    public float hunger_modifier = 1;
    // Start is called before the first frame update
    void Start()
    {
        modified = GetComponent<Cell>();
        modified.hunger = (int)(((modified.sight + modified.jump_leanght)*7.5 + modified.gene_stability/25)*hunger_modifier);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
