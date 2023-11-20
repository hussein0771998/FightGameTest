using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public GameObject cannon;
    public List<GameObject> enemys;
    public string enemyTag;
    public bool next;
    public GameObject LevelStatistic;
    bool oneTimeCallUpdate;
    public GameObject bombButton;
    public GameObject useBomb;
    private void Start()
    {
        oneTimeCallUpdate = true;
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag(enemyTag);
        enemys.AddRange(enemyObjects);

        if (enemyTag == "EnemyGirl")
            PlayerPrefs.SetInt("levelNumber", 0);

        if (enemyTag == "Zombie")
            PlayerPrefs.SetInt("levelNumber", 1);
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

            if (oneTimeCallUpdate)
            {
                if (enemyTag == "Zombie")
                    Destroy(cannon);

                oneTimeCallUpdate = false;
                StartCoroutine(PlayStatistic());

            }
            //Destroy(gameObject, 2f);

            
        }
    }

    IEnumerator PlayStatistic()
    {
        yield return new WaitForSeconds(1f);
        if (enemyTag == "Zombie")
        {
            bombButton.SetActive(true);
            useBomb.SetActive(true);

            Destroy(useBomb, 3f);

        }

        LevelStatistic.SetActive(true);
        
    }

  
}
