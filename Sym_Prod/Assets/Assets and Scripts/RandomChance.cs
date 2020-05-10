using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomChancePercentage
{
    public bool More(int max){
        int r = Random.Range(0, 100);
        return (max>=r);
    }
}
