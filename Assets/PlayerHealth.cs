using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    Animator playerAnim;
    Rigidbody playerRB;
    private void Start()
    {
        playerAnim = GameObject.FindWithTag("Player").GetComponent<Animator>();
        playerRB = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyGirl")
        {
            if(PlayerPrefs.GetInt("hitplayer")== 1)
            {
                playerAnim.SetBool("hit",true);
               // playerRB.constraints = RigidbodyConstraints.None; // Allow movement
                playerRB.AddForce(-transform.forward * 2f, ForceMode.VelocityChange); // Adjust force
                playerRB.interpolation = RigidbodyInterpolation.None; // Disable interpolation
                Debug.Log("EnemyGirl Hit Player");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "EnemyGirl")
        {
            playerAnim.SetBool("hit", false);
            PlayerPrefs.SetInt("hitplayer", 0);
        }
    }
}
