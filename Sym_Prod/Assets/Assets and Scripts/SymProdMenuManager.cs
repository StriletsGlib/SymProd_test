using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SymProdMenuManager : MonoBehaviour
{
    public void changeSceneToString(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
    public void startGame(){
        SceneManager.LoadScene("MainScene");
        //Debug.Log("aidfjs9i");
    }
    public void loadSetting(){
        SceneManager.LoadScene("Settings");
    }
    public void exitGame(){
        Application.Quit();
    }
}
