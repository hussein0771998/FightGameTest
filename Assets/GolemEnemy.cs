using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemEnemy : MonoBehaviour
{
    public GameObject bombPrefab;
    void Start()
    {
        StartCoroutine(InstaniateBomb());
    }

    IEnumerator InstaniateBomb()
    {
        
        while (true)
        {
            yield return new WaitForSeconds(4f);
            GameObject bomb = Instantiate(bombPrefab, transform.position, transform.rotation);
            bomb.transform.position += new Vector3(0f, 0f, -3f);
        }
      
    }
}
