using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class PlayerHealth : MonoBehaviour
{
    Animator playerAnim;
    Rigidbody playerRB;
    public Image healthImg;
    [SerializeField] float EnemygirlDamage;
    [SerializeField] float zombieDamage;
    [SerializeField] float cannonDamage;
    [SerializeField] float bombDamage;
    [SerializeField] float GolemDamage;
    [SerializeField] float healthEarn;
    bool die = true;
    public DOTweenAnimation cameraShake;
    public GameObject loseMenu;
    private void Start()
    {
        playerAnim = GameObject.FindWithTag("Player").GetComponent<Animator>();
        playerRB = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "girl")
        {
            if(PlayerPrefs.GetInt("girlhitplayer")== 1)
            {
                cameraShake.DORestart();
                
                AudioManager.instance.PlaySFX("hit");
                playerAnim.SetBool("hit",true);
               // playerRB.constraints = RigidbodyConstraints.None; // Allow movement
                playerRB.AddForce(-transform.forward * 2f, ForceMode.VelocityChange); // Adjust force
                playerRB.interpolation = RigidbodyInterpolation.None; // Disable interpolation
                Debug.Log("girl Hit Player");

                healthImg.fillAmount -= EnemygirlDamage / 100;
            }
        }
        if (other.tag == "ZombieHit")
        {
            if (PlayerPrefs.GetInt("zombieHit") == 1)
            {
                AudioManager.instance.PlaySFX("hit");
                playerAnim.SetBool("hit", true);
                healthImg.fillAmount -= zombieDamage / 100;
            }
           
        }
        
        if (other.tag == "bomb")
        {
            if (PlayerPrefs.GetInt("BombHit") == 1)
            {
                AudioManager.instance.PlaySFX("hit");
                playerAnim.SetBool("hit", true);
                healthImg.fillAmount -= bombDamage / 100;
            }
           
        }

        if(other.tag== "cannon")
        {
            AudioManager.instance.PlaySFX("hit");
            playerAnim.SetBool("hit", true);
            healthImg.fillAmount -= cannonDamage / 100;
        } 
        
        if(other.tag== "golemarm")
        {
            AudioManager.instance.PlaySFX("hit");
            playerAnim.SetBool("hit", true);
            healthImg.fillAmount -= GolemDamage / 100;
        }

        if (other.tag == "health")
        {
            healthImg.fillAmount += healthEarn / 100;
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "girl")
        {
           
            playerAnim.SetBool("hit", false);
            PlayerPrefs.SetInt("girlhitplayer", 0);
        }
        
        if (other.tag == "ZombieHit")
        {
            playerAnim.SetBool("hit", false);
            PlayerPrefs.SetInt("zombieHit", 0);
        } 
        
        if (other.tag == "bomb")
        {
            playerAnim.SetBool("hit", false);
            PlayerPrefs.SetInt("BombHit", 0);
        }
        
        if (other.tag == "cannon" || other.tag == "golemarm")
        {
            playerAnim.SetBool("hit", false);
        }


    }

    private void Update()
    {
        if (healthImg.fillAmount <= 0)
        {
            if (die)
            {
                AudioManager.instance.PlaySFX("die");
                playerAnim.SetTrigger("die");
                StartCoroutine(EndGame());
               
               
            }
            die = false;
        }
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(1.5f);
        UIManager.instance.Lose(loseMenu);
        Destroy(gameObject, 1.5f);
    }
}
