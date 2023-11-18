using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemProjectile : MonoBehaviour
{
    public static GolemProjectile instance;
    public GameObject bombPrefab;
    public int bombCount;
    private void Awake()
    {
        instance = this;
    }
    public void ThrowBomb()
    {
        StartCoroutine(InstaniateBomb());
    }

    IEnumerator InstaniateBomb()
    {
        int start = 0;
        while (start < bombCount)
        {
            yield return new WaitForSeconds(0.5f);
            GameObject bomb = Instantiate(bombPrefab, transform.position, transform.rotation);
            bomb.transform.position += new Vector3(0f, 0f, -3f);
            start++;
        }
      
    }
}
