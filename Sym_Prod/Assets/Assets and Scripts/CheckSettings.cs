using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckSettings : MonoBehaviour
{
    public void showSettings(){
    GameObject NACO = GameObject.Find("NACell");
    GameObject NPCO = GameObject.Find("NPCell");
    GameObject NSCO = GameObject.Find("NSCell");
    GameObject speed = GameObject.Find("GameSpeed");
    GameObject rate = GameObject.Find("FoodRate");
    GameObject chance = GameObject.Find("PresetChance");
    }
}
