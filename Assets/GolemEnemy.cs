using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GolemEnemy : MonoBehaviour
{
    public GameObject winMenu;
    [SerializeField] NavMeshAgent golemNavmesh;
    [SerializeField] Transform player;
    [SerializeField] float swordDamage;
    [SerializeField] float arrowDamage;
    [SerializeField] float bombDamage;
    public bool move;
    Animator golemAnimator;
    public GameObject golemArm;
    BoxCollider arm;
    public Image healthBar;
    bool oneTimePlayUpdate;
    public ParticleSystem hitPartical;
    void Start()
    {
        AudioManager.instance.PlayMusic("enemy3");
        oneTimePlayUpdate = true;
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        golemNavmesh.speed = 1;
        golemAnimator = gameObject.GetComponent<Animator>();
        golemNavmesh = gameObject.GetComponent<NavMeshAgent>();
        arm = golemArm.GetComponent<BoxCollider>();
        StartCoroutine(GolemSystem());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sword")
        {
            hitPartical.Play();
            AudioManager.instance.PlayEnemySFX("golem hit");
            golemAnimator.SetBool("hit", true);
            healthBar.fillAmount -= swordDamage / 100;

        }

        if (other.tag == "Arrow")
        {
            hitPartical.Play();
            AudioManager.instance.PlayEnemySFX("golem hit");
            healthBar.fillAmount -= arrowDamage / 100;
            golemAnimator.SetBool("hit", true);
        }

        if (other.tag == "playerbomb")
        {
            hitPartical.Play();
            AudioManager.instance.PlayEnemySFX("golem hit");
            healthBar.fillAmount -= bombDamage / 100;
            golemAnimator.SetBool("hit", true);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Sword" || other.tag == "Arrow" || other.tag == "playerbomb")
        {

            golemAnimator.SetBool("hit", false);

        }


    }

    void Update()
    {
        if (player == null)
            return;
        golemNavmesh.SetDestination(player.position);

        if (healthBar.fillAmount <= 0)
        {
            if (oneTimePlayUpdate)
            {
                AudioManager.instance.PlayEnemySFX("golem die");
                golemAnimator.SetBool("die", true);
                StartCoroutine(PlayWin());
                oneTimePlayUpdate = false;
            }
        }
        
    }
    IEnumerator PlayWin()
    {
        yield return new WaitForSeconds(2f);
        UIManager.instance.Win(winMenu);
        Destroy(gameObject, 2f);
    }

    IEnumerator GolemSystem()
    {
        
        while (true)
        {
            if (player == null)
                yield break;

            yield return new WaitForSeconds(8f);
            golemNavmesh.isStopped = true;
            golemAnimator.SetBool("walk", false);
            golemAnimator.SetBool("victory", false);
            golemAnimator.SetBool("attack2", true);
            AudioManager.instance.PlayEnemySFX("golem attack");
            arm.enabled = true;
            yield return new WaitForSeconds(2f);
            arm.enabled = false;
            // move = false;
            // golemNavmesh.isStopped = true;

            golemAnimator.SetBool("attack2", false);
            golemAnimator.SetBool("attack1", true);
            yield return new WaitForSeconds(1.5f);
            GolemProjectile.instance.ThrowBomb();
            yield return new WaitForSeconds(1.5f);
               
            golemAnimator.SetBool("walk", false);
            golemAnimator.SetBool("victory", true);
            AudioManager.instance.PlayEnemySFX("golem victory");
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
