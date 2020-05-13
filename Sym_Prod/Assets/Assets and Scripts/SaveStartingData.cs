using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SaveStartingData : MonoBehaviour
{
    //bool worked = true;
    public void LoadMainScene(){
        SceneManager.LoadScene("MainScene");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void saveDataDo(){
        //worked = true;
        GameObject InpAC = GameObject.Find("InputNumberOfAttackingCells");
        GameObject InpPC = GameObject.Find("InputNumberOfPeacefullCells");
        GameObject InpSC = GameObject.Find("InputNumberSavedCells");
        GameObject SpeedSource = GameObject.Find("InputGameSpeed");
        GameObject FoodSource = GameObject.Find("InputFoodSpawnRate");
        GameObject PresetChance = GameObject.Find("InputPresetChance");
        GameObject UseNNChance = GameObject.Find("InputNNChance");
        GameObject DoRestart = GameObject.Find("DoRestart");
        GameObject DBK = GameObject.Find("DoBordersKill");
        GameObject InpHungerModifier = GameObject.Find("InputHungerModifier");
        ImportantData startData = GameObject.Find("Start_Data").GetComponent<ImportantData>();
        startData.cellASpawn=tryToTransform(InpAC.GetComponent<InputField>().text,startData.cellASpawn);
        startData.cellPSpawn=tryToTransform(InpPC.GetComponent<InputField>().text,startData.cellPSpawn);
        startData.saveBestNum=tryToTransform(InpSC.GetComponent<InputField>().text,startData.saveBestNum);
        startData.doBordersKill = DBK.GetComponent<Toggle>().isOn;
        startData.restartWhenDead = DoRestart.GetComponent<Toggle>().isOn;
        startData.gameSpeed=tryToTransform(SpeedSource.GetComponent<InputField>().text,startData.gameSpeed);
        startData.foodSpawnRate=tryToTransform(FoodSource.GetComponent<InputField>().text,startData.foodSpawnRate);
        
        startData.chanceOfNNCell=tryToTransform(UseNNChance.GetComponent<InputField>().text,startData.chanceOfNNCell);
        
        startData.chanceOfPreset=tryToTransform(PresetChance.GetComponent<InputField>().text,startData.chanceOfPreset);
        startData.hunger_modifier=tryToTransform(InpHungerModifier.GetComponent<InputField>().text,startData.hunger_modifier);
        //startData.cellASpawn=tryToTransform(InpAC.GetComponent<InputField>().text,startData.cellASpawn);
        //startData.cellASpawn=tryToTransform(InpAC.GetComponent<InputField>().text,startData.cellASpawn);
        //startData.cellASpawn=tryToTransform(InpAC.GetComponent<InputField>().text,startData.cellASpawn);
        //startData.cellASpawn=tryToTransform(InpAC.GetComponent<InputField>().text,startData.cellASpawn);
        //startData.cellASpawn=tryToTransform(InpAC.GetComponent<InputField>().text,startData.cellASpawn);
        //Debug.Log("sidjo");
        //LoadMainScene();
    }
    int tryToTransform(string toInt, int num){
        int temp = num;
        if (int.TryParse(toInt, out temp)){
            num =temp;
        }
        return num;
    }
    float tryToTransform(string toFloat, float num){
        float temp = num;
        if (float.TryParse(toFloat, out temp)){
            num =temp;
        }
        return num;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
