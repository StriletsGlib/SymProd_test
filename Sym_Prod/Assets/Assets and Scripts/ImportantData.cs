using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImportantData : MonoBehaviour
{
    public int PCellSpawn = 0, ACellSpawn = 0, CellSave = 0;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad (gameObject);
    }

    // Update is called once per frame
}
