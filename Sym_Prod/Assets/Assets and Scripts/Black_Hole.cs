using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Black_Hole : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if ((collision.gameObject.CompareTag("Attacking_Cell")) || (collision.gameObject.CompareTag("Peacefull_Cell"))){
            collision.GetComponent<Cell>().ClearFromWorld();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Food")){
            collision.GetComponent<Food_Exist>().ClearFromWorld();
            Destroy(collision.gameObject);
        }
    }
}
