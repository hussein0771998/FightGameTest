using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GolemEnemy : MonoBehaviour
{
    [SerializeField] NavMeshAgent golemNavmesh;
    [SerializeField] Transform player;
    public bool move;
    Animator golemAnimator;
    void Start()
    {
       
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        golemNavmesh.speed = 1;
        golemAnimator = gameObject.GetComponent<Animator>();
        golemNavmesh = gameObject.GetComponent<NavMeshAgent>();
        StartCoroutine(GolemSystem());
    }


    void Update()
    {
        if (player == null)
            return;
        golemNavmesh.SetDestination(player.position);
    }

    IEnumerator GolemSystem()
    {
        
        while (true)
        {
            if (player == null)
                yield break;

            yield return new WaitForSeconds(8f);
            // move = false;
            golemNavmesh.isStopped = true;
            golemAnimator.SetBool("victory", false);
            golemAnimator.SetBool("walk", false);
            golemAnimator.SetBool("attack1", true);
            yield return new WaitForSeconds(1.5f);
            GolemProjectile.instance.ThrowBomb();
            yield return new WaitForSeconds(1.5f);
               
            golemAnimator.SetBool("walk", false);
            golemAnimator.SetBool("victory", true);
            golemAnimator.SetBool("attack1", false);
           // golemNavmesh.isStopped = true;
            yield return new WaitForSeconds(3.2f);
            // move = true;
            transform.LookAt(player);
            golemNavmesh.isStopped = false;
            golemAnimator.SetBool("walk", true);
            golemAnimator.SetBool("victory", false);
           
            
        }

    }
}
