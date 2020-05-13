using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NN
{
    public string state = " first random ";
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
    public NN(float[] input){
        for(int i = 0; i<12; i++){
            modifier[i] = input[i];
        }
    }
    public NN(NN copy){
        modifier = copy.modifier;
        gxgy = new float[2];
        state = copy.state;
    }
    public void CopyFromNN(NN copy){
        modifier = copy.modifier;
        state = copy.state;
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
        //string seing =(" xp "+input[0].ToString("0.00") + " yp "+ input[1].ToString("0.00") +" xf "+input[2].ToString("0.00") +" yf  "+input[3].ToString("0.00")+" xa "+input[4].ToString("0.00") +" ya  " + input[5].ToString("0.00")+" xv "+input[6].ToString("0.00") +" yv "+input[7].ToString("0.00"));
        //Debug.Log(seing);
        for(int i = 0; i<2; i++){
            intermediet[i] = input[i] * modifier[i] + input[i +2] * modifier[i +2];
            intermediet[i + 2] = input[i + 4] * modifier[i + 4] + input[i +6] * modifier[i +6];
            gxgy[i] = intermediet[i] * modifier[8 +i] + intermediet[i + 2]* modifier[8 +i + 2];
        }
        return gxgy;
    }
    public string GetWeights(){
        string res = " WeightsNN= ";
        for(int i = 0; i<12; i++){
            res += modifier[i].ToString("0.000") + " ";
        }
        res += state;
        return res;
    }
}
