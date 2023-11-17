using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonSystem : MonoBehaviour
{
    public GameObject cannon;
    Transform Player;
    int stoneCount = 0;
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
            StopCoroutine(AttackSystem());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(AttackSystem());
        }
    }
    
    IEnumerator AttackSystem()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);

            CannonEnemy.instance.AttackStone();
        }

    }
}
