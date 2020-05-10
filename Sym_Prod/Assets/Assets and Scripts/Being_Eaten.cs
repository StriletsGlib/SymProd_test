using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Being_Eaten : MonoBehaviour
{
    public string whatTag = "Peacefull_Cell";
    public bool itIsACell = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Deus");
        if (collision.gameObject.CompareTag(whatTag))
        {
            //Debug.Log("Vault");
            if (itIsACell){
                Cell consumer = collision.GetComponent<Cell>();
                //Debug.Log("AAA!");
                Cell itself = gameObject.GetComponent<Cell>();
                itself.ClearFromWorld();
                //Debug.Log("BBB");
                consumer.EatFood(itself.energy_count);
            }
            else{
                Cell consumer = collision.GetComponent<Cell>();
                consumer.EatFood();
                Game_World world;
                world = GameObject.Find("GameWorld_1").GetComponent<Game_World>();
                world.foods.Remove(gameObject);
            }
            Destroy(gameObject);
        }
    }
}
