using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : MonoBehaviour
{
    public static BombProjectile ins;
    public GameObject bombPrefab;

    private void Awake()
    {
        ins = this;
    }
    public void attack()
    {
        GameObject bomb = Instantiate(bombPrefab, transform.position, transform.rotation);
        bomb.GetComponent<Rigidbody>().AddForce(transform.forward * 7f, ForceMode.VelocityChange);
        bomb.GetComponent<Rigidbody>().AddForce(Vector3.up * 7f / 4f, ForceMode.VelocityChange);
    }

}
