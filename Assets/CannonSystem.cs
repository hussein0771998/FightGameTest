using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonSystem : MonoBehaviour
{
    public GameObject cannon;
    Transform Player;
    //int stoneCount = 0;
    public bool playerIn;
    private bool isAttacking = false;
    private void Start()
    {
        Player = GameObject.FindWithTag("Player").transform;
    }
    private void OnTriggerStay(Collider other)
    {
        cannon.transform.LookAt(Player);
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIn = false;
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !isAttacking)
        {
            playerIn = true;
            StartCoroutine(AttackSystem());
        }
    }
    
    IEnumerator AttackSystem()
    {
        isAttacking = true;
        while (playerIn)
        {
            Debug.Log("attackStone out");

            CannonEnemy.instance.AttackStone();
            yield return new WaitForSeconds(3f);

           // Debug.Log("cannon");
        }
        isAttacking = false;
    }
}
