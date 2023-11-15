using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform player;
    float distanceBetween;
    [SerializeField] Rigidbody _rig;
    [SerializeField] Animator enemyAnimation;
    public float followSpeed = 5f;
    private NavMeshAgent enemyNav;
    [SerializeField] int enemy1Num;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Arrow")
        {
            Debug.Log("Hit By Arrow ");
          //  Destroy(gameObject);
        }
        if (other.tag == "Sword")
        {
            Debug.Log("Hit By Sword ");
           // Destroy(gameObject);
        }
    }
    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        _rig = gameObject.GetComponent<Rigidbody>();
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
    }

}
