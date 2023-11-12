using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectTile : MonoBehaviour
{
    public static ArrowProjectTile instance;
    public GameObject arrowPrefab;
    public float shootSpeed = 10f;
    public bool canShoot = true;
    private void Awake()
    {
        instance = this;
    }
  
    public void ShootArow()
    {
        if (canShoot)
        {
            GameObject arrow = Instantiate(arrowPrefab, transform.position, transform.rotation);

            arrow.GetComponent<Rigidbody>().AddForce(transform.forward * shootSpeed, ForceMode.VelocityChange);
            arrow.GetComponent<Rigidbody>().AddForce(Vector3.up * shootSpeed / 4f, ForceMode.VelocityChange);
            Destroy(arrow, 2f);
            StartCoroutine(CanShot());
        }
       
        canShoot = false;
    }

    private IEnumerator CanShot()
    {
        yield return new WaitForSeconds(1f);
        canShoot = true;
    }
}
