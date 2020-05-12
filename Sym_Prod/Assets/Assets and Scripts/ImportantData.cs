using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImportantData : MonoBehaviour
{
    int amountOfIntData = 3;
    public List<int> intData = new List<int>();
    int amountOfBoolData = 1;
    public List<bool> boolData = new List<bool>();
    public int getPCellSpawn(){
        return intData[0];
    }
    public int getACellSpawn(){
        return intData[1];
    }
    public int getCellSave(){
        return intData[2];
    }
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i<amountOfBoolData; i++){
            boolData.Add(false);
        }
        for(int i = 0; i<amountOfIntData; i++){
            intData.Add(0);
        }
        DontDestroyOnLoad (gameObject);
    }

    // Update is called once per frame
}
