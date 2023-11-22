using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : MonoBehaviour
{
    public static BombProjectile ins;
    public GameObject bombPrefab;
    ParticleSystem particl;
    SphereCollider bombCollider;
    GameObject bomb;
    private void Awake()
    {
        ins = this;
    }
    public void Attack()
    {
        bomb = Instantiate(bombPrefab, transform.position, transform.rotation);
        bomb.GetComponent<Rigidbody>().AddForce(transform.forward * 7f, ForceMode.VelocityChange);
        bomb.GetComponent<Rigidbody>().AddForce(Vector3.up * 7f / 4f, ForceMode.VelocityChange);
        particl = bomb.GetComponentInChildren<ParticleSystem>();
        bombCollider = bomb.GetComponent<SphereCollider>();
        StartCoroutine(ExplosionBomb());
    }

    IEnumerator ExplosionBomb()
    {
        yield return new WaitForSeconds(2f);
        bombCollider.enabled = true;
        particl.Play();
        AudioManager.instance.PlaySFX("bomb");
        yield return new WaitForSeconds(1f);
        Destroy(bomb);
    }

}
