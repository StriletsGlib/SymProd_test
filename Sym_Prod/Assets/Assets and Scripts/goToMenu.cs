using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goToMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SymProdMenuManager>().changeSceneToString("Menu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
