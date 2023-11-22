using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonEnemy : MonoBehaviour
{
    public static CannonEnemy instance;
    public GameObject stonePrefab;
    [SerializeField] Rigidbody _rb;
    public ParticleSystem stoneFire;

    private void Awake()
    {
        instance = this;
    }
    public void AttackStone()
    {
       
        GameObject stone= Instantiate(stonePrefab, transform.position, Quaternion.identity);
        _rb = stone.GetComponent<Rigidbody>();
        _rb.AddForce(transform.forward * 14f, ForceMode.VelocityChange);
        stoneFire.Play();
        AudioManager.instance.PlaySFX("bomb");
        Destroy(stone, 1.25f);
        Debug.Log("attackStone in");
    }

}
