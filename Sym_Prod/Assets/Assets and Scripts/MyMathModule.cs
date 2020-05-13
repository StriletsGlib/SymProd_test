using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMathModule
{
    public float floatSigma (float put){
        if(put <0){
            return -1;
        }
        if(put > 0){
            return 1;
        }
            return 0;
    }
    public float floatMod(float put){
        if (put < 0){
            put = -put;
        }
        return put;
    }
    public float floatCompareByMod(float a, float b){
        if (floatMod(a)>floatMod(b)){
            return a;
        }
        else{
            return b;
        }
    }
}

