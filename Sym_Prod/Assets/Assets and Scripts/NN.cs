using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NN
{
    float[] modifier = new float[32];
    float[] gxgy = new float[4];
    public NN(){
        for(int i = 0; i<16; i = i + 1){
            modifier[i] = 0;
        }
        for(int i = 0; i<4; i = i + 1){
            gxgy[i] = 0;
        }
    }
    public void CopyFromNN(NN copy){
        modifier = copy.modifier;
    }
    public void GenNN(){
        for(int i = 0; i<16; i = i + 1){
            modifier[i] = ((float)Random.Range(-100, 100)) / 100;
        }
    }
    public float[] think(float[] input){
        for(int i = 0; i<4; i=i+1){
            float tempg = 0;
            for(int j = 0; j<8; j=j+1){
                tempg = tempg + modifier[i*8 + j]*input[j];
            }
            gxgy[i] = (int)tempg;
            if (tempg > 1){gxgy[i] = 1;}
            if (tempg < -1){gxgy[i] = -1;}
        }
        return gxgy;
    }
}
