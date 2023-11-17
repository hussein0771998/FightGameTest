using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    public List<GameObject> enemys;

    private void Start()
    {
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("EnemyGirl");
        enemys.AddRange(enemyObjects);
    }

    private void Update()
    {
        

        if (enemys.Count == 0)
        {
            Debug.Log("all killed");
        }
    }
}
