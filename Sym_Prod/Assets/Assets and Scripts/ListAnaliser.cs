using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListAnaliser
{
    public bool IsZero<T>(List<T> tested){
        foreach(var thing in tested){
            return false;
        }
        return true;
    }
    public int Leangh<T>(List<T> tested){
        int res = 0;
        foreach(var thing in tested){
            res++;
        }
        return res;
    }
}
