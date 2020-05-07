using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Being_Eaten : MonoBehaviour
{
    public string whatTag = "Peacefull_Cell";
    public bool itIsACell = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(whatTag))
        {
            Cell consumer = collision.GetComponent<Cell>();
            //Debug.Log("Deus");
            consumer.EatFood();
            if(itIsACell){
                CendEnergy(consumer);
            };
            Destroy(gameObject);
        }
    }
    void CendEnergy(Cell consumer){
        Cell itself = gameObject.GetComponent<Cell>();
        consumer.energy_count= consumer.energy_count + itself.energy_count/10;
    }
}
