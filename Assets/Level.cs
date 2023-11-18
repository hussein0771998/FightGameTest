using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public List<GameObject> enemys;
    public GameObject wall,nextLevel;
    public string enemyTag;
    public bool next;
    
    private void Start()
    {
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag(enemyTag);
        enemys.AddRange(enemyObjects);
    }

    private void Update()
    {
        for (int i = enemys.Count - 1; i >= 0; i--)
        {
            GameObject enemy = enemys[i];

            // Check if the GameObject is still active in the scene
            if (enemy == null || !enemy.activeSelf)
            {
                // Remove the destroyed GameObject from the list
                enemys.RemoveAt(i);
            }
        }

        if (enemys.Count == 0)
        {
            wall.GetComponent<Animator>().SetBool("down", true);
            //Debug.Log("all killed");
            if(next)
                nextLevel.SetActive(true);

            Destroy(gameObject, 3f);
        }
    }
}
