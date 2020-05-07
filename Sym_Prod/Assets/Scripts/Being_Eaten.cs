using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Being_Eaten : MonoBehaviour
{
    public string tag = "Peacefull_Cell";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(tag))
        {
            Cell consumer = collision.GetComponent<Cell>();
            //Debug.Log("Deus");
            consumer.EatFood();
            Destroy(gameObject);
        }
    }
}
