using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Arrow")
        {
            Debug.Log("Hit By Arrow ");
          //  Destroy(gameObject);
        }
        if (other.tag == "Sword")
        {
            Debug.Log("Hit By Sword ");
           // Destroy(gameObject);
        }
    }
}
