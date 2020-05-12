using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SaveStartingData : MonoBehaviour
{
    bool worked = true;
    public void LoadMainScene(){
        SceneManager.LoadScene("MainScene");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void saveData(){
        worked = true;
        GameObject InpAC = GameObject.Find("InputNumberOfAttackingCells");
        GameObject InpPC = GameObject.Find("InputNumberOfPeacefullCells");
        GameObject InpSC = GameObject.Find("InputNumberSavedCells");
        GameObject DBK = GameObject.Find("DoBordersKill");
        
        ImportantData startData = GameObject.Find("Start_Data").GetComponent<ImportantData>();
        putImportantInfoInt(InpAC, 1, startData);
        putImportantInfoInt(InpPC, 0, startData);
        putImportantInfoInt(InpSC, 2, startData);
        startData.boolData[0] = DBK.GetComponent<Toggle>().isOn;
        if (worked){
            LoadMainScene();
        }
        //ImportantData.ACellSpawn = 0;
        //ImportantData.CellSave = 0;
    }
    void putImportantInfoInt(GameObject textFrom, int textTo, ImportantData startDataSend ){
        string temp = textFrom.GetComponent<InputField>().text;
        int tempInt = 0;
        if (!int.TryParse(temp, out tempInt)){
            textFrom.GetComponent<InputField>().text="";
            worked = false;
        }
        startDataSend.intData[textTo] = tempInt;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
