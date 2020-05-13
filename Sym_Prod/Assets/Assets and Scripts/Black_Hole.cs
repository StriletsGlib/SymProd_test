using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Black_Hole : MonoBehaviour
{
    bool shouldDestoryCells = true;
    // Start is called before the first frame update
    void Start()
    {
        shouldDestoryCells = gameObject.GetComponent<BoxCollider2D>().isTrigger;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if (shouldDestoryCells&((collision.gameObject.CompareTag("Attacking_Cell")) || (collision.gameObject.CompareTag("Peacefull_Cell")))){
            collision.GetComponent<Cell>().ClearFromWorld();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Food")){
            collision.GetComponent<Food_Exist>().ClearFromWorld();
            Destroy(collision.gameObject);
        }
    }
}
