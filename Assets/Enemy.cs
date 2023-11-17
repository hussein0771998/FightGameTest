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
    
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Arrow")
        {
            //Debug.Log("Hit By Arrow ");
            if (PlayerPrefs.GetInt("shootarrow1") == 1)
            {
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


    }

    private void Update()
    {
        
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
            Debug.Log("2");
            enemyAnimation.SetBool("walk", false);

            if (enemy1Num == 1)
                enemyAnimation.SetBool("attack1", true);
            else
                enemyAnimation.SetBool("attack2", true);

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
            enemyAnimation.SetBool("Die", true);
            Destroy(gameObject,2f);
        }
    }

}
