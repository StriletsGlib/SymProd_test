using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peacefull_Cell : Cell
{
    private void Start()
    {
        if (state == "generated") {GenCell();}
        hunger = (int)(((sight + jump_leanght)*7.5 + gene_stability/25)*hunger_modifier);
    }
}
