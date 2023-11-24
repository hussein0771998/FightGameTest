using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ZombEnemy : MonoBehaviour
{

    Transform player;
    
    [SerializeField] Animator enemyAnimation;
    public float followSpeed = 5f;
    private NavMeshAgent enemyNav;
    public GameObject coinPrefab;
    float distanceBetween;
    int coinNumber = 1;
    public SphereCollider zombieCollider;
    public ParticleSystem hitPartical;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Arrow" || other.tag=="Player")
        {
           
            if (PlayerPrefs.GetInt("shootarrow1") == 1 || PlayerPrefs.GetInt("shootSword1") == 1)
            {
                hitPartical.Play();
                AudioManager.instance.PlayEnemySFX("zombie die");
                enemyAnimation.SetBool("Die", true);
                PlayerPrefs.SetInt("shootarrow1", 0);
                PlayerPrefs.SetInt("shootSword1", 0);
                zombieCollider.enabled = false;
                StartCoroutine(DropCoin());
                Destroy(gameObject, 1f);
               
                
            }


        }
       
    }
    private void Start()
    {
        AudioManager.instance.PlayMusic("enemy2");
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();

        enemyAnimation = gameObject.GetComponent<Animator>();
        enemyNav = gameObject.GetComponent<NavMeshAgent>();


    }

    private void Update()
    {
        if (player == null)
            return;
        transform.LookAt(player);

        distanceBetween = Vector3.Distance(player.position, transform.position);
        //  Debug.Log(distanceBetween);
        if (distanceBetween > enemyNav.stoppingDistance)
        {

            enemyNav.SetDestination(player.position);
            enemyNav.isStopped = false;
            enemyAnimation.SetBool("walk", true);
            enemyAnimation.SetBool("attack", false);

        }
        else
        {
            enemyNav.isStopped = true;
            enemyAnimation.SetBool("walk", false);
            enemyAnimation.SetBool("attack", true);
            PlayerPrefs.SetInt("zombieHit", 1);
        }

    }

    IEnumerator DropCoin()
    {
        
        while (coinNumber <= 5)
        {
            GameObject coin = Instantiate(coinPrefab, transform.position, transform.rotation);

            // Calculate random offset within a small range
            float offsetX = Random.Range(-1f, 1f); // Adjust the range as needed
            float offsetZ = Random.Range(-1f, 1f); // Adjust the range as needed


            // Apply the random offset to the coin's position
            coin.transform.position += new Vector3(offsetX, 3f, offsetZ);

            coin.gameObject.name = "Coin " + coinNumber.ToString();

            yield return new WaitForSeconds(0.2f);
            coinNumber++;
        }



    }
}
