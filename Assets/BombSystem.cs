using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BombSystem : MonoBehaviour
{
    Transform player;
    NavMeshAgent bombNavmesh;
    Animator bombAnimator;
    float distanceBetween;
    public ParticleSystem smoke;
    bool playVFX = true;
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        bombNavmesh = gameObject.GetComponent<NavMeshAgent>();
        bombAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log("distanceBetween =  " + distanceBetween);
        distanceBetween = Vector3.Distance(player.position, transform.position);

        if (bombNavmesh != null)
        {
            if (distanceBetween <= bombNavmesh.stoppingDistance)
            {
                bombNavmesh.isStopped = true;
               //bombNavmesh.speed = 0;

                if (playVFX)
                {
                    bombAnimator.SetBool("walk", false);
                    bombAnimator.SetBool("attack01",true);
                   
                    smoke.Play();
                    Destroy(gameObject, 1f);
                    playVFX = false;
                }



            }
            else
            {
                bombNavmesh.SetDestination(player.position);
                bombAnimator.SetBool("walk", true);
                bombAnimator.SetBool("attack01", false);
            }


        }

    }
}
