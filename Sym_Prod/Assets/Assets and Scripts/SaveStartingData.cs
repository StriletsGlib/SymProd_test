using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveStartingData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void saveData(){
        GameObject InpAC = GameObject.Find("InputNumberOfAttackingCells");
        GameObject InpPC = GameObject.Find("InputNumberOfPeacefullCells");
        GameObject InpSC = GameObject.Find("InputNumberSavedCells");
        
        ImportantData startData = GameObject.Find("Start Data").GetComponent<ImportantData>();
        string temp = InpAC.GetComponent<Text>();
        ImportantData.PCellSpawn = 0;
        ImportantData.ACellSpawn = 0;
        ImportantData.CellSave = 0;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
