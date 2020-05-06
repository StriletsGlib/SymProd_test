using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Being_Eaten : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("cell"))
        {
            Cell consumer = collision.GetComponent<Cell>();
            Debug.Log("Deus");
            consumer.EatFood();
            Destroy(gameObject);
        }
    }
}
