using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Being_Eaten : MonoBehaviour
{
    private void OnTRiggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("cell"))
        {
            
            
        }
    }
}
