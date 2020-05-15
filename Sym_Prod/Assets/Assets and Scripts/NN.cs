using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NN
{
    
    MyMathModule mathModule = new MyMathModule();
    public string state = " first random ";
    RandomChancePercentage rand = new RandomChancePercentage();
    public float[] modifier = new float[40];
    float[] gxgy = new float[2];
    public NN(){
        for(int i = 0; i<40; i = i + 1){
            modifier[i] = 0;
        }
        for(int i = 0; i<2; i = i + 1){
            gxgy[i] = 0;
        }
    }
    public NN(float[] input){
        modifier = new float[40];
        modifier[1] = input[0];
        modifier[4] = input[1];
        modifier[9] = input[2];
        modifier[12] = input[3];
        modifier[19] = input[4];
        modifier[22] = input[5];
        modifier[27] = input[6];
        modifier[30] = input[7];
        modifier[33] = input[8];
        modifier[34] = input[9];
        modifier[37] = input[10];
        modifier[38] = input[11];
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
        for(int i = 0; i<40; i = i + 1){
            modifier[i] = ((float)Random.Range(-1000, 1000)) / 1000;
        }
    }
    public void mutate(int gene_stability){
        for (int i = 0; i<40; i++){
            if (rand.More(gene_stability)){
                modifier[i] += ((float)Random.Range(-10, 10)) / 1000;
                if (modifier[i]>1){modifier[i] = 1;}
                if (modifier[i]<-1){modifier[i] = -1;}
            }
        }
    }
    public float[] think(float[] input){
        float[] intermediet = new float[4];
        for(int i = 1; i<8; i++){
            if (input[i]==0){}
            else {
                input[i] = mathModule.floatSigma(input[i]) * (1 - mathModule.floatMod(input[i]));
            }
        }
        for(int i = 0; i<8; i++){
            intermediet[0] = intermediet[0] + input[i] * modifier[4*i];
            intermediet[1] = intermediet[1] + input[i] * modifier[4*i + 1];
            intermediet[2] = intermediet[2] + input[i] * modifier[4*i + 2];
            intermediet[3] = intermediet[3] + input[i] * modifier[4*i + 3];
            /*
            if (mathModule.floatMod(input[i] * modifier[i] + input[i +2] * modifier[i +2])<0.07){
                return new float[2];
            }*/
            //intermediet[i] = mathModule.floatCompareByMod(input[i] * modifier[i],input[i +2] * modifier[i +2]);
            //intermediet[i] = input[i] * modifier[i] + input[i +2] * modifier[i +2];
            /*
            input[i] * modifier[i] + input[i +2] * modifier[i +2];
            if (mathModule.floatMod(input[i + 4] * modifier[i + 4] + input[i +6] * modifier[i +6])<0.07){
                return new float[2];
            }
            */
            //intermediet[i + 2] = mathModule.floatCompareByMod(input[i + 4] * modifier[i + 4],input[i +6] * modifier[i +6]);
            //intermediet[i + 2] = mathModule.floatCompareByMod(input[i + 4] * modifier[i + 4], input[i +6] * modifier[i +6]);
            //input[i + 4] * modifier[i + 4] + input[i +6] * modifier[i +6];
            //gxgy[i] = mathModule.floatCompareByMod(intermediet[i] * modifier[8 +i] , intermediet[i + 2]* modifier[8 +i + 2]);
            //intermediet[i] * modifier[8 +i] + intermediet[i + 2]* modifier[8 +i + 2];
        }
        gxgy[0] = 0;
        gxgy[1] = 0;
        for (int i = 0; i<4; i++){
            gxgy[0] += intermediet[i]* modifier[32 + i*2];
            gxgy[1] += intermediet[i]* modifier[33 + i*2];
        }
        return gxgy;
    }
    public string GetWeights(){
        string res = " WeightsNN= ";
        for(int i = 0; i<40; i++){
            res += modifier[i].ToString("0.000") + " ";
        }
        res += state;
        return res;
    }
}
