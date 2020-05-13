using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AlgPeacefull_Cell : Cell
{
    RandomChancePercentage randomChancePercentage = new RandomChancePercentage();
    public double fearOfEnemies = 4, avoidness = 4;
    // Start is called before the first frame update
    void Start()
    {
        if (state == "generated") {GenCell();}
        hunger = (int)(((jump_leanght)+ gene_stability/25)*hunger_modifier);
    }
    override public void divState(){
        state = "alggen";
    }
    //This was created --
    public float[] whereToGo(float[] food1, float[] food2, float[] food3, float[] enemy, float[] friend)
    {
        double food1_K = (rasst(food1,0)-fearOfEnemies/rasst(food1,enemy))*(rasst(food1,0)-avoidness/rasst(friend,0));
        double food2_K = (rasst(food2, 0) - fearOfEnemies / rasst(food2, enemy)) * (rasst(food2, 0) - avoidness / rasst(friend, 0));
        double food3_K = (rasst(food3, 0) - fearOfEnemies / rasst(food3, enemy)) * (rasst(food3, 0) - avoidness / rasst(friend, 0));
        if (food1_K <=food2_K && food1_K <=food3_K)
        { return food1; }
        else {
            if (food2_K <=food3_K)
            { return food2; }
            else {
                return food3;
            }
        }
    }
    public double rasst(float [] food1, float [] food2)
    {
        float x1 = food1[0];
        float y1 = food1[1];
        float x2 = food2[0];
        float y2 = food2[1];
        
        double rasst=  Math.Sqrt(((x2 - x1)*(x2 - x1) + (y2 - y1)*(y2 - y1)));
        return rasst;
    }
    public double rasst(float[] food1, int x)
    {
        float x1 = food1[0];
        float y1 = food1[1];
        float x2 = 0;
        float y2 = 0;
        
        double rasst = Math.Sqrt(((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1)));
        return rasst;
    }
    //end of this was created --
    override public Vector2 thinkDirection(){
        Game_World world;
        world = GameObject.Find("GameWorld_1").GetComponent<Game_World>();
        float[,] foodArr = world.findThreeClosest(world.foods, gameObject);
        float[] fooda = new float[2] {foodArr[0,0], foodArr[0,1]};
        float[] foodb = new float[2] {foodArr[1,0], foodArr[1,1]};
        float[] foodc = new float[2] {foodArr[2,0], foodArr[2,1]};
        float[] enemyA = world.findClosest(gameObject,world.pCells, float.MaxValue);
        float[] allayA = world.findClosest(gameObject,world.aCells, float.MaxValue);
        float[] res = whereToGo(fooda, foodb, foodc, enemyA, allayA);
        Vector2 movement_vector = new Vector2(res[0], res[1]);
        return movement_vector;
    }
    override public void ClearFromWorld(){
        Game_World world;
        world = GameObject.Find("GameWorld_1").GetComponent<Game_World>();
        world.pCells.Remove(gameObject);
    }
    override public void mutateBehaviour(){
        if (randomChancePercentage.More(gene_stability)){
            fearOfEnemies = fearOfEnemies + (double)UnityEngine.Random.Range(-10,10)/10;
        }
        if (randomChancePercentage.More(gene_stability)){
            avoidness = avoidness + (double)UnityEngine.Random.Range(-10,10)/10;
        }
        if(avoidness*fearOfEnemies == 0){
            ClearFromWorld();
        }
    }
}

