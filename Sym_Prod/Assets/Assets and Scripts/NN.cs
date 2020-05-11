using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NN
{
    RandomChancePercentage rand = new RandomChancePercentage();
    public float[] modifier = new float[12];
    float[] gxgy = new float[2];
    public NN(){
        for(int i = 0; i<12; i = i + 1){
            modifier[i] = 0;
        }
        for(int i = 0; i<2; i = i + 1){
            gxgy[i] = 0;
        }
    }
    public NN(NN copy){
        modifier = copy.modifier;
        gxgy = new float[2];
    }
    public void CopyFromNN(NN copy){
        modifier = copy.modifier;
    }
    public void GenNN(){
        for(int i = 0; i<12; i = i + 1){
            modifier[i] = ((float)Random.Range(-1000, 1000)) / 1000;
        }
    }
    public void mutate(int gene_stability){
        for (int i = 0; i<12; i++){
            if (rand.More(gene_stability)){
                modifier[i] += ((float)Random.Range(-100, 100)) / 1000;
                if (modifier[i]>1){modifier[i] = 1;}
                if (modifier[i]<-1){modifier[i] = -1;}
            }
        }
    }
    public float[] think(float[] input){
        float[] intermediet = new float[4];
        for(int i = 0; i<2; i++){
            intermediet[i*2] = input[i*4] * modifier[i*4] + input[i*4 +2] * modifier[i*4 +2];
            intermediet[i*2 + 1] = input[i*4 + 1] * modifier[i*4 + 1] + input[i*4 +3] * modifier[i*4 +3];
            gxgy[i] = intermediet[i*2] * modifier[8 +i*2] + intermediet[i*2 + 1]* modifier[8 +i*2 + 1];
        }
        
        ///
        return gxgy;
    }
}
