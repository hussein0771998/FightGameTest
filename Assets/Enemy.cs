using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Transform player;
    float distanceBetween;
    [SerializeField] Animator enemyAnimation;
    public float followSpeed = 5f;
    private NavMeshAgent enemyNav;
    [SerializeField] int enemy1Num;
    public Image healthBar;
    public GameObject coinPrefab;
    int coinNumber = 0;
    bool playDieOnce;
    public SphereCollider girlGun;
    public ParticleSystem hitPartical;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Arrow")
        {
            //Debug.Log("Hit By Arrow ");
            if (PlayerPrefs.GetInt("shootarrow1") == 1)
            {
                hitPartical.Play();
                AudioManager.instance.PlayEnemySFX("girl hit");
                Debug.Log("Hit By arrow ");
                enemyAnimation.SetBool("hit2", true);
                healthBar.fillAmount -= 0.35f;
                PlayerPrefs.SetInt("shootarrow1", 0);
                StartCoroutine(StopAnimation());


            }

            //  Destroy(gameObject);
        }
        if (other.tag == "Sword")
        {
            if (PlayerPrefs.GetInt("shootSword1") == 1)
            {
                hitPartical.Play();
                AudioManager.instance.PlayEnemySFX("girl hits");
                healthBar.fillAmount -= 0.35f;
                Debug.Log("Hit By Sword ");
                enemyAnimation.SetBool("hit",true);
                PlayerPrefs.SetInt("shootSword1", 0);
                StartCoroutine(StopAnimation());
            }
            
           // Destroy(gameObject);
        }
    }
  /*  private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Sword")
        {
            enemyAnimation.SetBool("hit", false);
        }
       
    }*/

    IEnumerator StopAnimation()
    {
        yield return new WaitForSeconds(1f);
        enemyAnimation.SetBool("hit", false);
        enemyAnimation.SetBool("hit2", false);
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
       
        enemyAnimation = gameObject.GetComponent<Animator>();
        enemyNav = gameObject.GetComponent<NavMeshAgent>();
        playDieOnce = true;

    }

    private void Update()
    {
        if (player == null)
            return;


        distanceBetween = Vector3.Distance(player.position, transform.position);

        //  Debug.Log(distanceBetween);
        if (distanceBetween <= 7f && distanceBetween > enemyNav.stoppingDistance)
        {

            enemyNav.SetDestination(player.position);
            enemyNav.isStopped = false;
            enemyAnimation.SetBool("walk", true);
            enemyAnimation.SetBool("attack2", false);
            enemyAnimation.SetBool("attack1", false);

        }
        if (distanceBetween <= enemyNav.stoppingDistance)
        {
            enemyNav.isStopped = true;
           // Debug.Log("2");
            enemyAnimation.SetBool("walk", false);

            if (enemy1Num == 1)
            {
                enemyAnimation.SetBool("attack1", true);
                PlayerPrefs.SetInt("girlhitplayer", 1);
               
            }
            else
            {
               
                enemyAnimation.SetBool("attack2", true);
                PlayerPrefs.SetInt("girlhitplayer", 1);
            }
               

            transform.LookAt(player);
           
           
        }
        if (distanceBetween > 7f)
        {
            enemyNav.isStopped = true;
            enemyAnimation.SetBool("walk", false);
            enemyAnimation.SetBool("attack2", false);
            enemyAnimation.SetBool("attack1", false);
        }

        if (healthBar.fillAmount <= 0)
        {
            enemyNav.isStopped = true;

            if (playDieOnce)
            {
                girlGun.enabled = false;
                enemyAnimation.SetBool("Die", true);
                AudioManager.instance.PlayEnemySFX("girl die");
                playDieOnce = false;
            }
                

            DropCoin();
           
            Destroy(gameObject,1f);
        }
    }

    void DropCoin()
    {
        if (coinNumber <= 5)
        {
            GameObject coin = Instantiate(coinPrefab, transform.position, transform.rotation);

            // Calculate random offset within a small range
            float offsetX = Random.Range(-1f, 1f); // Adjust the range as needed
            float offsetZ = Random.Range(-1f, 1f); // Adjust the range as needed
           

            // Apply the random offset to the coin's position
            coin.transform.position += new Vector3(offsetX, 3f, offsetZ);

            coin.gameObject.name = "Coin " + coinNumber.ToString();
        }
        coinNumber += 1;
    }


}
